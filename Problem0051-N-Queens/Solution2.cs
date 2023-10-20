namespace Problem0051_N_Queens_2
{
    public static class SolutionRunner
    {
        public static void Run()
        {
            for (int i = 1; i <= 9; i++)
            {
                new Solution().SolveNQueens(i, out int c);
                Console.WriteLine(c);
            }
        }
    }

    public class Solution
    {
        private List<int[,]> _allSolutions = new();
        private int _n;

        public IList<IList<string>> SolveNQueens(int n, out int c)
        {
            _n = n;
            _allSolutions = new();
            GetAllSolutions(new int[n, n], n);
            c = _allSolutions.Count;
            return new List<IList<string>>();
        }

        public void GetAllSolutions(int[,] board, int queensLeft)
        {
            if (queensLeft == 0)
            {
                int[,] newBoard = new int[_n, _n];
                Array.Copy(board, newBoard, board.Length);
                _allSolutions.Add(newBoard);
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

            if (!isQueenPlaceable) return;

            // +1 - queen
            // -1 - contested square
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _n; j++)
                {
                    if (board[i, j] == 0)
                    {
                        if (IsBoardDifferent(board, i, j))
                        {
                            PlaceQueenCommand command = new();
                            command.Do(board, i, j);
                            GetAllSolutions(board, queensLeft - 1);
                            command.Undo(board);
                        }


                        //allSolutions.AddRange(GetAllSolutions(board, _n - 1));

                    }
                }
            }

            return;
        }

        public bool IsBoardDifferent(int[,] board, int i1, int j1)
        {
            bool isBoardDifferent = true;

            board[i1, j1] = 1;

            foreach (int[,] solution in _allSolutions)
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
        }
    }

    public class PlaceQueenCommand
    {
        private int[,] _board = new int[0, 0];

        public void Cache(int[,] board)
        {
            int n = board.GetLength(0);
            _board = new int[n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    _board[i, j] = board[i, j];
                }
            }
        }

        public void Do(int[,] board, int i, int j)
        {
            Cache(board);

            for (int x = 0; x < board.GetLength(0); x++)
            {
                board[i, x] = board[i, x] == 0 ? -1 : board[i, x];
            }

            for (int y = 0; y < board.GetLength(0); y++)
            {
                board[y, j] = board[y, j] == 0 ? -1 : board[y, j];
            }

            for (int x = j, y = i; (x >= 0) && (y >= 0); x--, y--)
            {
                board[y, x] = board[y, x] == 0 ? -1 : board[y, x];
            }

            for (int x = j, y = i; (x < board.GetLength(0)) && (y >= 0); x++, y--)
            {
                board[y, x] = board[y, x] == 0 ? -1 : board[y, x];
            }

            for (int x = j, y = i; (x < board.GetLength(0)) && (y < board.GetLength(0)); x++, y++)
            {
                board[y, x] = board[y, x] == 0 ? -1 : board[y, x];
            }

            for (int x = j, y = i; (x >= 0) && (y < board.GetLength(0)); x--, y++)
            {
                board[y, x] = board[y, x] == 0 ? -1 : board[y, x];
            }

            board[i, j] = 1;
        }

        public void Undo(int[,] board)
        {
            int n = board.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    board[i, j] = _board[i, j];
                }
            }
        }
    }
}