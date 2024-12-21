using System.Collections.Concurrent;

namespace Utils.CSharp.Queues;

public class AsyncQueue<T> : IDisposable where T : notnull
{
    public AsyncQueue(Action<T> onDequeue, int degreeOfParallelisation = 1)
    {
        OnDequeue = onDequeue;
        Semaphore = new(degreeOfParallelisation);

        for (int i = 0; i <degreeOfParallelisation; i++)
            ThreadPool.QueueUserWorkItem(Loop);
    }

    public void Dispose()
    {
        CancellationTokenSource.CreateLinkedTokenSource(CancellationToken).Cancel();
        ResetEvent.Set();

        Thread.Sleep(100);
        Semaphore.Dispose();
        ResetEvent.Dispose();

        GC.SuppressFinalize(this);
    }

    public async Task Enqueue(T item)
    {
        var itemResetEvent = new ManualResetEventSlim(false);
        Queue.Enqueue((item, itemResetEvent));
        ResetEvent.Set();
        await Task.Run(() =>
        {
            itemResetEvent.Wait();
            itemResetEvent.Dispose();
        });
    }

    private void Loop(object? stateInfo)
    {
        while (!CancellationToken.IsCancellationRequested)
        {
            ResetEvent.Wait();

            if (CancellationToken.IsCancellationRequested)
                return;

            if (!TryDequeue())
                ResetEvent.Reset();
        }
    }

    private bool TryDequeue()
    {
        Semaphore.Wait();

        try
        {
            var foundItem = false;

            while (Queue.TryDequeue(out var tuple))
            {
                foundItem = true;
                OnDequeue(tuple.Item);
                tuple.Event.Set();
            }

            return foundItem;
        }
        finally
        {
            Semaphore.Release();
        }
    }

    private CancellationToken CancellationToken { get; } = new();
    private Action<T> OnDequeue { get; }
    private ConcurrentQueue<(T Item, ManualResetEventSlim Event)> Queue { get; } = new();
    private ManualResetEventSlim ResetEvent { get; } = new(false);
    private SemaphoreSlim Semaphore { get; }
}
