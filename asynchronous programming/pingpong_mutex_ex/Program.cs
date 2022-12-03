// See https://aka.ms/new-console-template for more information
using System.Diagnostics;

Console.WriteLine("Hello, World!");
//MutexPingPongTest pptest = new MutexPingPongTest();
//pptest.RunTest();

//SemaphonePingPongTest ppTest = new SemaphonePingPongTest();
//ppTest.RunTest();

// InterlockedPingPongTest ppTest = new InterlockedPingPongTest();
// ppTest.RunTest();

// EventWaitHandleExample evTest = new EventWaitHandleExample();
// evTest.RunTest();

// MonitorPingPongExample ppTest = new MonitorPingPongExample();
// ppTest.RunTest();

// lockPingPongExample ppTest = new lockPingPongExample();
// ppTest.RunTest();

SpinWaitPingPongExample ppTest = new SpinWaitPingPongExample();
ppTest.RunTest();

public class SpinLockPingPongExample
{
    private volatile bool flag = false;
    private volatile bool shutdown = false;
    private SpinLock sl = new SpinLock();
        private void Ping()
    {
        while(!shutdown)
        {
            bool lockTaken = false;
            sl.Enter(ref lockTaken);
            while(flag)
            {
                sl.Exit();
                lockTaken = false;
                sl.Enter(ref lockTaken);
            }
            System.Console.WriteLine("Ping");
            flag = true;
            sl.Exit();
        }

    }

    private void Pong()
    {
        while(!shutdown)
        {
            bool lockTaken = false;
            sl.Enter(ref lockTaken);
            while(!flag)
            {
                sl.Exit();
                lockTaken = false;
                sl.Enter(ref lockTaken);
            }
            System.Console.WriteLine("Pong");
            flag = false;
            sl.Exit();
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread(()=>{
            Ping();
        });

        Thread th2 = new Thread(()=>{
            Pong();
        });

        th1.Start();
        th2.Start();

        Thread.Sleep(1000);
        shutdown = true;
        
        th1.Join();
        th2.Join();
    }

}

public class SpinWaitPingPongExample
{
    private volatile bool flag = false;
    private volatile bool shutdown = false;
    private void Ping()
    {
        while(!shutdown)
        {
            SpinWait sw = new SpinWait();
            while(flag)
            {
                sw.SpinOnce();
            }
            flag = true;
            System.Console.WriteLine("Ping");
        }
    }

    private void Pong()
    {
        while(!shutdown)
        {
            SpinWait sw = new SpinWait();
            while(!flag)
            {
                sw.SpinOnce();
            }
            flag = false;
            System.Console.WriteLine("Pong");
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread(()=>{
            Ping();
        });

        Thread th2 = new Thread(()=>{
            Pong();
        });

        th1.Start();
        th2.Start();

        Thread.Sleep(1000);
        shutdown = true;
        
        th1.Join();
        th2.Join();
    }

}

public class SpingUntilPingPongExample
{
    private volatile bool flag = false;
    private volatile bool shutdown = false;
    private void Ping()
    {
        while(!shutdown)
        {
            SpinWait.SpinUntil(()=>{return !flag;});
            System.Console.WriteLine("Ping");
            flag = true;
        }
    }

    private void Pong()
    {
        while(!shutdown)
        {
            SpinWait.SpinUntil(()=>{return flag;});
            System.Console.WriteLine("Pong");
            flag = false;
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread(()=>{
            Ping();
        });

        Thread th2 = new Thread(()=>{
            Pong();
        });

        th1.Start();
        th2.Start();

        Thread.Sleep(1000);
        shutdown = true;
        
        th1.Join();
        th2.Join();
    }

}

public class lockPingPongExample
{
    private bool flag = false;
    private volatile bool shutdown = false;
    private Object lockObj = new Object();
    private void Ping()
    {
        while(!shutdown)
        {
            lock(lockObj)
            {
                while(flag)
                {
                    Monitor.Wait(lockObj);
                }
                flag = true;
                System.Console.WriteLine("Ping");
                Monitor.Pulse(lockObj);
            }
        }
    }

    private void Pong()
    {
        while(!shutdown)
        {
            lock(lockObj)
            {
                while(!flag)
                {
                    Monitor.Wait(lockObj);
                }
                flag = false;
                System.Console.WriteLine("Pong");
                Monitor.Pulse(lockObj);
            }
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread(()=>{
            Ping();
        });

        Thread th2 = new Thread(()=>{
            Pong();
        });

        th1.Start();
        th2.Start();

        Thread.Sleep(1000);
        shutdown = true;
        
        th1.Join();
        th2.Join();
    }
}

public class MonitorPingPongExample
{
    private bool flag = false;
    volatile bool shutdown = false;
    private Object lockObj = new Object();

