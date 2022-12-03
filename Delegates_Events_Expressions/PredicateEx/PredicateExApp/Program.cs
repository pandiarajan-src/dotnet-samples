// See https://aka.ms/new-console-template for more information

using PredicateExLib;

Publisher pub = new Publisher();

pub.myEvent += (o) =>
{
    System.Console.WriteLine(o.a + o.b);
    return true;
};

System.Console.WriteLine(pub.RaiseEvent(new OpParams(10, 20)));
System.Console.WriteLine(pub.RaiseEvent(new OpParams(100, 200)));
System.Console.WriteLine(pub.RaiseEvent(new OpParams(1000, 2000)));