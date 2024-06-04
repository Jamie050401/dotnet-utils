using Utils.CSharp.Queues;

var queue = AsyncQueue<Action>.Create(5);

var random = new Random();
Enumerable
    .Range(1, 100)
    .ToList()
    .ForEach(count => queue.Enqueue(() =>
    {
        Thread.Sleep(random.Next(5000));
        Console.WriteLine($"Message {count} from queue! On thread {Environment.CurrentManagedThreadId}");
    }));

Console.WriteLine($"Press ENTER to close this window ...{Environment.NewLine}"); Console.ReadLine();
queue.Stop();
