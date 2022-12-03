// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

TokenBucketFilterTest test = new TokenBucketFilterTest();
//test.RunTest();
test.RunMultipleThreadOnceTest();

internal class TokenBucketFilterTest
{
    //TokenBuketFilterNaive test_obj = new TokenBuketFilterNaive(10);
    TokenBucketFilterAlt test_obj = new TokenBucketFilterAlt(10);

    public void RunMultipleThreadOnceTest()
    {
        int length = 10;
        Thread[] threads = new Thread[length];
        for (int index = 0; index < length; index++)
        {
            threads[index] = new Thread(new ThreadStart(test_obj.GetToken));
        }
        for (int index = 0; index < length; index++)
        {
            threads[index].Start();
        }
        for (int index = 0; index < length; index++)
        {
            threads[index].Join();
        }                

    }

    public void RunTest()
    {
        Thread th1 = new Thread(() => 
        {
            Int64? val;
            int loopCount = 10;
            while(loopCount > 0)
            {
                val = (System.Int64)(test_obj.GetToken());
                System.Console.WriteLine($"GetToken Return {val} on Thread {Thread.CurrentThread.ManagedThreadId}");
                loopCount--;
            }
        });
        th1.Start();

        Thread th2 = new Thread(() => 
        {
            Int64? val;
            int loopCount = 10;
            while(loopCount > 0)
            {
                val = (System.Int64)(test_obj.GetToken());
                System.Console.WriteLine($"GetToken Return {val} on Thread {Thread.CurrentThread.ManagedThreadId}");
                loopCount--;                
            } 
        });
        th2.Start();

        Thread.Sleep(1000);
        //test_obj.Dispose();
        th1.Join();
        th2.Join();
    }

}