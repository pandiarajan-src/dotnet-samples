// See https://aka.ms/new-console-template for more information

using EventExApp;
using EventExLib;

Console.WriteLine("Hello, World!");
Subscriber mySub = new Subscriber();
Publisher myPub = new Publisher();
myPub.myEvent += mySub.Add;
myPub.RaiseEvent(10, 20);

