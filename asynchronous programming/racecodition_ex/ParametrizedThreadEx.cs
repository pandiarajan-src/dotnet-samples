using System;
using System.Threading;

public delegate void ResultsCallBack(double results);

class GreetingsClass
{
    private string NameToGreet;
    ResultsCallBack resultsDelegate;
    private double variable;
    public GreetingsClass(string name, double variable, ResultsCallBack resDel )
    {
        NameToGreet = name;
        resultsDelegate = resDel;
        this.variable = variable;
    }

    public void Square()
    {
        double results = variable * variable;
        if(resultsDelegate)
        {
            resultsDelegate(results);
        }
    }
}

public class ParameterizedThreadExApp
{
    public static void RunTest()
    {
        GreetingsClass gc = new GreetingsClass("Pandi", 20, double(data) => 
        {
            System.Console.WriteLine($"The square results are : {data}");
        });
        Thread th1 = new Thread( new ThreadStart(gc.Square));
        th1.Start();
        th1.Join();
    }
}