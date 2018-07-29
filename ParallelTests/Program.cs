using System;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelTests
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Parallel.For(i, 100, (shit) => 
            {
                Console.WriteLine($"{i}. {Thread.CurrentThread.ManagedThreadId}");
                i++;
            });

            Console.ReadLine();
        }
    }
}
