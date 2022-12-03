internal class ReaderWriterExample
{
    private bool isWriteInProgress;
    private int nReaders;
    private Object padlock;

    public ReaderWriterExample()
    {
        nReaders = 0;
        isWriteInProgress = false;
        padlock = new Object();
    }

    public void AcquireReaderLock()
    {
        Monitor.Enter(padlock);
        while(isWriteInProgress)
        {
            Monitor.Wait(padlock);
        }
        nReaders++;
        Monitor.Exit(padlock);
    }

    public void ReleaseReaderLock()
    {
        Monitor.Enter(padlock);
        nReaders--;
        //This is not requied, but there is a possibility of reducing number of pulseall
        if(nReaders == 0)
        {
            Monitor.PulseAll(padlock);
        }
        //Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }

    public void AcquireWriterLock()
    {
        Monitor.Enter(padlock);
        while(nReaders != 0 && isWriteInProgress)
        {
            Monitor.Wait(padlock);
        }
        isWriteInProgress = true;
        Monitor.Exit(padlock);
    }
    public void ReleaseWriterLock()
    {
        Monitor.Enter(padlock);
        isWriteInProgress = false;
        Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }
    public bool IsWriterInProgress()
    {
        return isWriteInProgress;
    }

    public int GetReadersCount()
    {
        return nReaders;
    }
}