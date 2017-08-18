using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1
{

    /*
     We are required to observe any exceptions thrown by a task to do this we must either:
     1. call .Wait or touch .Result - the exception will be re-thrown at this point
     2. call Task.WaitAll - exception re-thrown when all have finished
     3. touch task's .Exception property after the exception has completed
     4. subscribe to TaskSchedular.UnobservedTaskException (subscribe to in static ctor, so only subscribed to once)
    */

    public class ExceptionHandlingSample
    {
        static ExceptionHandlingSample()
        {
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        }

        private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            var exception = e.Exception.Flatten();
            foreach (var ex in exception.InnerExceptions)
            {
                Console.WriteLine("UnObservedException: {0}", ex.Message);
            }
         
            e.SetObserved(); //calling SetObserved means we have followed the rule to observe an exception, the exception won't be re-thrown at garbage collection
        }

        public void ExceptionSample1()
        {
            var task1 = Task.Factory.StartNew(() =>
            {
                throw new Exception("task 1 exception");
            });

            var task2 = Task.Factory.StartNew(() =>
            {
                throw new Exception("task 2 exception");
            });

            var task3 = Task.Factory.StartNew(() =>
            {
                throw new Exception("task 3 exception");
            });

            try
            {
                //WaitAll - exception will be re-thrown here
                Task.WaitAll(task1, task2, task3);
            }
            catch ( AggregateException ae)
            {
                ae = ae.Flatten(); //call flatten to flatten the exception tree
                foreach(var ex in ae.InnerExceptions)
                {
                    Console.WriteLine("ExceptionSample1: {0}", ex.Message);
                }
            }
        }

        public void ExceptionSample2()
        {
            var task1 = Task<int>.Factory.StartNew(() =>
            {
                throw new Exception("task 1 exception");
            });

            var task2 = Task<int>.Factory.StartNew(() =>
            {
                throw new Exception("task 2 exception");
            });

            var task3 = Task<int>.Factory.StartNew(() =>
            {
                throw new Exception("task 3 exception");
            });


            task1.ContinueWith((antecedent) =>
            {
                try
                {
                    //Accessing the result - exception will be re-thrown here
                    int result = antecedent.Result;
                }
                catch (AggregateException ae)
                {
                    ae = ae.Flatten(); //call flatten to flatten the exception tree
                    foreach (var ex in ae.InnerExceptions)
                    {
                        Console.WriteLine("ExceptionSample2: {0}", ex.Message);
                    }
                }
            });

            //We haven't observed exceptions from task2 and task3, these will go to the TaskScheduler_UnobservedTaskException handler
            GC.Collect(); //Just to demonstrate unhandled exceptions going to TaskScheduler_UnobservedTaskException
        }

    }
}
