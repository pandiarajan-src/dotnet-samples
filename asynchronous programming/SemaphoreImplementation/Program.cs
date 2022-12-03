// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

SemaphoreImplTest test = new SemaphoreImplTest();
test.RunTest();

internal class SemaphoreImplTest
{
    SemaphoreImplExample sem = new SemaphoreImplExample(1);
    private void task1()
    {
        System.Console.WriteLine($"Acquiring-0 {DateTime.Now.ToLongTimeString()}");
        sem.acquire();

        System.Console.WriteLine($"Acquiring-1 {DateTime.Now.ToLongTimeString()}");
        sem.acquire();

        System.Console.WriteLine($"Acquiring-2 {DateTime.Now.ToLongTimeString()}");
        sem.acquire();
    }

    private void task2()
    {
        Thread.Sleep(2000);
        System.Console.WriteLine($"Releasing-0 {DateTime.Now.ToLongTimeString()}");
        sem.release();

        Thread.Sleep(2000);
        System.Console.WriteLine($"Releasing-1 {DateTime.Now.ToLongTimeString()}");
        sem.release();

        Thread.Sleep(2000);
        System.Console.WriteLine($"Releasing-2 {DateTime.Now.ToLongTimeString()}");
        sem.release();
    }

    public void RunTest()
    {
        Thread th1 = new Thread(() => {task1();});
        Thread th2 = new Thread(() => {task2();});
        th1.Start();
        Thread.Sleep(1000);
        th2.Start();
        th1.Join();
        th2.Join();
    }
}