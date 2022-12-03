public class DeadLockExample
{

    private Mutex lockA = new Mutex();
    private Mutex lockB = new Mutex();

    public void Thread1()
    {
        lockA.WaitOne();
        System.Console.WriteLine($"Thread1 {Thread.CurrentThread.Name} acquires LockA");
        Thread.Sleep(1000);
        lockB.WaitOne();
        System.Console.WriteLine($"Thread1 {Thread.CurrentThread.Name} acquires LockB");

    }

    public void Thread2()
    {
        lockB.WaitOne();
        System.Console.WriteLine($"Thread2 {Thread.CurrentThread.Name} acquires LockB");
        Thread.Sleep(1000);
        lockA.WaitOne();
        System.Console.WriteLine($"Thread2 {Thread.CurrentThread.Name} acquires LockA");

    }

    public void RunTest()
    {
        Thread th1 = new Thread( ()=> 
        {
            Thread1();
        });
        Thread th2 = new Thread( ()=>
        {
            Thread2();
        });

        th1.Start();
        th2.Start();

        th1.Join();
        th2.Join();
    }

}