namespace Problem0064_MinPathSum
{
    public class Solution2
    {
        public static int MinPathSum(int[][] grid)
        {
            for (int i = grid.Length - 2; i >= 0; i--)
            {
                grid[i][^1] += grid[i + 1][^1];
            }

            for (int j = grid[0].Length - 2; j >= 0; j--)
            {
                grid[^1][j] += grid[^1][j + 1];
            }

            for (int i = grid.Length - 2; i >= 0; i--)
            {
                for (int j = grid[0].Length - 2; j >= 0; j--)
                {
                    if (grid[i][j + 1] < grid[i + 1][j])
                    {
                        grid[i][j] += grid[i][j + 1];
                    }
                    else
                    {
                        grid[i][j] += grid[i + 1][j];
                    }
                }
            }

            return grid[0][0];
        }
    }
}