namespace SingleCastDelegateLib;

public delegate double MyCalcAddDelegate(double a, double b);

public class Calc
{
    public double Add(double a, double b)
    {
        return a + b;
    }
}

