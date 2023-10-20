namespace Problem0064_MinPathSum
{
    internal class Program
    {
        static void Main()
        {
            var solution = new Solution();
            var solution2 = new Solution2();
            int[][] c1 = new int[][] { new int[] { 1, 3, 1 }, new int[] { 1, 5, 1 }, new int[] { 4, 2, 1 } };
            Console.WriteLine(solution.MinPathSum(c1));
            Console.WriteLine(Solution2.MinPathSum(c1));

            solution = new Solution();
            solution2 = new Solution2();
            int[][] c2 = new int[][] { new int[] { 1, 2, 3 }, new int[] { 4, 5, 6 } };
            Console.WriteLine(solution.MinPathSum(c2));
            Console.WriteLine(Solution2.MinPathSum(c2));
        }
    }

    public class Solution
    {
        private readonly Dictionary<int, int> _hash = new();

        public int MinPathSum(int[][] grid)
        {
            int i = grid.Length - 1;
            int j = grid[0].Length - 1;
            if ((i == 0) && (j == 0)) return grid[i][j];

            int h = (i << 8) + j;
            _hash[h] = grid[i][j];

            return MinPathSum2(0, 0, grid);
        }

        private int MinPathSum2(int i, int j, int[][] grid)
        {
            int r = int.MaxValue;
            int d = int.MaxValue;

            int h = (i << 8) + j;
            int hd = ((i + 1) << 8) + j;
            int hr = (i << 8) + j + 1;

            if (i != grid.Length - 1)
            {
                if (_hash.ContainsKey(hd))
                    d = _hash[hd];
                else
                    d = MinPathSum2(i + 1, j, grid);
            }

            if (j != grid[i].Length - 1)
            {
                if (_hash.ContainsKey(hr))
                    r = _hash[hr];
                else
                    r = MinPathSum2(i, j + 1, grid);
            }

            int p = grid[i][j];

            if (d < r)
            {
                p += d;
            }
            else
            {
                p += r;
            }

            _hash[h] = p;
            return p;
        }
    }
}