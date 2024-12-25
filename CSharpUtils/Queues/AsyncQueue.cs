﻿using System.Collections.Concurrent;

namespace Utils.CSharp.Queues;

public class AsyncQueue<T>(Action<T> onDequeue, int degreeOfParallelisation = 1, int intervalDelay = 10, int maxRetries = 3) : IDisposable
{
    public AsyncQueue(Func<T, Task> onDequeue, int degreeOfParallelisation = 1, int intervalDelay = 10, int maxRetries = 3) : this(item => onDequeue(item).GetAwaiter().GetResult(), degreeOfParallelisation: degreeOfParallelisation, intervalDelay: intervalDelay, maxRetries: maxRetries)
    {

    }

    public void Dispose()
    {
        CancellationTokenSource.CreateLinkedTokenSource(CancellationToken).Cancel();
        Semaphore.Dispose();
        GC.SuppressFinalize(this);
    }

    public async Task Enqueue(T item)
    {
        var isComplete = false;
        Queue.Enqueue((item, () => isComplete = true));
        Start();

        while (!isComplete)
            await Task.Delay(intervalDelay).ConfigureAwait(false);
    }

    public AsyncQueue<T> WithExceptionHandler(Action<Exception> onException)
    {
        OnException = onException;
        return this;
    }

    private void Loop(object? stateInfo)
    {
        try
        {
            Semaphore.Wait();

            var retryCount = 1;
            while (!CancellationToken.IsCancellationRequested)
            {
                if (CancellationToken.IsCancellationRequested)
                    return;

                if (!Queue.TryDequeue(out var item))
                {
                    if (retryCount > maxRetries)
                        return;

                    retryCount++;
                    Thread.Sleep(intervalDelay);
                    continue;
                }

                try
                {
                    onDequeue(item.Value);
                }
                catch (Exception ex)
                {
                    if (OnException == null)
                    {
                        Faults.Add(ex);
                        continue;
                    }

                    OnException(ex);
                }
                finally
                {
                    item.IsComplete();
                }
            }
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private void Start()
    {
        if (!(Semaphore.CurrentCount > 0))
            return;

        for (var i = Semaphore.CurrentCount; i > 0; i--)
            ThreadPool.QueueUserWorkItem(Loop);
    }

    private CancellationToken CancellationToken { get; } = new();
    private ConcurrentBag<Exception> Faults { get; } = [];
    private Action<Exception>? OnException { get; set; }
    private ConcurrentQueue<(T Value, Func<bool> IsComplete)> Queue { get; } = new();
    private SemaphoreSlim Semaphore { get; } = new(degreeOfParallelisation);
}
