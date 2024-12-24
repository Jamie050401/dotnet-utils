using System.Collections.Concurrent;

namespace Utils.CSharp.Queues;

public static class AsyncQueueFactory
{
    public static AsyncQueue<T> Create<T>(Func<T, Task> onDequeue, int degreeOfParallelisation = 1) => new(item => onDequeue(item).Wait(), degreeOfParallelisation);

    public static AsyncQueue<T> Create<T>(Action<T> onDequeue, int degreeOfParallelisation = 1) => new(onDequeue, degreeOfParallelisation);
}

public class AsyncQueue<T> : IDisposable
{
    internal AsyncQueue(Action<T> onDequeue, int degreeOfParallelisation)
    {
        OnDequeue = onDequeue;

        for (int i = 0; i < degreeOfParallelisation; i++)
            ThreadPool.QueueUserWorkItem(Loop);
    }

    public void Dispose()
    {
        CancellationTokenSource.CreateLinkedTokenSource(CancellationToken).Cancel();
        ResetEvent.Set();

        ResetEvent.Dispose();

        GC.SuppressFinalize(this);
    }

    public async Task Enqueue(T item)
    {
        var isComplete = false;
        Queue.Enqueue((item, () => isComplete = true));
        ResetEvent.Set();

        while (!isComplete)
            await Task.Delay(10);
    }

    private void Loop(object? stateInfo)
    {
        while (!CancellationToken.IsCancellationRequested)
        {
            ResetEvent.Wait();

            if (CancellationToken.IsCancellationRequested)
                return;

            if (!Queue.TryDequeue(out var item))
            {
                ResetEvent.Reset();
                continue;
            }

            OnDequeue(item.Value);
            item.IsComplete();
        }
    }

    private CancellationToken CancellationToken { get; } = new();
    private Action<T> OnDequeue { get; }
    private ConcurrentQueue<(T Value, Func<bool> IsComplete)> Queue { get; } = new();
    private ManualResetEventSlim ResetEvent { get; } = new(false);
}
