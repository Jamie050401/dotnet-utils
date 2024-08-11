using System.Collections.Concurrent;
using Utils.CSharp.Extensions;

namespace Utils.CSharp.Queues;

public static class AsyncQueueFactory
{
    public static AsyncQueue<Action> CreateAutomaticActionQueue(int threadCount = 1) => new(action => action(), threadCount);
    public static AsyncQueue<Action> CreateManualActionQueue(int threadCount = 1) => new(threadCount);
}

public class AsyncQueue<T>
{
    internal AsyncQueue(int threadCount = 1)
    {
        if (threadCount < 1)
            throw new ArgumentException($"{nameof(threadCount)} cannot be less than one.");

        ProcessingSemaphore = new(threadCount);
        ThreadCount = threadCount;
    }

    internal AsyncQueue(Action<T> action, int threadCount = 1)
    {
        if (threadCount < 1)
            throw new ArgumentException($"{nameof(threadCount)} cannot be less than one.");

        Action = action;
        IsStopped = false;
        ProcessingSemaphore = new(threadCount);
        ThreadCount = threadCount;

        Enumerable
            .Range(1, threadCount)
            .ForEach(_ => Enumerate(action));
    }

    public void Enqueue(T item)
    {
        Queue.Enqueue(item);

        if (!EmptyWaitHandle.IsSet)
            EmptyWaitHandle.Set();
    }

    public void Restart()
    {
        if (!Token.IsCancellationRequested && Action is not null)
            Start(Action);
    }

    public void Start(Action<T> action)
    {
        if (!IsStopped)
            return;

        Action = action;
        IsStopped = false;
        Token = new();

        Enumerable
            .Range(1, ThreadCount)
            .ForEach(_ => Enumerate(action));
    }

    public void Stop()
    {
        IsStopped = true;
        CancellationTokenSource.CreateLinkedTokenSource(Token).Cancel(); // TODO: Need to ensure the cancellation doesn't loose items from the queue if it is mid processing
    }

    private void Enumerate(Action<T> action)
    {
        async Task Enumerate()
        {
            await ProcessingSemaphore.WaitAsync();

            try
            {
                while (true)
                {
                    EmptyWaitHandle.Wait();

                    if (Queue.TryDequeue(out var item))
                    {
                        action(item);

                        if (IsEmpty)
                        {
                            EmptyWaitHandle.Reset();
                        }
                    }
                }
            }
            finally
            {
                ProcessingSemaphore.Release();
            }
        }

        Task.Run(() => Enumerate(), Token);
    }

    public bool IsEmpty { get => Queue.IsEmpty; }
    private Action<T>? Action { get; set; } // This action defines the logic to be applied to each element in the queue.
    private ManualResetEventSlim EmptyWaitHandle { get; } = new(); // This manual reset event is used to halt the processing loop whenever the queue is empty.
    private bool IsStopped { get; set; } = true;
    private SemaphoreSlim ProcessingSemaphore { get; } // This semaphore is used to restrict the number of threads that can be processing the queue at any given moment.
    private ConcurrentQueue<T> Queue { get; } = new();
    private int ThreadCount { get; }
    private CancellationToken Token { get; set; } = new();
}
