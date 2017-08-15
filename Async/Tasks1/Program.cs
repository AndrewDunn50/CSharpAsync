using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1
{
    class Program
    {
        static void Main(string[] args)
        {
            var simple = new SimpleTaskSample();

            for (int i = 0; i < 10; i++)
            {
                simple.SimpleTask();
            }

            var wait = new WaitTaskSample();
            wait.WaitSample();

            var child = new ChildTaskSample();
            child.ChildSample();

            Console.Read();
        }

    }
}
