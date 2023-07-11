PrintHelp();

List<string> list1 = new List<string>();
List<string> list2 = new List<string>();
List<string> list3 = new List<string>();
List<string> list4 = new List<string>();
CancellationTokenSource cts = new CancellationTokenSource();

void AddStringsToList(List<string> list)
{
    while (true)
    {
        if (cts.Token.IsCancellationRequested)
            break;
        
        list.Add($"String added at {DateTime.Now}");
    }
}

void PrintHelp()
{
    Console.WriteLine("=== Application Help ===");
    Console.WriteLine("Available commands:");
    Console.WriteLine("start    - Start adding strings to the lists.");
    Console.WriteLine("stop     - Stop adding strings to the lists and exit the application.");
    Console.WriteLine("continue - The tasks are already running.");
    Console.WriteLine("clear    - Clear all the lists.");
    Console.WriteLine("gc       - Call the garbage collector.");
    Console.WriteLine("help     - Display this help message.");
    Console.WriteLine("========================");
}

var isStarted = false;
var tasks = new List<Task>();

while (true)
{
    string? input = Console.ReadLine();

    if (input == "start")
    {
        if (isStarted)
        {
            Console.WriteLine("The tasks are already running.");
            continue;
        }

        cts = new CancellationTokenSource();
        tasks.Add(Task.Run(() => AddStringsToList(list1), cts.Token));
        tasks.Add(Task.Run(() => AddStringsToList(list2), cts.Token));
        tasks.Add(Task.Run(() => AddStringsToList(list3), cts.Token));
        tasks.Add(Task.Run(() => AddStringsToList(list4), cts.Token));

        isStarted = true;
        Console.WriteLine("Tasks started.");
    }
    else if (input == "stop")
    {
        if (!isStarted)
        {
            Console.WriteLine("The tasks are not running.");
            continue;
        }

        cts.Cancel();
        await Task.WhenAll(tasks);
        isStarted = false;
        Console.WriteLine("Tasks stopped.");
    }
    else if (input == "clear")
    {
        list1.Clear();
        list1.TrimExcess();
        
        list2.Clear();
        list2.TrimExcess();
        
        list3.Clear();
        list3.TrimExcess();
        
        list4.Clear();
        list4.TrimExcess();
        
        Console.WriteLine("Lists cleared.");
    }
    else if (input == "gc")
    {
        GC.Collect();
        GC.WaitForPendingFinalizers();
        Console.WriteLine("Garbage collector called.");
    }
    else if (input == "exit")
    {
        break;
    }
    else if (input == "help")
    {
        PrintHelp();
    }
}

Console.WriteLine("Testing has been finished. Press any key to exit ...");
