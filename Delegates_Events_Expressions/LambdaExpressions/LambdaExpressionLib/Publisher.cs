namespace LambdaExpressionLib;

public delegate void MyDelegateType(int a, int b);
public class Publisher
{
    //Auto implemented event, even without defining delegatetype and having add, remove accessors
    public event MyDelegateType? myEvent;
    public void RaiseEvent(int a, int b)
    {
        if(this.myEvent != null)
        {
            this.myEvent(a, b);
        }
    }
}