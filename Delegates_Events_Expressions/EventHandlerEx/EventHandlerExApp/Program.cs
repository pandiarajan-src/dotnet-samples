// See https://aka.ms/new-console-template for more information
using EventHandlerExLib;

public class Program
{
    static void Main()
    {
        Program prg = new Program();
        prg.DoWork();
    }

    public void DoWork()
    {
        Publisher pub = new Publisher();

        pub.myEvent += (s, e) =>
        {
            System.Console.WriteLine(s?.ToString());
            System.Console.WriteLine(e.a + e.b);
        };

        pub.RaiseEvent(this, new CustomeEventArgs(10, 20));
        pub.RaiseEvent(this, new CustomeEventArgs(100, 200));
        pub.RaiseEvent(this, new CustomeEventArgs(1000, 2000));
    }
}
