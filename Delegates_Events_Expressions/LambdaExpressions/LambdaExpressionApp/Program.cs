// See https://aka.ms/new-console-template for more information
using LambdaExpressionLib;

Publisher myPub = new Publisher();

// This is called Lamda Expressions.
// myPub.myEvent += (a, b) =>
// {
//     System.Console.WriteLine(a + b);
// };

// This is inline Lambda expressions.
myPub.myEvent += (a, b) => System.Console.WriteLine(a + b);

myPub.RaiseEvent(10, 20);
myPub.RaiseEvent(40, 50);