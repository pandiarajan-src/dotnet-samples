// See https://aka.ms/new-console-template for more information
Console.WriteLine("Tasks example program!");

//SimpleTasksExample.RunSimpleTest();
//SimpleTasksExample.RunFactoryTest();
SimpleTasksExample.RunTaskContinuations();

internal class SimpleTasksExample
{

    public static void RunTaskContinuations()
    {
         Task t1 = Task.Factory.StartNew((Object input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl1 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            System.Console.WriteLine($"HEllo {input}");
        }, "Pandi");

        Task t2 = t1.ContinueWith((input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl2 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            System.Console.WriteLine($"HEllo");
        });

        Task<string>.WaitAll(t2);     
    }


    public static void RunFactoryTest()
    {
         Task<string> t1 = Task<string>.Factory.StartNew((input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl1 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            System.Console.WriteLine($"HEllo {input}");
            return "Hello " + input;
        }, "Pandi1", TaskCreationOptions.AttachedToParent);

        Task<string> t2 = Task<string>.Factory.StartNew((input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl2 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            System.Console.WriteLine($"HEllo {input}");
            return $"Hello {input}";
        }, "Pandi2", TaskCreationOptions.None);

        Task<string>.WaitAll(t1, t2);     
    }

    public static void RunSimpleTest()
    {
        Task<string> t1 = new Task<string>((input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl1 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            return "Hello " + input;
        }, "Pandi1", TaskCreationOptions.AttachedToParent);

        Task<string> t2 = new Task<string>((input) => {
            Thread.Sleep(1000);
            System.Console.WriteLine($"Impl2 : This message is from thread id {Thread.CurrentThread.ManagedThreadId}");
            //System.Console.WriteLine($"HEllo {input}");
            return $"Hello {input}";
        }, "Pandi2", TaskCreationOptions.None);

        t1.Start(); t2.Start();
        System.Console.WriteLine($"{t1.Result}");
        System.Console.WriteLine($"{t2.Result}");
    }

}