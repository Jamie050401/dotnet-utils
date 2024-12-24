using Utils.CSharp.Queues;

using var queue = AsyncQueueFactory.Create<Func<Task>>(async action => await action(), degreeOfParallelisation: 1);

var random = new Random();

var tasks = Enumerable
    .Range(1, 100)
    .ToArray()
    .Select(count => queue.Enqueue(async () =>
    {
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
        await Task.Delay(random.Next(1000));
    }));

await Task.WhenAll(tasks);

Console.WriteLine("Finished");
