using System;
using System.Threading;

namespace Async
{
    public static class ThreadPoolDemo
    {
        public class StateObject
        {
            public bool IsCompleted { get; set; } = false;
        }

        public static void ThreadProc(object state)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("ThreadPoolProc: {0}", i);
            }

            if (state is StateObject stateObject)
            {
                stateObject.IsCompleted = true;
            }  
        }

        public static void Run()
        {
            Console.WriteLine("Threadpool demo");

            var state = new StateObject();
            ThreadPool.QueueUserWorkItem(p => ThreadProc(p), state);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine("MainProc: {0}", i);
            }

            while (!state.IsCompleted)
            {
                Console.WriteLine("Waiting for Threadproc...");
                Thread.Sleep(10);
            }

            Console.WriteLine("END Threadpool demo");
        }
    }
}
