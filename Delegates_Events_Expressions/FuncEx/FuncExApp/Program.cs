// See https://aka.ms/new-console-template for more information
using FuncLibEx;

Publisher pub = new Publisher();

pub.myEvent += (a, b) =>
{
    int c = a + b;
    return c;
};

System.Console.WriteLine(pub.RaiseEvent(10, 20));
System.Console.WriteLine(pub.RaiseEvent(100, 200));
System.Console.WriteLine(pub.RaiseEvent(1000, 2000));