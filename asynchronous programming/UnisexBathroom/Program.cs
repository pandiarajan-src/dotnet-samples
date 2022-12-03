// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

UniSexBathroomExampleTest test = new UniSexBathroomExampleTest();
test.RunTest();

internal class UniSexBathroomExampleTest
{
    UniSexBathroomExample unisexBathroom = new UniSexBathroomExample(3);
    public void RunTest()
    {
        Thread male1 = new Thread(new ParameterizedThreadStart(unisexBathroom.MaleUseBathRoom));
        Thread male2 = new Thread(new ParameterizedThreadStart(unisexBathroom.MaleUseBathRoom));
        Thread male3 = new Thread(new ParameterizedThreadStart(unisexBathroom.MaleUseBathRoom));
        Thread male4 = new Thread(new ParameterizedThreadStart(unisexBathroom.MaleUseBathRoom));
        Thread male5 = new Thread(new ParameterizedThreadStart(unisexBathroom.MaleUseBathRoom));

        Thread female1 = new Thread(new ParameterizedThreadStart(unisexBathroom.FemaleUseBathRoom));
        Thread female2 = new Thread(new ParameterizedThreadStart(unisexBathroom.FemaleUseBathRoom));
        Thread female3 = new Thread(new ParameterizedThreadStart(unisexBathroom.FemaleUseBathRoom));
        Thread female4 = new Thread(new ParameterizedThreadStart(unisexBathroom.FemaleUseBathRoom));

        male1.Start("Alex");
        male2.Start("Bob");
        female1.Start("Zora");
        female2.Start("Yadha");
        male3.Start("Chris");
        male4.Start("David");
        male5.Start("Frank");
        female3.Start("Xenifer");
        female4.Start("Wari");

        male1.Join();
        male2.Join();
        male3.Join();
        male4.Join();
        male5.Join();
        female1.Join();
        female2.Join();
        female3.Join();
        female4.Join();

        System.Console.WriteLine($"Users at the end in the bathroom {unisexBathroom.GetUsersInBathroom()}");

    }
}