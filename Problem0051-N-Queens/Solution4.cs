namespace Problem0051_N_Queens_4
{
    public static class SolutionRunner
    {
        public static void Run(int s, int e)
        {
            for (int i = s; i <= e; i++)
            {
                new Solution().SolveNQueens(i, out int c);
                Console.WriteLine($"n={i}: {c}");
            }
        }
    }

    public class Solution
    {
        private readonly HashSet<string> _failedQueenPlacements = new();
        private readonly HashSet<string> _exploredQueenPlacements = new();
        private readonly HashSet<string> _completeSolutions = new();
        private int _n;

        public IList<IList<string>> SolveNQueens(int n, out int c)
        {
            _n = n;
            SolveNQueens(new int[n, n], n, new SortedSet<int>());
            c = _completeSolutions.Count;
            return new List<IList<string>>();
        }

        public static string HashSetToStr(SortedSet<int> sortedSet)
        {
            string str = string.Empty;
            foreach (int hash in sortedSet)
            {
                str += $"{hash},";
            }

            return str;
        }

        public bool SolveNQueens(int[,] board, int queensLeft, SortedSet<int> currentSolution)
        {
            if (queensLeft == 0)
            {
                _exploredQueenPlacements.Add(HashSetToStr(currentSolution));
                _completeSolutions.Add(HashSetToStr(currentSolution));
                return true;
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
                return false;
            }

            // +1 - queen
            // -1 - contested square
            bool isSolvable = false;
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
                        bool isExplored = _exploredQueenPlacements.Contains(str);

                        if (!isGonnaFail && !isExplored)
                        {
                            PlaceQueenCommand command = new(board, _n);
                            command.Do(i, j);
                            isSolvable |= SolveNQueens(board, queensLeft - 1, currentSolution);
                            command.Undo(board);
                        }

                        currentSolution.Remove(hash);
                    }
                }
            }

            if (isSolvable && currentSolution.Count > 0)
            {
                _exploredQueenPlacements.Add(HashSetToStr(currentSolution));
            }

            if (!isSolvable && currentSolution.Count > 0)
            {
                _failedQueenPlacements.Add(HashSetToStr(currentSolution));
            }

            // Console.WriteLine($"n={_n}, solutions: {_completeSolutions.Count}, dismissed: {_failedQueenPlacements.Count}");

            return isSolvable;
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
        private readonly int _n;
        private readonly int[,] _board;
        private readonly int[,] _boardCache;

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
            Array.Copy(_boardCache, board, _board.Length);
        }
    }
}