using Utils.CSharp.Queues;

using var queue = new AsyncQueue<Action>(action => action(), degreeOfParallelisation: 16);

var random = new Random();

var tasks = Enumerable
    .Range(1, 100)
    .ToArray()
    .Select(count => queue.Enqueue(() =>
    {
        Thread.Sleep(random.Next(1000));
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
    }));

await Task.WhenAll(tasks);

Console.WriteLine("Finished");
