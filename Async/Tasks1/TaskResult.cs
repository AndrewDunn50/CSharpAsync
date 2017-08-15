using System;

namespace Tasks1
{
    public class TaskResult
    {
        public long Counter { get; set; }
        public long TaskTime { get; set; }
        public long ChildTaskTime { get; set; }
        public override string ToString()
        {
            var nl = Environment.NewLine;

            return
                $"Counter: {Counter} {nl} TaskTime: {TaskTime}ms {nl} ChildTaskTime: {ChildTaskTime}ms {nl}";
        }
    }
}
