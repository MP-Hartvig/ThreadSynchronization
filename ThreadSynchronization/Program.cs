using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSynchronization
{
    // ØVELSE 1
    internal class Program
    {
        private static bool terminator = false;
        static int counter = 0;
        static object lockResource = new object();

        private static void SingleCount()
        {
            while (terminator == false)
            {
                Monitor.Enter(lockResource);
                //Interlocked.Decrement(ref counter);
                counter -= 1;
                Console.WriteLine(counter);
                Monitor.Exit(lockResource);
                Thread.Sleep(1000);
            }
        }

        private static void DoubleCount()
        {
            while (terminator == false)
            {
                Monitor.Enter(lockResource);
                //Interlocked.Add(ref counter, 2);
                counter += 2;
                Console.WriteLine(counter);
                Monitor.Exit(lockResource);
                Thread.Sleep(1000);
            }
        }

        static void Main(string[] args)
        {
            Thread singleC = new Thread(SingleCount);
            singleC.Start();

            Thread doubleC = new Thread(DoubleCount);
            doubleC.Start();

            singleC.Join();
            doubleC.Join();

            Console.ReadLine();
        }
    }


    // ØVELSE 2 OG 3
    internal class Program2
    {
        private static int counter = 0;
        private static char star = '*';
        private static char hashtag = '#';
        private static bool terminator = false;
        private static object testObj = new object();

        private static void StarWriter()
        {
            while (terminator == false)
            {
                Monitor.Enter(testObj);
                for (int i = 0; i < 60; i++)
                {
                    Console.Write(star);
                    counter++;
                }
                Console.Write(" " + counter + "\n");
                Monitor.Exit(testObj);
                Thread.Sleep(1000);
            }
        }

        private static void HashtagWriter()
        {
            while (terminator == false)
            {
                Monitor.Enter(testObj);
                for (int i = 0; i < 60; i++)
                {
                    Console.Write(hashtag);
                    counter++;
                }
                Console.Write(" " + counter + "\n");
                Monitor.Exit(testObj);
                Thread.Sleep(1000);
            }
        }

        static void Main2(string[] args)
        {
            Thread tStar = new Thread(StarWriter);
            Thread tHashtag = new Thread(HashtagWriter);

            tStar.Start();
            tHashtag.Start();

            if (Console.ReadKey().Key == ConsoleKey.Enter)
            {
                terminator = true;
            }

            if (terminator == true)
            {
                try
                {
                    tStar.Abort();
                    tHashtag.Abort();
                }
                catch (ThreadAbortException ex)
                {
                    Console.WriteLine(ex);
                }
            }
            Console.ReadLine();
        }
    }
}
