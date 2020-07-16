using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Delegate
    {
        public delegate void Notification();

        public event Notification OnNotification;

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
