internal class ProducerConsumerMonitor
{
    private readonly int max_size = 3;
    private int head;
    private int tail;
    private int curr_size;
    private Object[] items;
    
    public ProducerConsumerMonitor()
    {
        head = 0;
        tail = 0;
        curr_size = 0;
        items = new Object[max_size];
    }
    public void enqueue(Object val)
    {
        lock(items){
            //Monitor.Enter(items);
            while(curr_size == max_size)
            {
                Monitor.Wait(items);
            }
            if(tail == max_size) tail = 0;
            items[tail] = val;
            System.Console.WriteLine($"Produced : items[{tail}] = {val}");
            tail++;
            curr_size++;
            Monitor.PulseAll(items);
            //Monitor.Exit(items);
        }

    }
    public Object dequeue()
    {
        //Monitor.Enter(items);
        lock(items)
        {
            while(curr_size == 0)
            {
                Monitor.Wait(items);
            }
            if(head == max_size) head = 0;
            var val = items[head];
            System.Console.WriteLine($"Consumed : items[{head}]={val}");
            head++;
            curr_size--;
            Monitor.PulseAll(items);
            //Monitor.Exit(items);
            return val;
        }
    }
}