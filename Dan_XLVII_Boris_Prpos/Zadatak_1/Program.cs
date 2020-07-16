using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        //List that will contain all threads
        static List<Thread> threadList = new List<Thread>();
        //countdown object that will signal when all threads are finished
        static CountdownEvent countdown = new CountdownEvent(1);

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            //starting stopwatch that will measure time in app
            sw.Start();
            Delegate d = new Delegate();
            Random rnd = new Random();
            //generating random number that will represent number of cars
            int carNum = rnd.Next(1, 16);
            //array containing directions=>direction will be given to thread using random number in line 30
            string[] directions = new string[] { "North", "South" };

            for (int i = 0; i < carNum; i++)
            {
                int directionRan = rnd.Next(0, 2);
                //will be used to create thread name
                string temp = (i + 1).ToString();
                //forwarding method to thread
                Thread t = new Thread(()=>Go(Thread.CurrentThread))
                {
                    //creating name based on random selected direction 
                    Name=string.Format("Car " + temp+ " moving to "+directions[directionRan])
                };
                threadList.Add(t);
            }
            //calling delegate
            d.Ready(carNum, threadList);
            Console.WriteLine();

            for (int i = 0; i < threadList.Count; i++)
            {
                //starting thread
                threadList[i].Start();
                Thread.Sleep(100);
                //if it is not last thread(avoiding out of range exception) and if two consecutive threads have same direction
                if ((i < threadList.Count - 1) &&  CompareNames(threadList[i].Name, threadList[i + 1].Name) == false)
                {
                    //wait and let previous thread to finish
                    Console.WriteLine("\t {0} is waiting for bridge to be open.", threadList[i + 1].Name);
                    //LET previous thread to finish
                    threadList[i].Join();

                }
                //if this is last thread
                if (threadList[i] == threadList[threadList.Count - 1])
                {
                    //let him finish and signal
                    threadList[i].Join();
                    countdown.Signal();
                }               
            }
            //catch signal from line 63 (every thread has finished)
            if (countdown.IsSet)
            {
                //stop the stopwatch and display the results
                sw.Stop();
                Console.WriteLine("\n Total time spent in application : {0}h {1}m {2}s {3}ms", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);
            }       
            Console.ReadLine();
        }
        /// <summary>
        /// Method that will be forwarded to every thread
        /// </summary>
        /// <param name="t"></param>
        static void Go(Thread t)
        {
            //Going over the bridge lasts for 500 mS
            Console.WriteLine("{0} going over the bridge.",t.Name);
            Thread.Sleep(500);
            Console.WriteLine("\t\t{0} has crossed the bridge.",t.Name);
        }
        /// <summary>
        /// Method compares if one string contains another, will be used to determine if two consecutive threads (cars) have same directions (North or South)
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        static bool CompareNames(string s1, string s2)
        {
            if (s1.Contains("North"))
            {
                if (s2.Contains("North"))
                {
                    //they contain same direction
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (s2.Contains("South"))
                {
                    //they contain same direction
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}