    private void Ping()
    {
        while(!shutdown)
        {
            Monitor.Enter(lockObj);
            while(flag)
            {
                Monitor.Wait(lockObj);
            }
            flag = true;
            System.Console.WriteLine("Ping");
            Monitor.Pulse(lockObj);
            Monitor.Exit(lockObj);
        }
    }
    private void Pong()
    {
        while(!shutdown)
        {
            Monitor.Enter(lockObj);
            while(!flag)
            {
                Monitor.Wait(lockObj);
            }
            flag = false;
            System.Console.WriteLine("Pong");
            Monitor.Pulse(lockObj);
            Monitor.Exit(lockObj);
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread(() => {
            Ping();
        });
        Thread th2 = new Thread( () =>
        {
            Pong();
        });

        th1.Start();
        th2.Start();

        Thread.Sleep(10000000);
        shutdown = true;

        th1.Join();
        th2.Join();
    }
}

public class EventWaitHandleExample
{
    private static EventWaitHandle ewh = new EventWaitHandle(false, EventResetMode.ManualReset);
    private static EventWaitHandle done = new EventWaitHandle(false, EventResetMode.AutoReset);

    private void wait(int value)
    {
        ewh.WaitOne();
        System.Console.WriteLine($"{value * value}");
        done.Set();
    }

    public void RunTest()
    {
        Thread th1 = new Thread( () => {
            wait(5);
        });
        Thread th2 = new Thread(() => {
            wait(10);
        });

        th1.Start();
        th2.Start();
        
        Thread.Sleep(10000);
        WaitHandle.SignalAndWait(ewh, done);

        th1.Join();
        th2.Join();
    
    }

}

public class InterlockedPingPongTest
{
    private long flag = 0;

    private void Ping()
    {
        while(true)
        {
            while(Interlocked.Read(ref flag) == 1) {}
            System.Console.WriteLine("Ping");
            Interlocked.Exchange(ref flag, 1);
        }
    }

    private void Pong()
    {
        while(true)
        {
            while(Interlocked.Read(ref flag) == 0 ){}
            System.Console.WriteLine("Pong");
            Interlocked.Exchange(ref flag, 0);
        }
    }
    public void RunTest()
    {
        Thread th1 = new Thread( () => {
            Ping();
        });
        Thread th2 = new Thread( () => {
            Pong();
        });

        th1.Start();
        th2.Start();
        Thread.Sleep(100000);
    }
}

public class SemaphonePingPongTest
{
    private Semaphore sem1 = new Semaphore(0,1);
    private Semaphore sem2 = new Semaphore(0,1);

    private void Ping()
    {
        while(true)
        {
            sem2.Release();
            sem1.WaitOne();
            System.Console.WriteLine("Ping");
        }
    }

    private void Pong()
    {
        while(true)
        {
            sem2.WaitOne();
            System.Console.WriteLine("Pong");  
            sem1.Release(); 
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread( () => {
            Ping();
        });

        Thread th2 = new Thread( () =>
        {
            Pong();
        });

        th1.Start();
        th2.Start();

        th1.Join();
        th2.Join();
    }

}

public class MutexPingPongTest
{
    Mutex mutex_obj = new Mutex();
    bool flag = false; //Shared flag
    int index = 0;
    int loopCount = 10;
    private void Ping()
    {
        try{
            while(true)
            {
                mutex_obj.WaitOne();
                while(flag == true)
                {
                    mutex_obj.ReleaseMutex();
                    mutex_obj.WaitOne();
                }
                flag = true;
                System.Console.WriteLine("Ping");
                if(index == loopCount ){break;}
                mutex_obj.ReleaseMutex();
            }
        }
        catch(AbandonedMutexException)
        {
            System.Console.WriteLine("Its ok we are done from Ping");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }

    private void Pong()
    {
        try{
            while(true)
            {
                mutex_obj.WaitOne();
                while(flag == false)
                {
                    mutex_obj.ReleaseMutex();
                    mutex_obj.WaitOne();
                }
                flag = false;
                index++;
                System.Console.WriteLine("Pong");
                if(index == loopCount ){break;}
                mutex_obj.ReleaseMutex();
            }
        }
        catch(AbandonedMutexException)
        {
            System.Console.WriteLine("Its ok we are done from Pong");
        }
        catch(Exception ex)
        {
            System.Console.WriteLine(ex.Message);
        }
    }
    public void RunTest()
    {
        Stopwatch watch = new Stopwatch();
        watch.Start();
        Thread th1 = new Thread( () =>
        {
            Ping();
        });
        Thread th2 = new Thread ( () =>
        {
            Pong();
        });
        th1.IsBackground = true;
        th2.IsBackground = true;

        th1.Start();
        th2.Start();

        //Thread.Sleep(TimeSpan.FromMilliseconds(1000000));
        th1.Join();
        th2.Join();
        watch.Stop();
        System.Console.WriteLine($"Performance : {watch.Elapsed}");
    }
}