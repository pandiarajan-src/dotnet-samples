namespace PredicateExLib;

public class OpParams{
    public OpParams(int x, int y){a = x; b = y;}
    public int a { get; set; }
    public int b { get; set; }
}

public class Publisher
{
    public event Predicate<OpParams>? myEvent;

    public bool RaiseEvent(OpParams obj)
    {
        if(this.myEvent is not null)
        {
            return myEvent(obj);
        }
        return false;
    }
}
