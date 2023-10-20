namespace Problem0063_UniquePathsII
{
    internal class Program
    {
        static void Main()
        {
            var solution = new Solution();

            int[][] c1 = new int[][] { new int[] { 0, 0, 0 }, new int[] { 0, 1, 0 }, new int[] { 0, 0, 0 } };
            Console.WriteLine(solution.UniquePathsWithObstacles(c1));

            int[][] c2 = new int[][] { new int[] { 0, 1 }, new int[] { 0, 0 } };
            Console.WriteLine(solution.UniquePathsWithObstacles(c2));
        }

        public class Solution
        {
            public int UniquePathsWithObstacles(int[][] obstacleGrid)
            {
                int m = obstacleGrid.Length - 1;
                int n = obstacleGrid[0].Length - 1;
                obstacleGrid[m][n] = 1 - obstacleGrid[m][n];

                for (int i = m - 1; i >= 0; i--)
                {
                    obstacleGrid[i][n] = (1 - obstacleGrid[i][n]) * obstacleGrid[i + 1][n];
                }

                for (int j = n - 1; j >= 0; j--)
                {
                    obstacleGrid[m][j] = (1 - obstacleGrid[m][j]) * obstacleGrid[m][j + 1];
                }

                for (int i = m - 1; i >= 0; i--)
                {
                    for (int j = n - 1; j >= 0; j--)
                    {
                        obstacleGrid[i][j] = (1 - obstacleGrid[i][j]) * (obstacleGrid[i + 1][j] + obstacleGrid[i][j + 1]);
                    }
                }

                return obstacleGrid[0][0];
            }
        }
    }
}