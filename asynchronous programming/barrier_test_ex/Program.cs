// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

BarrierTestExample barrier = new BarrierTestExample();
barrier.RunTest();

internal class BarrierTestExample
{
    private Barrier barrier = new Barrier(3, (Barrier barrier) => {
        System.Console.WriteLine("All Threads arrived at the barrier");
    });

    private void work()
    {
        barrier.SignalAndWait();
        System.Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} released");
        Thread.Sleep(1000);

        barrier.SignalAndWait();
        System.Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} released ");
        Thread.Sleep(1000);

        barrier.SignalAndWait();
        System.Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} reached");
    }

    public void RunTest()
    {
        const int length = 3;
        Thread[] threads = new Thread[length];
        for (int i = 0; i < length; i++)
        {
            threads[i] = new Thread(() => {
                work();
            });
        }

        for (int i = 0; i < length; i++)
            threads[i].Start();

        for (int i = 0; i < length; i++)
            threads[i].Join();
    }

}