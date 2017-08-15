using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1
{
    class WaitTaskSample
    {
        internal void WaitSample()
        {
            int counter = 0; ;

            var task1 = Task.Factory.StartNew(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                { }
                counter++;
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                counter++;
            });

            var task3 = Task.Factory.StartNew(() =>
            {
                counter++;
            });

            var task4 = Task.Factory.StartNew(() =>
            {
                counter++;
            });

            task1.Wait(); //wait until task1 is complete - longest running task

            Console.WriteLine($"WaitSample: {counter} tasks complete");
        }
    }
}
