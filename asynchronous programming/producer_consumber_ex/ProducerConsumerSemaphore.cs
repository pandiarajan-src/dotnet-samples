internal class ProducerConsumerSemaphore
{
    private readonly int max_size = 3;
    private int head;
    private int tail;
    private Object[] items;
    private Semaphore prodSem;
   private Semaphore consSem;
   private Semaphore mutSemOneLock;
    
    public ProducerConsumerSemaphore()
    {
        head = 0;
        tail = 0;
        items = new Object[max_size];
        prodSem = new Semaphore(max_size,max_size);
        consSem = new Semaphore(0,max_size);
        mutSemOneLock = new Semaphore(1,1);
    }
   public void enqueue(Object val)
    {
        //Monitor.Enter(items);
        prodSem.WaitOne();
        mutSemOneLock.WaitOne();
        if(tail == max_size) tail = 0;
        items[tail] = val;
        System.Console.WriteLine($"Produced : items[{tail}] = {val}");
        tail++;
        mutSemOneLock.Release();
        consSem.Release();
    }
    public Object dequeue()
    {
        consSem.WaitOne();
            mutSemOneLock.WaitOne();
                if(head == max_size) head = 0;
                var val = items[head];
                System.Console.WriteLine($"Consumed : items[{head}]={val}");
                head++;
            mutSemOneLock.Release();
        prodSem.Release();
            return val;

    }

}