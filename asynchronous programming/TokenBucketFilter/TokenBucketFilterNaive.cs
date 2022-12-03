
internal class TokenBuketFilterNaive : IDisposable
{
    private int max_size;
    private Stack<Object> bucket;
    CancellationTokenSource cts;
    Thread th1;
    public TokenBuketFilterNaive(int size)
    {
        max_size = size;
        bucket = new Stack<Object>();
        cts = new CancellationTokenSource();
        th1 = new Thread(() => {
            while(true)
            {
                if(cts.IsCancellationRequested)
                {
                    break;
                }
                Thread.Sleep(1000);
                Monitor.Enter(this);
                if(bucket.Count < size)
                {
                    bucket.Push((Object)(Random.Shared.NextInt64()));
                    System.Console.WriteLine($"Pushing value {DateTime.Now}");
                }
                Monitor.PulseAll(this);
                Monitor.Exit(this);
            }
        });
        th1.Start();
    }

    public void Dispose()
    {
        cts.Cancel();
        cts.Dispose();
    }

    public Object GetToken()
    {
        Monitor.Enter(this);
        while(bucket.Count == 0 && !cts.IsCancellationRequested)
        {
            System.Console.WriteLine("Waiting since count is zero");
            Monitor.Wait(this);
        }
        Object val;
        try{
            val = bucket.Pop();
        }
        catch(InvalidOperationException)
        {
            val = 0L;
        }
        System.Console.WriteLine($"Poping value at {DateTime.Now}");
        Monitor.PulseAll(this);
        Monitor.Exit(this);
        return (Object)val;
    }

}