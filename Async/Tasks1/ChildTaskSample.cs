using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Tasks1
{
    class ChildTaskSample
    {
        internal void ChildSample()
        {
            var task = Task<TaskResult>.Factory.StartNew(() =>
            {
                Console.WriteLine("Child Sample: Starting Task...");
                var stopWatch = Stopwatch.StartNew();

                var result = new TaskResult();

                var childTask = Task.Factory.StartNew(() =>
                {
                    Console.WriteLine("Child Sample: Starting Child Task...");

                    var childStopWatch = Stopwatch.StartNew();

                    for (int i = 0; i < int.MaxValue; i++)
                    {
                        result.Counter++;
                    }

                    childStopWatch.Stop();

                    result.ChildTaskTime = childStopWatch.ElapsedMilliseconds;

                }, TaskCreationOptions.AttachedToParent); //if we don't specify attach to parent this child task could finish after main task

                var taskTimeStart = DateTime.Now;

                for (int i = 0; i < int.MaxValue; i++)
                {
                    result.Counter++;
                }

                stopWatch.Stop();
                result.TaskTime = stopWatch.ElapsedMilliseconds; ;

                return result;
            });

            Console.WriteLine("Child Sample: Waiting for result...");
            Console.WriteLine(task.Result); //waits here until task is finished
            Console.WriteLine("Child Sample: Task result returned.");

        }
    }
}
