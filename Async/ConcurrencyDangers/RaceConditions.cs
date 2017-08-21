using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace ConcurrencyDangers
{
    [TestClass]
    public class RaceConditions
    {
        [TestMethod]
        public void Race_Condition_Demonstration()
        {
            long expected = (LargeComputation.ExpectedAnswer * 3);
            int sum = 0;
            int counter = 0;

            while (sum == expected || sum == 0) //The race condition won't trigger every time, keep tryign until it occurs
            {
                sum = 0;

                var task1 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    sum += t; //All tasks are accessing sum at the same time, this could trigger a race condition
                });

                var task2 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    sum += t;
                });

                var task3 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    sum += t;
                });

                Task.WaitAll(task1, task2, task3);

                counter++;

            }

            Assert.AreEqual(expected, sum, $"It took {counter} tries to trigger the race condition.");
        }

        [TestMethod]
        public void Race_Condition_Solved_By_Lock()
        {
            long expected = (LargeComputation.ExpectedAnswer * 3);
            int sum = 0;
            int counter = 0;
            var l = new object();

            while ((sum == expected || sum == 0) && (counter < 50)) //try at least 50 times to trigger the race condition
            {
                sum = 0;

                var task1 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    lock (l) //critical section only one task can update 'sum' variable at a time, keep critical section as small as possible
                    {
                        sum += t;
                    }
                });

                var task2 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    lock (l) //critical section only one task can update 'sum' variable at a time
                    {
                        sum += t;
                    }
                });

                var task3 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    lock (l) //critical section only one task can update 'sum' variable at a time
                    {
                        sum += t;
                    }
                });

                Task.WaitAll(task1, task2, task3);

                counter++;

            }

            Assert.AreEqual(expected, sum, $"It took {counter} tries to trigger the race condition.");

        }

        [TestMethod]
        public void Race_Condition_Solved_By_Interlocked()
        {
            /*
             Interlocked is a hardware based lock for simple, arithmetic critical sections.
             Typically more efficient than 'lock'.
             It contains simple arithemtic methods 'Add', 'Increment', 'Decrement' etc.
             */

            long expected = (LargeComputation.ExpectedAnswer * 3);
            int sum = 0;
            int counter = 0;

            while ((sum == expected || sum == 0) && (counter < 50)) //try at least 50 times to trigger the race condition
            {
                sum = 0;

                var task1 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    Interlocked.Add(ref sum, t);
                });

                var task2 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    Interlocked.Add(ref sum, t);
                });

                var task3 = Task.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    Interlocked.Add(ref sum, t);
                });

                Task.WaitAll(task1, task2, task3);

                counter++;

            }

            Assert.AreEqual(expected, sum, $"It took {counter} tries to trigger the race condition.");

        }

        [TestMethod]
        public void Race_Condition_Solved_By_Lock_Free_Design()
        {
            /*
            Using a lock free design to solve the race condition.
            The variable 'sum' is no longer updated by each task, instead each task returns it's result and that is used instead
            Lock free design should be faster than lock because there is less contention (locking)
             *  */

            long expected = (LargeComputation.ExpectedAnswer * 3);
            int sum = 0;
            int counter = 0;

            while ((sum == expected || sum == 0) && (counter < 50)) //try at least 50 times to trigger the race condition
            {
                sum = 0;

                var task1 = Task<int>.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    return t;
                });

                var task2 = Task<int>.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    return t;
                });

                var task3 = Task<int>.Factory.StartNew(() =>
                {
                    int t = new LargeComputation().Compute();
                    return t;
                });

                sum = task1.Result + task2.Result + task3.Result;

                counter++;

            }

            Assert.AreEqual(expected, sum, $"It took {counter} tries to trigger the race condition.");

        }

        [TestMethod]
        public void Race_Condition_Caused_By_Shared_Object_Demonstration()
        {
            List<int> results = new List<int>(); //List is not thread safe

            for (int i = 0; i < int.MaxValue; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    
                });
            }

        }
    }
}
