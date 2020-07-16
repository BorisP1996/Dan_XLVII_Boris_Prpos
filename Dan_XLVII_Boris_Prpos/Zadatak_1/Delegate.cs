using System;
using System.Collections.Generic;
using System.Threading;

namespace Zadatak_1
{
    /// <summary>
    /// Class contains delegates and events
    /// </summary>
    class Delegate
    {
        public delegate void Notification();

        public event Notification OnNotification;

        /// <summary>
        /// Method that will display number of cars and their directions
        /// </summary>
        /// <param name="carNum"></param>
        /// <param name="list"></param>
        public void Ready(int carNum,List<Thread> list)
        {
            OnNotification += () =>
            {
                Console.WriteLine("Total number of cars is:{0}\n", carNum);
                Console.WriteLine("Directions of cars:\n");
                foreach (Thread item in list)
                {
                    Console.WriteLine(item.Name);
                }
            };
            OnNotification.Invoke();
        }
    }
}
