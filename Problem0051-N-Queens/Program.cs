using System.Diagnostics;

namespace Problem0051_N_Queens
{
    public static class Program
    {
        public static void Main()
        {
            int s = 1;
            int e = 9;
            Stopwatch sw = Stopwatch.StartNew();
            Problem0051_N_Queens_6.SolutionRunner.Run(s, e);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
            Console.WriteLine();
            sw = Stopwatch.StartNew();
            Problem0051_N_Queens_4.SolutionRunner.Run(s, e);
            sw.Stop();
            Console.WriteLine(sw.Elapsed);
        }
    }
}