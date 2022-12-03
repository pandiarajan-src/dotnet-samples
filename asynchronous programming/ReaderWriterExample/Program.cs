// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ReaderWriterExampleTest test = new ReaderWriterExampleTest();
test.RunTest();

internal class ReaderWriterExampleTest
{
    ReaderWriterExample rw = new ReaderWriterExample();
    volatile bool shutdown = false;

    private void WriterTest()
    {
        while(!shutdown)
        {
            rw.AcquireWriterLock();
            System.Console.WriteLine($"Acquiring writer lock in {Thread.CurrentThread.ManagedThreadId} n={rw.GetReadersCount()} b={rw.IsWriterInProgress()}");

            Thread.Sleep(Random.Shared.Next(1,3) * 1000);

            rw.ReleaseWriterLock();
            System.Console.WriteLine($"Releasing writer lock in {Thread.CurrentThread.ManagedThreadId} n={rw.GetReadersCount()} b={rw.IsWriterInProgress()}");
        }
    }

    private void ReaderTest()
    {
        while(!shutdown)
        {
            rw.AcquireReaderLock();
            System.Console.WriteLine($"Acquiring reader lock in {Thread.CurrentThread.ManagedThreadId} n={rw.GetReadersCount()} b={rw.IsWriterInProgress()}");

            Thread.Sleep(Random.Shared.Next(1,3) * 1000);
            
            rw.ReleaseReaderLock();
            System.Console.WriteLine($"Releasing reader lock in {Thread.CurrentThread.ManagedThreadId} n={rw.GetReadersCount()} b={rw.IsWriterInProgress()}");
            
        }
    }

    public void RunTest()
    {
        //Two writer thread and 5 Reader threads
        Thread wt1 = new Thread(new ThreadStart(WriterTest));
        Thread wt2 = new Thread(new ThreadStart(WriterTest));
       
        int length = 5;
        Thread[] rt = new Thread[length];
        for (int i = 0; i < length; i++)
        {
            rt[i] = new Thread(new ThreadStart(ReaderTest));
        }

        wt1.Start();
        for (int i = 0; i < length; i++)
        {
            rt[i].Start();
        }
        wt2.Start();        

        Thread.Sleep(10000);
        shutdown = true;

        wt1.Join();
        wt2.Join();
        for (int i = 0; i < length; i++)
        {
            rt[i].Join();
        }     

    }
}