using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tasks1
{
    class WaitTaskSample
    {
        internal void WaitSample()
        {

            Task<int> task1 = Task.Factory.StartNew(() =>
            {
                int counter = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    counter++;
                }
                return counter;
            });

            Task<int> task2 = Task.Factory.StartNew(() =>
            {
                int counter = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    counter++;
                }
                return counter;
            });

            Task<int> task3 = Task.Factory.StartNew(() =>
            {
                int counter = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    counter++;
                }
                return counter;
            });

            Task<int> task4 = Task.Factory.StartNew(() =>
            {
                int counter = 0;
                for (int i = 0; i < int.MaxValue; i++)
                {
                    counter++;
                }
                return counter;
            });

            //Wait for all tasks to finish
            //Task.WaitAll(task1, task2, task3, task4);

            //Wait for any task to finish
            //Task.WaitAny(task1, task2, task3, task4); //returns index of task completed

            //WaitAllOneByOne pattern
            //Use when:
            // - Some may fail - discard/retry
            // - Overlap computation with result processing (hide latency)
            List<Task<int>> tasks = new List<Task<int>>()
            {
                task1, task2, task3, task4
            };

            while(tasks.Count > 0)
            {
                var taskArray = tasks.ToArray();
                int index = Task.WaitAny(taskArray);
                Console.WriteLine($"WaitSample: index {index} complete. {taskArray.Count()} remaining. Result: {tasks[index].Result}");
                tasks.RemoveAt(index);
            }
        }
    }
}
