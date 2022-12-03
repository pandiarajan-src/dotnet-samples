// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ThreadSafeDeferedCallBackExecutor exec = new ThreadSafeDeferedCallBackExecutor();
Thread start_thread = new Thread( () => {
    exec.Start();
});
start_thread.Start();

int length = 5;
DeferedAction[] act0 = new DeferedAction[length];
DeferedAction[] act1 = new DeferedAction[length];
DeferedAction[] act2 = new DeferedAction[length];
for (int i = 0; i < length; i++)
{
    for (int j = 0; j < length; j++)
    {
        act0[j] = new DeferedAction();
        act0[j].Message = "from act0 with 15sec";
        act0[j].ExecutesAt = DateTimeOffset.Now.AddSeconds(15).ToUnixTimeSeconds();     
        exec.registerCallback(act0[j]);

        act1[j] = new DeferedAction();
        act1[j].Message = "from act1 with 5sec";
        act1[j].ExecutesAt = DateTimeOffset.Now.AddSeconds(5).ToUnixTimeSeconds();   
        exec.registerCallback(act1[j]);        

        act2[j] = new DeferedAction();
        act2[j].Message = "from act2 with 25sec";
        act2[j].ExecutesAt = DateTimeOffset.Now.AddSeconds(25).ToUnixTimeSeconds();      
        exec.registerCallback(act2[j]);                
    }
}

start_thread.Join();
