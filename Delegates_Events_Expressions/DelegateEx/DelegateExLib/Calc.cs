namespace DelegateExLib
{
    public class Calc
    {
        public double add(double a, double b)
        {
            double result = a + b;
            Console.WriteLine(result);
            return result;
        }

        public double multiply(double a, double b)
        {
            double result = a * b;
            Console.WriteLine(result);
            return result;
        }

        public static double static_subtraction(double a, double b)
        {
            double result = a - b;
            Console.WriteLine(result);
            return result;
        }
    }
}
