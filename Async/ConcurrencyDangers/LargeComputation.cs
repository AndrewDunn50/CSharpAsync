namespace ConcurrencyDangers
{
    public class LargeComputation
    {
        public const int ExpectedAnswer = 999;

        public int Compute()
        {
            int counter = 0;
            for(int i = 0; i < ExpectedAnswer; i++)
            {
                counter++;
            }
            return counter;
        }
    }
}
