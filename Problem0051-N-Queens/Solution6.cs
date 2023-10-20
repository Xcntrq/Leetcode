namespace Problem0051_N_Queens_6
{
    public static class SolutionRunner
    {
        public static void Run(int s, int e)
        {
            for (int i = s; i <= e; i++)
            {
                new Solution().SolveNQueens(i);
                //  Console.WriteLine($"n={i}: {c}");
            }
        }
    }

    public class Solution
    {
        private readonly HashSet<HashSet<int>> _completeSolutions = new();
        private int _n;

        public IList<IList<string>> SolveNQueens(int n)
        {
            _n = n;
            SolveNQueens(new int[n, n], 0, new HashSet<int>());

            IList<IList<string>> result = new List<IList<string>>();

            foreach (HashSet<int> solution in _completeSolutions)
            {
                IList<string> solutionStr = new List<string>();
                for (int i = 0; i < _n; i++)
                {
                    string str = string.Empty;
                    for (int j = 0; j < _n; j++)
                    {
                        str += solution.Contains((i << 4) + j) ? 'Q' : '.';
                    }

                    solutionStr.Add(str);
                }

                result.Add(solutionStr);
            }

            return result;
        }

        public void SolveNQueens(int[,] board, int currentRow, HashSet<int> currentSolution)
        {
            if (currentRow == _n)
            {
                _completeSolutions.Add(new(currentSolution));
                return;
            }

            bool isQueenPlaceable = false;
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    isQueenPlaceable = board[i, j] == 0;
                    if (isQueenPlaceable) break;
                }

                if (isQueenPlaceable) break;
            }

            if (!isQueenPlaceable) return;

            for (int j = 0; j < _n; j++)
            {
                if (board[currentRow, j] != 0) continue;

                int hash = (currentRow << 4) + j;
                currentSolution.Add(hash);

                SolveNQueens(PlaceQueen(board, currentRow, j), currentRow + 1, currentSolution);

                currentSolution.Remove(hash);
            }
        }

        public int[,] PlaceQueen(int[,] board, int i, int j)
        {
            int[,] resultBoard = new int[_n, _n];
            Array.Copy(board, resultBoard, board.Length);

            for (int x = 0; x < _n; x++)
            {
                resultBoard[i, x] = resultBoard[i, x] == 0 ? -1 : resultBoard[i, x];
            }

            for (int y = 0; y < _n; y++)
            {
                resultBoard[y, j] = resultBoard[y, j] == 0 ? -1 : resultBoard[y, j];
            }

            for (int x = j, y = i; (x >= 0) && (y >= 0); x--, y--)
            {
                resultBoard[y, x] = resultBoard[y, x] == 0 ? -1 : resultBoard[y, x];
            }

            for (int x = j, y = i; (x < _n) && (y >= 0); x++, y--)
            {
                resultBoard[y, x] = resultBoard[y, x] == 0 ? -1 : resultBoard[y, x];
            }

            for (int x = j, y = i; (x < _n) && (y < _n); x++, y++)
            {
                resultBoard[y, x] = resultBoard[y, x] == 0 ? -1 : resultBoard[y, x];
            }

            for (int x = j, y = i; (x >= 0) && (y < _n); x--, y++)
            {
                resultBoard[y, x] = resultBoard[y, x] == 0 ? -1 : resultBoard[y, x];
            }

            resultBoard[i, j] = 1;
            return resultBoard;
        }
    }
}