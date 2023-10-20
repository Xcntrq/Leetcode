namespace Problem0051_N_Queens_1
{
    public static class SolutionRunner
    {
        public static void Run()
        {
            new Solution().SolveNQueens(1, out int c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(2, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(3, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(4, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(5, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(6, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(7, out c);
            Console.WriteLine(c);
            /*new Solution().SolveNQueens(8, out c);
            Console.WriteLine(c);
            new Solution().SolveNQueens(9, out c);
            Console.WriteLine(c);*/
        }
    }

    public class Solution
    {
        public IList<IList<string>> SolveNQueens(int n, out int c)
        {
            List<int[,]> allSolutions = GetAllSolutions(new int[n, n], n);
            c = allSolutions.Count;
            return new List<IList<string>>();
        }

        public List<int[,]> GetAllSolutions(int[,] board, int n)
        {
            // n - number of queens to place
            if (n == 0)
            {
                int l = board.GetLength(0);
                int[,] newBoard = new int[l, l];

                for (int i = 0; i < l; i++)
                {
                    for (int j = 0; j < l; j++)
                    {
                        newBoard[i, j] = board[i, j];
                    }
                }

                return new() { newBoard };

            }


            // 0 - available square
            bool isQueenPlaceable = false;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == 0)
                    {
                        isQueenPlaceable = true;
                    }
                }
            }

            if (!isQueenPlaceable) return new();

            // +1 - queen
            // -1 - contested square

            List<int[,]> allSolutions = new();

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == 0)
                    {
                        if ((i == 0) && (j == 1))
                        {

                        }

                        PlaceQueenCommand command = new();
                        command.Do(board, i, j);

                        allSolutions.AddRange(GetAllSolutions(board, n - 1));

                        command.Undo(board);
                    }
                }
            }

            return allSolutions;
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