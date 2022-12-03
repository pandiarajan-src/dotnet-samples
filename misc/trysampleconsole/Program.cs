// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

List<string> list = Enumerable.Range(0, 10).Select(x => x.ToString().PadLeft(10,'0')).ToList();
list.ForEach(x => Console.WriteLine(x));
