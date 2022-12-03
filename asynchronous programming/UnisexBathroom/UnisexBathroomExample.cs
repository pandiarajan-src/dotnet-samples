internal class UniSexBathroomExample
{
    private string useBy;
    private int nUsers;
    private int maxUsers;
    private Object padlock;
    private Semaphore sem;

    public UniSexBathroomExample(int maxUsers)
    {
        this.maxUsers = maxUsers;
        useBy = "None";
        nUsers = 0;
        padlock = new Object();
        sem = new Semaphore(maxUsers, maxUsers);
    }

    private void UseBathroom(Object name)
    {
        System.Console.WriteLine($"{name} Enters Bathroom who is {useBy} so total users {nUsers} in Thread {Thread.CurrentThread.ManagedThreadId}");
        Thread.Sleep(2000);
        System.Console.WriteLine($"{name} Exits Bathroom who is {useBy} so total users {nUsers} in Thread {Thread.CurrentThread.ManagedThreadId}");

    }

    public void MaleUseBathRoom(Object? name)
    {
        if(name == null)
            return;
        Monitor.Enter(padlock);
        while(useBy.Equals("Female"))
        {
            Monitor.Wait(padlock);
        }
        useBy = "Male";
        sem.WaitOne();
        nUsers++;
        Monitor.Exit(padlock);

        UseBathroom(name);
        sem.Release();

        Monitor.Enter(padlock);
        nUsers--;
        if(nUsers == 0)
        {
            useBy = "None";
        }
        Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }

    public void FemaleUseBathRoom(Object? name)
    {
        if(name == null)
            return;        
        Monitor.Enter(padlock);
        while(useBy.Equals("Male"))
        {
            Monitor.Wait(padlock);
        }
        useBy = "Female";
        sem.WaitOne();
        nUsers++;
        Monitor.Exit(padlock);

        UseBathroom(name);
        sem.Release();

        Monitor.Enter(padlock);
        nUsers--;
        if(nUsers == 0)
        {
            useBy = "None";
        }
        Monitor.PulseAll(padlock);
        Monitor.Exit(padlock);
    }

    public int GetUsersInBathroom()
    {
        return nUsers;
    }
}