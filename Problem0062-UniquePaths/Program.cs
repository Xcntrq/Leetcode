namespace Problem0062_UniquePaths
{
    internal class Program
    {
        static void Main()
        {
            var solution = new Solution();
            Console.WriteLine(solution.UniquePaths(3, 7));
            Console.WriteLine(solution.UniquePaths(3, 2));
        }

        public class Solution
        {
            public int UniquePaths(int m, int n)
            {
                int[,] grid = new int[m, n];
                grid[m - 1, n - 1] = 1;

                for (int i = m - 2; i >= 0; i--)
                {
                    grid[i, n - 1] = grid[i + 1, n - 1];
                }

                for (int j = n - 2; j >= 0; j--)
                {
                    grid[m - 1, j] = grid[m - 1, j + 1];
                }

                for (int i = m - 2; i >= 0; i--)
                {
                    for (int j = n - 2; j >= 0; j--)
                    {
                        grid[i, j] = grid[i + 1, j] + grid[i, j + 1];
                    }
                }

                return grid[0, 0];
            }
        }
    }
}