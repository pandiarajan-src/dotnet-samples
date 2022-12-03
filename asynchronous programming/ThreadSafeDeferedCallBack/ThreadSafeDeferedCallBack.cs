internal class DeferedAction : IComparable<DeferedAction>
{
    public string Message{get; set;}
    public long ExecutesAt{get; set;}
    public DeferedAction()
    {
        Message = "";
        ExecutesAt = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public void Execute()
    {
        System.Console.WriteLine($"Executing {Message} at {ExecutesAt}");
    }
    public int CompareTo(DeferedAction? destAction)
    {
        long val = 0L;
        if(destAction is not null)
        {
            val = (long)destAction.ExecutesAt;
        }
        return (int)(this.ExecutesAt - val);
    }
}

internal class ThreadSafeDeferedCallBackExecutor
{
    private PriorityQueue<DeferedAction, long> queue = new PriorityQueue<DeferedAction, long>();
    private Object padlock = new Object();

    public void Start()
    {
        while(true)
        {
            Monitor.Enter(padlock);

            while(queue.Count == 0)
            {
                Monitor.Wait(padlock);
            }
            while(queue.Count > 0)
            {
                DeferedAction dact = queue.Peek();
                int time2sleep = (int)(dact.ExecutesAt - DateTimeOffset.Now.ToUnixTimeSeconds());
                if(time2sleep > 0)
                    Monitor.Wait(padlock, time2sleep);
                else
                    break;
            }
            DeferedAction act = queue.Dequeue();
            act.Execute();
            Monitor.PulseAll(padlock);
            Monitor.Exit(padlock);
        }
    }

    public void registerCallback(DeferedAction request)
    {
        DeferedAction act = (DeferedAction)request;
        if( act.ExecutesAt == 0L)
        {
            //If execution time is not set then run this after 5 sec of gettin this call.
            act.ExecutesAt = DateTimeOffset.Now.ToUnixTimeSeconds() + DateTimeOffset.Now.AddSeconds(5).ToUnixTimeSeconds();
        }
        Monitor.Enter(padlock);
        queue.Enqueue(request, act.ExecutesAt);
        Monitor.Pulse(padlock);
        Monitor.Exit(padlock);
    }
}