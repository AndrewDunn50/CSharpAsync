using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tasks1
{
    public class CancelTaskSample
    {
        /*
          To cancel a task:
          - Create a CancellationTokenSource on the caller of the task
          - Pass the CancellationTokenSource.Token to the task
          - Monitor the token in the task:
                - cancel the task by calling CancellationTokenSource.Cancel() from the caller
                - if token.IsCancellationRequested is true (in the task) then call token.ThrowIfCancellationRequested();
          - On the caller catch the OperationCancelledException
        */


        public void CancelSample()
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var task1 = Task<int>.Factory.StartNew(() =>
            {
                for (int i = 0; i < int.MaxValue; i++)
                {
                    if (token.IsCancellationRequested)
                    {
                        //clean up objects here then throw exception
                        token.ThrowIfCancellationRequested();
                    }
                }
                return 0;
            }, token);

            Thread.Sleep(1000); //Wait 1 second then cancel task
            cts.Cancel(); //Cancel task

            try
            {
                var result = task1.Result;
            }
            catch (AggregateException ae)
            {
                if (ae.InnerException is OperationCanceledException)
                {
                    Console.WriteLine("CancelSample: Operation cancelled");
                }
                else
                {
                    ae = ae.Flatten(); 
                    foreach (var ex in ae.InnerExceptions)
                    {
                        Console.WriteLine("CancelSample: {0}", ex.Message);
                    }
                }

            }
        }
    }
}
