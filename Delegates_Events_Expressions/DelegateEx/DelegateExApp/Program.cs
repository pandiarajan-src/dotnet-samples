using DelegateExLib;

Calc myCalc = new Calc();

//Single Cast Delegate that deals with one method reference
Console.WriteLine("This is single cast delegate example");
DelegateTypeEx singleCastExample;
singleCastExample = new DelegateTypeEx(myCalc.add);
Console.WriteLine(singleCastExample.Invoke(10, 20));

//Multi Cast delegate that deals with multiple method reference
Console.WriteLine("This is multi cast delegate example");
DelegateTypeEx multiCastExample;
multiCastExample = myCalc.add;
multiCastExample += myCalc.multiply;

//Delegate using static method
multiCastExample += Calc.static_subtraction;

Console.WriteLine(multiCastExample.Invoke(10, 20));
