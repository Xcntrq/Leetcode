namespace Problem0051_N_Queens_3
{
    public static class SolutionRunner
    {
        public static void Run()
        {
            for (int i = 4; i <= 9; i++)
            {
                new Solution().SolveNQueens(i, out int c);
                Console.WriteLine($"n={i}: {c}");
            }
        }
    }

    public class Solution
    {
        private HashSet<string> _failedQueenPlacements = new();
        private HashSet<string> _completeSolutions = new();
        private int _n;

        public IList<IList<string>> SolveNQueens(int n, out int c)
        {
            _n = n;
            SolveNQueens(new int[n, n], n, new HashSet<int>());
            c = _completeSolutions.Count;
            return new List<IList<string>>();
        }

        public string HashSetToStr(HashSet<int> hashSet)
        {
            List<int> list = new List<int>(hashSet);
            list.Sort();
            string str = string.Empty;

            for (int i = 0; i < list.Count - 1; i++)
            {
                str += $"{list[i]},";
            }

            str += $"{list[list.Count - 1]}";
            return str;
        }

        public void SolveNQueens(int[,] board, int queensLeft, HashSet<int> currentSolution)
        {
            if (queensLeft == 0)
            {
                _completeSolutions.Add(HashSetToStr(currentSolution));
                return;
            }

            // 0 - available square
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

            if (!isQueenPlaceable)
            {
                _failedQueenPlacements.Add(HashSetToStr(currentSolution));
                return;
            }

            // +1 - queen
            // -1 - contested square
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (board[i, j] == 0)
                    {
                        int hash = (i << 4) + j;
                        currentSolution.Add(hash);
                        string str = HashSetToStr(currentSolution);
                        bool isGonnaFail = _failedQueenPlacements.Contains(str);

                        if (!isGonnaFail)
                        {
                            PlaceQueenCommand command = new(board, _n);
                            command.Do(i, j);
                            SolveNQueens(board, queensLeft - 1, currentSolution);
                            command.Undo(board);
                        }

                        currentSolution.Remove(hash);
                    }
                }
            }

            return;
        }

        /*public bool IsBoardDifferent(int[,] board, int i1, int j1)
        {
            bool isBoardDifferent = true;

            board[i1, j1] = 1;

            foreach (int[,] solution in null)
            {
                isBoardDifferent = false;

                for (int i2 = 0; i2 < _n; i2++)
                {
                    for (int j2 = 0; j2 < _n; j2++)
                    {
                        bool q1 = solution[i2, j2] == 1;
                        bool q2 = board[i2, j2] == 1;
                        bool isThereAQueen = q1 || q2;
                        bool areBothQueens = q1 && q2;
                        isBoardDifferent = isThereAQueen && !areBothQueens;
                        if (isBoardDifferent) break;
                    }

                    if (isBoardDifferent) break;
                }

                if (!isBoardDifferent) break;
            }

            board[i1, j1] = 0;

            return isBoardDifferent;
        }*/
    }

    public class PlaceQueenCommand
    {
        private int _n;
        private int[,] _board;
        private int[,] _boardCache;

        public PlaceQueenCommand(int[,] board, int n)
        {
            _n = n;
            _board = board;
            _boardCache = new int[n, n];
        }

        public void Do(int i, int j)
        {
            Array.Copy(_board, _boardCache, _board.Length);

            for (int x = 0; x < _n; x++)
            {
                _board[i, x] = _board[i, x] == 0 ? -1 : _board[i, x];
            }

            for (int y = 0; y < _n; y++)
            {
                _board[y, j] = _board[y, j] == 0 ? -1 : _board[y, j];
            }

            for (int x = j, y = i; (x >= 0) && (y >= 0); x--, y--)
            {
                _board[y, x] = _board[y, x] == 0 ? -1 : _board[y, x];
            }

            for (int x = j, y = i; (x < _n) && (y >= 0); x++, y--)
            {
                _board[y, x] = _board[y, x] == 0 ? -1 : _board[y, x];
            }

            for (int x = j, y = i; (x < _n) && (y < _n); x++, y++)
            {
                _board[y, x] = _board[y, x] == 0 ? -1 : _board[y, x];
            }

            for (int x = j, y = i; (x >= 0) && (y < _n); x--, y++)
            {
                _board[y, x] = _board[y, x] == 0 ? -1 : _board[y, x];
            }

            _board[i, j] = 1;
        }

        public void Undo(int[,] board)
        {
            board = _boardCache;
        }
    }
}