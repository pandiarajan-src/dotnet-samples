using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalcLib
{

    public  interface ICalc : IDisposable
    {
        public double Init();
        public double Add(double x, double y);
        public double Subtract(double x, double y);
        public double Multiply(double x, double y);
        public double Divide(double x, double y);
        public double Percentage(double x, double y);
        public double OwnPercentage(double x);
        public double Negate(double x);
    }
}
