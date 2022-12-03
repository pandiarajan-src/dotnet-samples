namespace ActionExLib;
public class Publisher
{
    public event Action<int, int>? myEvent;

    public void RaiseEvent(int a, int b)
    {
        if(this.myEvent is not null)
        {
            this.myEvent(a, b);
        }
    }
}
