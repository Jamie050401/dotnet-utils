using Utils.CSharp.Queues;

var random = new Random();

//using var queue = AsyncQueueFactory.Create<Func<Task>>(async action => await action(), degreeOfParallelisation: 1);

//var tasks = Enumerable
//    .Range(1, 100)
//    .Select(count => queue.Enqueue(async () =>
//    {
//        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
//        await Task.Delay(random.Next(1000));
//    }));

using var queue = AsyncQueueFactory.Create<Action>(action => action(), degreeOfParallelisation: 1);

var tasks = Enumerable
    .Range(1, 100)
    .Select(count => queue.Enqueue(() =>
    {
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
        Thread.Sleep(random.Next(1000));
    }));

await Task.WhenAll(tasks).ConfigureAwait(ConfigureAwaitOptions.None | ConfigureAwaitOptions.SuppressThrowing);

Console.WriteLine("Finished");
