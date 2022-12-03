// See https://aka.ms/new-console-template for more information

using AnnoMethodLib;

Publisher myPub = new Publisher();

// Annoymous method of  using delegate without any subscriber.
myPub.myEvent += delegate(int a, int b)
{
    int c = a + b;
    System.Console.WriteLine(c);
};

myPub.RaiseEvent(10, 20);
myPub.RaiseEvent(30, 20);


