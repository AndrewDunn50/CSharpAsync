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
            var sample = new TaskSample();

            for (int i = 0; i < 10; i++)
            {
                sample.SimpleTask();
            }

            Console.Read();
        }

    }
}
