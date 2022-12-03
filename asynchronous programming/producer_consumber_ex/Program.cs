// See https://aka.ms/new-console-template for more information
System.Console.WriteLine("Producer and Consumer example");

ExampleTest ex_test = new ExampleTest();
ex_test.Run();

internal class ExampleTest
{
    //private ProducerConsumerMutex prodconsex = new ProducerConsumerMutex();
    //private ProducerConsumerMonitor prodconsex = new ProducerConsumerMonitor();
    private ProducerConsumerSemaphore prodconsex = new ProducerConsumerSemaphore();
    int loopCount_max = 10;
    private void ProdThread()
    {
        int data = 1;
        int loopCount = 0;
        while(loopCount < loopCount_max)
        {
            prodconsex.enqueue((Object)data);
            //System.Console.WriteLine($"Thread with id {Thread.CurrentThread.ManagedThreadId} produced = {data}");
            data++;
            loopCount++;
        }
    }

    private void ConsThread()
    {
        int loopCount = 0;
        while(loopCount < loopCount_max)
        {
            //Thread.Sleep(100);
            Object data = prodconsex.dequeue();
            //System.Console.WriteLine($"Thread with id {Thread.CurrentThread.ManagedThreadId} consumed = {(data?.ToString())}");
            loopCount++;
        }
    }

    public void Run()
    {
        Thread prodThread1 = new Thread(() => {
            ProdThread();
        });
        prodThread1.Start();

        Thread prodThread2 = new Thread(() => {
            ProdThread();
        });
        prodThread2.Start();

        Thread consThread1 = new Thread(() => {
            ConsThread();
        });
        consThread1.Start();

        Thread consThread2 = new Thread(() => {
            ConsThread();
        });
        consThread2.Start();

        prodThread1.Join();
        prodThread2.Join();
        consThread1.Join();
        consThread2.Join();
    }
}