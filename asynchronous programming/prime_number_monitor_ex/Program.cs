// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

PrimeNumberMonitorEx pn_test = new PrimeNumberMonitorEx();
pn_test.RunTest();

internal class PrimeNumberMonitorEx
{
    private int prime_val = 2;
    private bool found = false;
    volatile bool shutdown = false;
    private object lockObj = new Object();

    private bool isPrime(int input)
    {
        if(input == 2 || input == 3) return true;
        int div = 2;
        while(div <= input/2)
        {
            if(input%div == 0) return false;
            div++;
        }
        return true;
    }

    private void printer()
    {
        while(!shutdown)
        {
            Monitor.Enter(lockObj);
            while(!found && !shutdown)
            {
                Monitor.Wait(lockObj);
            }
            System.Console.WriteLine($"Found Prime number {prime_val}");
            found = false;
            Monitor.PulseAll(lockObj);
            Monitor.Exit(lockObj);
        }
    }

    private void finder()
    {
        while(!shutdown)
        {
            Monitor.Enter(lockObj);

            while(found && !shutdown){
                Monitor.Wait(lockObj);
            }

            if(isPrime(prime_val))
            {
                found = true;
                Monitor.PulseAll(lockObj);
            }
            prime_val++;
            
            Monitor.Exit(lockObj);            
        }
    }

    public void RunTest()
    {
        Thread th1 = new Thread( () => {
            finder();
        });
        Thread th2 = new Thread( () => {
            finder();
        });        
        Thread printer_th = new Thread( () => {
            printer();
        });

        th1.Start();
        th2.Start();
        printer_th.Start();


        Thread.Sleep(1000);
        shutdown = true;

        th1.Join();
        th2.Join();
        printer_th.Join();
    }
}