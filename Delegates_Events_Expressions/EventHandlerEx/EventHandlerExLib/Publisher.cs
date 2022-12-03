namespace EventHandlerExLib;

public class CustomeEventArgs : EventArgs
{
    public CustomeEventArgs(int x, int y){a=x; b=y;}
    public int a { get; set; }
    public int b { get; set; }
}

public class Publisher
{
    public event EventHandler<CustomeEventArgs>? myEvent;

    public void RaiseEvent(Object sender, CustomeEventArgs e)
    {
        if(this.myEvent is not null)
        {
            this.myEvent(sender, e);
        }
    }

}
