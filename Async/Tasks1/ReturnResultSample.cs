using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks1
{
    public class ReturnResultSample
    {

        List<int> _numbers = new List<int>
        {
            40,88,55,77,21,56,48,35,15,48,38,15,84,53,48,35,18,38,1,78,56,856,8,45,456,18,61,16,186,186,86,168,168,16,8
        };

        internal void ReturnSample()
        {
            Task<double> avgTask = Task.Factory.StartNew(() =>
            {
                return _numbers.Average();
            });

            Task<int> minTask = Task.Factory.StartNew(() =>
            {
                return _numbers.Min();
            });

            Task<int> maxTask = Task.Factory.StartNew(() =>
            {
                return _numbers.Max();
            });

            //No need to call "Wait" as this is implicit when getting the result
            Console.WriteLine($"Return Sample: Average: {avgTask.Result}");
            Console.WriteLine($"Return Sample: Min: {minTask.Result}");
            Console.WriteLine($"Return Sample: Max: {maxTask.Result}");
        }
    }
}
