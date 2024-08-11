using System.Collections.Concurrent;
using Utils.CSharp.Extensions;

namespace Utils.CSharp.Queues;

public class AsyncQueue<T>
{
    private AsyncQueue(Action<T> action, int threadCount = 1)
    {
        if (threadCount < 1)
            throw new ArgumentException($"{nameof(threadCount)} cannot be less than one.");

        Action = action;
        ThreadCount = threadCount;

        ProcessingSemaphore = new(threadCount);

        Enumerable
            .Range(1, threadCount)
            .ForEach(_ => Enumerate(action));
    }

    public static AsyncQueue<Action> Create(int threadCount = 1) => new(action => action(), threadCount);

    public void Enqueue(T item)
    {
        Queue.Enqueue(item);

        if (!EmptyWaitHandle.IsSet)
            EmptyWaitHandle.Set();
    }

    public void Restart()
    {
        if (!Token.IsCancellationRequested)
            return;

        Token = new();

        Enumerable
            .Range(1, ThreadCount)
            .ForEach(_ => Enumerate(Action));
    }

    public void Stop()
    {
        CancellationTokenSource.CreateLinkedTokenSource(Token).Cancel();
    }

    private void Enumerate(Action<T> action)
    {
        async Task Enumerate(Action<T> action)
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

        Task.Run(() => Enumerate(action), Token);
    }

    public bool IsEmpty { get => Queue.IsEmpty; }
    private CancellationToken Token { get; set; } = new();
    private ConcurrentQueue<T> Queue { get; } = new();
    private Action<T> Action { get; } // This action defines the logic to be applied to each element in the queue.
    private SemaphoreSlim ProcessingSemaphore { get; } // This semaphore is used to restrict the number of threads that can be processing the queue at any given moment.
    private ManualResetEventSlim EmptyWaitHandle { get; } = new(); // This manual reset event is used to halt the processing loop whenever the queue is empty.
    private int ThreadCount { get; }
}
