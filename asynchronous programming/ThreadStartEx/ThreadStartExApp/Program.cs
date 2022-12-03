using System;
using System.Threading;

public class MainTest
{
    public static void Main()
    {
        //ParameterizedThreadExApp.RunTest();
        ThreadTest.RunTest();
    }
}

public class ThreadTest
{
    public static void RunTest()
    {
        Thread[] thr = new Thread[5];
        int i = 0; 
        int length = 5;
        for(i = 0; i < length; i++)
        {
            thr[i] = new Thread(() =>
            {
                System.Console.WriteLine(i);
            });
        }
        i = 9;
        for (int k = 0; k < length; k++)
        {
            thr[k].Start();
        }
        for (int k = 0; k < length; k++)
        {
            thr[k].Join();
        }        

    }

}

public delegate void ResultsCallBack(double results);

public class GreetingsClass
{
    ResultsCallBack resultsDelegate;
    private double variable;
    public GreetingsClass(double variable, ResultsCallBack resDel )
    {
        resultsDelegate = resDel;
        this.variable = variable;
    }

    public void Square()
    {
        double results = variable * variable;
        if(resultsDelegate != null)
        {
            resultsDelegate(results);
        }
    }
}

public class ParameterizedThreadExApp
{
    public static void RunTest()
    {
        GreetingsClass gc = new GreetingsClass(20, (double data) => 
        {
            System.Console.WriteLine($"The square results are : {data}");
        });
        ThreadStart thSt = new ThreadStart(gc.Square);
        Thread th1 = new Thread(thSt);
        th1.Start();
        th1.Join();
    }
}