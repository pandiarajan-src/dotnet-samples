internal class ProducerConsumerMutex
{
    private readonly int max_size = 3;
    private Object[] item;
    private int head;
    private int tail;
    private int curr_size;
    private Mutex mutex;

    public ProducerConsumerMutex()
    {
        curr_size = 0;
        head = 0;
        tail = 0;
        item = new Object[max_size];
        mutex = new Mutex(false, "ProducerConsumerMutex");
    }

    public void enqueue(Object val)
    {
        mutex.WaitOne();
        while(curr_size == max_size)
        {
            mutex.ReleaseMutex();
            mutex.WaitOne();
        }
        if(tail == max_size) tail = 0;
        item[tail] = val;
        System.Console.WriteLine($"Produced item[{tail}]={val}");
        tail++;
        curr_size++;
        mutex.ReleaseMutex();
    }

    public Object dequeue()
    {
        mutex.WaitOne();
        while(curr_size == 0)
        {
            mutex.ReleaseMutex();
            mutex.WaitOne();            
        }
        if(head == max_size) head = 0;
        Object val = item[head];
        System.Console.WriteLine($"Consumed item[{head}]={val}");
        head++;
        curr_size--;
        mutex.ReleaseMutex();
        return val;
    }
}