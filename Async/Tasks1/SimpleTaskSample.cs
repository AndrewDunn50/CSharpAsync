using System;
using System.Threading.Tasks;

namespace Tasks1
{
    class SimpleTaskSample
    {
        private int _taskCounter = 0;
        private object lockObject = new object();

        internal void SimpleTask()
        {
            var task = Task.Factory.StartNew(() =>
            {
                lock (lockObject)
                {
                    _taskCounter++;
                }

                int taskNumber = _taskCounter;

                for (int i = 0; i < int.MaxValue; i++) { }

                return taskNumber;
            });

            //Continuation - fire task after 'task' is finished
            var continueTask = task.ContinueWith((antecedent) => //antecendent - task that fired this task (parent)
            {
                Console.WriteLine($"{Environment.NewLine}Simple Sample: Task {antecedent.Result} Complete");
            }); 
        }

    }
}
