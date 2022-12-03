namespace CalcLib
{
    public class Calc : ICalc
    {
        public double Add(double x, double y) => x + y;

        public double Divide(double x, double y) => y == 0 ? throw new DivideByZeroException() : x / y;

        public double Init() => 0;

        public double Multiply(double x, double y) => x * y;

        public double Negate(double x) => x * -1;

        public double OwnPercentage(double x) => x / 100;

        public double Percentage(double x, double y) => x * ((x * y) / 100);

        public double Subtract(double x, double y) => x - y;

        void IDisposable.Dispose()
        {
            GC.Collect();
        }
    }
}