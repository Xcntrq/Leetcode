﻿namespace Problem0037_SudokuSolver
{
    internal class Program
    {
        static void Main()
        {
            char[][] board2 = new char[][]
            {
                new char[] { '.','.','9','7','4','8','.','.','.' },
                new char[] { '7','.','.','.','.','.','.','.','.' },
                new char[] {'.','2','.','1','.','9','.','.','.' },
                new char[] { '.','.','7','.','.','.','2','4','.' },
                new char[] { '.','6','4','.','1','.','5','9','.' },
                new char[] { '.','9','8','.','.','.','3','.','.' },
                new char[] {'.','.','.','8','.','3','.','2','.'},
                new char[] { '.','.','.','.','.','.','.','.','6' },
                new char[] { '.', '.', '.', '2', '7', '5', '9', '.', '.' }
            };

            char[][] board1 = new char[][]
            {
                new char[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' },
                new char[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' },
                new char[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' },
                new char[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' },
                new char[] { '4','.','.','8','.','3','.','.','1' },
                new char[] { '7','.','.','.','2','.','.','.','6' },
                new char[] { '.','6','.','.','.','.','2','8','.' },
                new char[] { '.','.','.','4','1','9','.','.','5' },
                new char[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' }
            };

            var solution = new Solution();
            solution.SolveSudoku(board2);
            solution.SolveSudoku(board1);

            if (board1[1][1] == '1')
            {
                board1[1][1] = '2';
            }
        }
    }
}