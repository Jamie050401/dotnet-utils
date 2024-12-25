using Utils.CSharp.Queues;

var random = new Random();

using var queueOne = new AsyncQueue<Func<Task>>(async action => await action(), degreeOfParallelisation: 1);

var tasksOne = Enumerable
    .Range(1, 30)
    .Select(count => queueOne.Enqueue(async () =>
    {
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
        await Task.Delay(random.Next(100));
    }));

await Task.WhenAll(tasksOne);

Console.WriteLine($"{Environment.NewLine}Finished processing queue one.{Environment.NewLine}");

using var queueTwo = new AsyncQueue<Action>(action => action(), degreeOfParallelisation: 1);

var tasksTwo = Enumerable
    .Range(1, 30)
    .Select(count => queueTwo.Enqueue(() =>
    {
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
        Thread.Sleep(random.Next(100));
    }));

await Task.WhenAll(tasksTwo);

Console.WriteLine($"{Environment.NewLine}Finished processing queue two.");
