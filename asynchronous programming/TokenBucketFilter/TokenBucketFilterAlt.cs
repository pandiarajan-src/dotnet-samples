internal class TokenBucketFilterAlt
{
    private int max_size;
    private long lastRequestedTime;
    private int possible_tokens;
    private Mutex objLock;

    public TokenBucketFilterAlt(int max_size)
    {
        possible_tokens = 0;
        this.max_size = max_size;
        objLock = new Mutex();
        lastRequestedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
    }

    public Object GetToken()
    {
        objLock.WaitOne();
        possible_tokens +=  (int)((DateTimeOffset.Now.ToUnixTimeSeconds() - lastRequestedTime));
        if(possible_tokens == 0)
            Thread.Sleep(1000);
        else if(possible_tokens > 0)
            possible_tokens--;
        else if(possible_tokens > max_size)
            possible_tokens = max_size;
        lastRequestedTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        var val = (Object)Random.Shared.NextInt64();
        objLock.ReleaseMutex();
        return val;
    }
}