// See https://aka.ms/new-console-template for more information
using ActionExLib;

Publisher pub = new Publisher();

pub.myEvent += (a, b) =>
{
    System.Console.WriteLine(a+b);
};

pub.RaiseEvent(10, 20);
pub.RaiseEvent(100, 200);
pub.RaiseEvent(1000, 2000);
