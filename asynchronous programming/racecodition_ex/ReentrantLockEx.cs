public class ReentrantLockEx
{
    public static void RunTest()
    {
        Mutex mutex = new Mutex();
        
        mutex.WaitOne();
        System.Console.WriteLine("Acquired once");

        mutex.WaitOne();
        System.Console.WriteLine("Acquired twice");
    }
}