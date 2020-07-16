﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static List<Thread> threadList = new List<Thread>();
        static CountdownEvent countdown = new CountdownEvent(1);
        static readonly object locker = new object();

        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            Delegate d = new Delegate();
            Random rnd = new Random();
            int carNum = rnd.Next(1, 16);
            string[] directions = new string[] { "North", "South" };
            //Console.WriteLine("Number of cars is :{0}\n\n\n",carNum);
            for (int i = 0; i < carNum; i++)
            {
                int directionRan = rnd.Next(0, 2);
                string temp = (i + 1).ToString();
                Thread t = new Thread(()=>Go(Thread.CurrentThread))
                {
                    Name=string.Format(temp+" moving to "+directions[directionRan])
                };
                threadList.Add(t);
            }
            d.Ready(carNum, threadList);
            Console.WriteLine();
            for (int i = 0; i < threadList.Count; i++)
            {
                int counter = 0;
                threadList[i].Start();
                Thread.Sleep(100);
                if ((i < threadList.Count - 1) && /*(threadList[i + 1].Name != threadList[i].Name)*/ CompareNames(threadList[i].Name, threadList[i + 1].Name) == false)
                {
                    Console.WriteLine("\tCar {0} is waiting for bridge to be open", threadList[i + 1].Name);
                    threadList[i].Join();

                }
                //lock (locker)
                //{
                //    counter++;
                if (threadList[i] == threadList[threadList.Count - 1])
                {
                    threadList[i].Join();
                    countdown.Signal();
                }
                //}                 
            }
            if (countdown.IsSet)
            {
                sw.Stop();
                Console.WriteLine("\n Total time spent in application : {0}h {1}m {2}s {3}ms", sw.Elapsed.Hours, sw.Elapsed.Minutes, sw.Elapsed.Seconds, sw.Elapsed.Milliseconds);
            }
            
            
            Console.ReadLine();
        }
        static void Go(Thread t)
        {
            Console.WriteLine("{0} going over the bridge.",t.Name);
            Thread.Sleep(2000);
            Console.WriteLine("\t\t{0} has crossed the bridge.",t.Name);
        }
        static bool CompareNames(string s1, string s2)
        {
            if (s1.Contains("North"))
            {
                if (s2.Contains("North"))
                {
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
