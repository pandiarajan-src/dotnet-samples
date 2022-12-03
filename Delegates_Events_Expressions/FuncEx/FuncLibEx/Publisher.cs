namespace FuncLibEx;
public class Publisher
{
    public event Func<int, int, int>? myEvent;

    public int RaiseEvent(int a, int b)
    {
        if(this.myEvent is not null)
        {
            return this.myEvent(a, b);
        }
        return 0;
    }
}
