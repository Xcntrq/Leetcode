namespace Problem0051_N_Queens_5
{
    // i dont get why it fails n=9
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
        public class Board
        {
            public HashSet<int> Queens = new();
            public HashSet<int> Available = new();

            public Board(HashSet<int> defaultBoard)
            {
                Available.UnionWith(defaultBoard);
            }

            public Board(Board board)
            {
                Queens.UnionWith(board.Queens);
                Available.UnionWith(board.Available);
            }
        }

        private readonly HashSet<int> _defaultBoard = new();
        private readonly HashSet<int> _failedSquares = new();
        private readonly HashSet<string> _failedQueenPlacements = new();
        private readonly HashSet<string> _exploredQueenPlacements = new();
        private readonly HashSet<string> _completeSolutions = new();
        private int _n;

        public IList<IList<string>> SolveNQueens(int n, out int c)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _defaultBoard.Add(GetHash(i, j));
                }
            }

            _n = n;
            SolveNQueens(new Board(_defaultBoard), n, new SortedSet<int>());
            c = _completeSolutions.Count;
            return new List<IList<string>>();
        }

        public static int GetHash(int i, int j) => (i << 4) + j;

        public static string HashSetToStr(SortedSet<int> sortedSet)
        {
            string str = string.Empty;
            foreach (int hash in sortedSet)
            {
                str += $"{hash},";
            }

            return str;
        }

        public bool SolveNQueens(Board board, int queensLeft, SortedSet<int> currentSolution)
        {
            if (queensLeft == 0)
            {
                _exploredQueenPlacements.Add(HashSetToStr(currentSolution));
                _completeSolutions.Add(HashSetToStr(currentSolution));
                return true;
            }

            // 0 - available square
            if (board.Available.Count == 0)
            {
                _failedQueenPlacements.Add(HashSetToStr(currentSolution));
                return false;
            }

            // +1 - queen
            // -1 - contested square
            bool isSolvable = false;

            HashSet<int> availableSquares = new();
            availableSquares.UnionWith(board.Available);

            foreach (int hash in availableSquares)
            {
                if (_failedSquares.Contains(hash)) continue;

                currentSolution.Add(hash);
                string str = HashSetToStr(currentSolution);
                bool isGonnaFail = _failedQueenPlacements.Contains(str);
                bool isExplored = _exploredQueenPlacements.Contains(str);

                if (!isGonnaFail && !isExplored)
                {
                    PlaceQueenCommand command = new(board, _n);
                    command.Do(hash);
                    isSolvable |= SolveNQueens(board, queensLeft - 1, currentSolution);
                    command.Undo(out board);
                }

                currentSolution.Remove(hash);
            }

            if (isSolvable && currentSolution.Count > 0)
            {
                _exploredQueenPlacements.Add(HashSetToStr(currentSolution));
            }

            if (!isSolvable && currentSolution.Count > 0)
            {
                _failedQueenPlacements.Add(HashSetToStr(currentSolution));
            }

            if (!isSolvable && currentSolution.Count == 1)
            {
                _failedSquares.UnionWith(currentSolution);
            }

            // Console.WriteLine($"n={_n}, solutions: {_completeSolutions.Count}, dismissed: {_failedQueenPlacements.Count}");

            return isSolvable;
        }

        /*public bool IsSquareInSolutions(int hash)
        {
            foreach (HashSet<int> solution in _completeSolutions2)
            {
                if (solution.Contains(hash)) return true;
            }

            return false;
        }*/

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

        public class PlaceQueenCommand
        {
            private readonly int _n;
            private readonly Board _board;
            private readonly Board _boardCache;

            public PlaceQueenCommand(Board board, int n)
            {
                _n = n;
                _board = board;
                _boardCache = new(board);
            }

            public void Do(int hash)
            {
                int i = hash >> 4;
                int j = (hash << 28) >> 28;

                HashSet<int> minusOnes = new();

                for (int x = 0; x < _n; x++)
                {
                    minusOnes.Add(GetHash(i, x));
                }

                for (int y = 0; y < _n; y++)
                {
                    minusOnes.Add(GetHash(y, j));
                }

                for (int x = j, y = i; (x >= 0) && (y >= 0); x--, y--)
                {
                    minusOnes.Add(GetHash(y, x));
                }

                for (int x = j, y = i; (x < _n) && (y >= 0); x++, y--)
                {
                    minusOnes.Add(GetHash(y, x));
                }

                for (int x = j, y = i; (x < _n) && (y < _n); x++, y++)
                {
                    minusOnes.Add(GetHash(y, x));
                }

                for (int x = j, y = i; (x >= 0) && (y < _n); x--, y++)
                {
                    minusOnes.Add(GetHash(y, x));
                }

                _board.Available.ExceptWith(minusOnes);
                _board.Queens.Add(GetHash(i, j));
            }

            public void Undo(out Board board)
            {
                board = _boardCache;
            }
        }
    }
}