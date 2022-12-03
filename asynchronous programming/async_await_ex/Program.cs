// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

// AsyncAwaitExample asyn_ex = new AsyncAwaitExample();
// asyn_ex.RunTest();

// AsyncSuperSleepClass sc = new AsyncSuperSleepClass();
// Task mainTask = sc.RunTest();
// mainTask.Wait();

AsyncTaskCompletionSourceExample atcse = new AsyncTaskCompletionSourceExample();
atcse.RunTest();

internal class AsyncTaskCompletionSourceExample
{
    private Task WaitFor3Sec()
    {
        System.Console.WriteLine($"Running WaitFor3Sec in thread before delay : {Thread.CurrentThread.ManagedThreadId}" );
        TaskCompletionSource tcs = new TaskCompletionSource();
        Task task = tcs.Task;

        Thread th = new Thread( () =>
        {
            Thread.Sleep(3000);
            tcs.SetResult();
        });
        th.Start();
        return task;
    }

    private async Task WaitWrapper()
    {
     await WaitFor3Sec();
     System.Console.WriteLine("At the end of WaitWrapper");
    }

    public Task RunTest()
    {
        Stopwatch st = new Stopwatch();
        st.Start();

        //await WaitWrapper();
        Task tsk = WaitFor3Sec();
        System.Console.WriteLine("Before task wait");
        tsk.Wait();

        st.Stop();
        System.Console.WriteLine($"Running RunTest in thread at end {Thread.CurrentThread.ManagedThreadId}");
        System.Console.WriteLine($"Time taken for RunTest: {st.Elapsed}");
        return tsk;
    }
}

internal class AsyncSuperSleepClass
{
    private static async Task<string> AsyncSleep(int timout, int noOfTry)
    {
        for (int i = 0; i < noOfTry; i++)
        {
            System.Console.WriteLine($"Running AsyncSleep in thread before sleep {Thread.CurrentThread.ManagedThreadId}");
            await Task.Delay(timout);
            System.Console.WriteLine($"Running AsyncSleep in thread after sleep {Thread.CurrentThread.ManagedThreadId}");

        }
        return "I had a good sleep";
    }

    public async Task RunTest()
    {
        System.Console.WriteLine($"Running RunTest in thread at start {Thread.CurrentThread.ManagedThreadId}");
        Stopwatch st = new Stopwatch();
        st.Start();
        Task<string> t1 = AsyncSleep(3000, 1);
        Task<string>  t2 = AsyncSleep(3000, 1);
        Task<string> t3 = AsyncSleep(3000, 1);

        Task<string[]> combinedTask = Task.WhenAll<string>(t1, t2, t3);
        System.Console.WriteLine("before await combined task");
        await combinedTask;
        System.Console.WriteLine("after await combined task");

        //combinedTask.Wait();

        System.Console.WriteLine($"t1 result {t1.Result} & {combinedTask.Result[0]}");
        System.Console.WriteLine($"t2 result {t2.Result} & {combinedTask.Result[0]}");
        System.Console.WriteLine($"t3 result {t3.Result} & {combinedTask.Result[0]}");

        st.Stop();
        System.Console.WriteLine($"Running RunTest in thread at end {Thread.CurrentThread.ManagedThreadId}");
        System.Console.WriteLine($"Time taken for AsyncSleep RunTest: {st.Elapsed}");


    }
}

internal class AsyncAwaitExample
{
    private static async Task AsyncSleep(int timeout)
    {
        System.Console.WriteLine($"Running AsyncSleep in thread before sleep {Thread.CurrentThread.ManagedThreadId}");
        await Task.Delay(timeout);
        System.Console.WriteLine($"Running AsyncSleep in thread after sleep {Thread.CurrentThread.ManagedThreadId}");
    }
    public void RunTest()
    {
        Stopwatch st = new Stopwatch();
        st.Start();
        //await AsyncSleep(1000);
        Task delayTask = AsyncSleep(3000);
        delayTask.Wait();
        st.Stop();
        System.Console.WriteLine($"Time taken for AsyncSleep RunTest: {st.Elapsed}");
    }

}