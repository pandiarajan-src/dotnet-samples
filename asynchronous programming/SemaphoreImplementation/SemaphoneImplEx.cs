internal class SemaphoreImplExample
{
    private int max_size;
    private Object padlock;
    private int alloted_size;

    public SemaphoreImplExample(int maxsize)
    {
        max_size = maxsize;
        padlock = new Object();
        alloted_size = 0;
    }

    public void acquire()
    {
        Monitor.Enter(padlock);
        while(max_size == alloted_size)
        {
            Monitor.Wait(padlock);
        }
        alloted_size++;
        Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }

    public void release()
    {
        Monitor.Enter(padlock);
        while(alloted_size == 0)
        {
            Monitor.Wait(padlock);
        }
        alloted_size--;
        Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }
}