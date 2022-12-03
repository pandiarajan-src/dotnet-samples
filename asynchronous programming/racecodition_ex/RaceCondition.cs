public class RaceConditionExample
{
    public int randVal = 0;
    Random rand = new Random();
    Mutex mutex = new Mutex();

    public void printer()
    {
        long index = 1000000;
        while(index > 0)
        {
            mutex.WaitOne();
            if(randVal % 5 == 0)
            {
                if(randVal % 5 == 0)
                System.Console.WriteLine($"{randVal}");

            }
            mutex.ReleaseMutex();
            index--;
        }
    }

    public void Modifier()
    {
        long i = 1000000;
        while(i > 0)
        {
            mutex.WaitOne();
            randVal = rand.Next(5000);
            mutex.ReleaseMutex();
            i--;
        }
    }

    public void RunTest()
    {
        Thread PrinterThread = new Thread( new ThreadStart(printer));
        Thread ModifierThread = new Thread(new ThreadStart(Modifier));

        PrinterThread.Start();
        ModifierThread.Start();

        PrinterThread.Join();
        ModifierThread.Join();

    }
}
