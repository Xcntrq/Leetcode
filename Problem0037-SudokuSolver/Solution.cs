namespace Problem0037_SudokuSolver
{
    public class Solution
    {
        public void SolveSudoku(char[][] board) => SolveSudoku(new Sudoku(board)).TryToShit(ref board);

        public Sudoku SolveSudoku(Sudoku sudoku)
        {
            while (sudoku.PropagateDefinedCells() > 0) ;

            SudokuState sudokuState = sudoku.GetState();

            if ((sudokuState == SudokuState.Failed) || (sudokuState == SudokuState.Solved))
            {
                return sudoku;
            }

            Sudoku sudokuAfterMove = sudoku;
            if (sudoku.TryGetUndefinedCell(out Cell undefinedCell))
            {
                foreach (int p in undefinedCell._possibilities)
                {
                    sudokuAfterMove = sudoku.DefineCell(undefinedCell._x, undefinedCell._y, p);
                    sudokuAfterMove = SolveSudoku(sudokuAfterMove);
                    if (sudokuAfterMove.GetState() == SudokuState.Solved)
                    {
                        return sudokuAfterMove;
                    }
                }
            }

            return sudokuAfterMove;
        }
    }

    public enum SudokuState
    {
        Failed,
        Solved,
        InProgress,
    }

    public class Sudoku
    {
        public Cell[,] _cells;
        public char[][] _shit;
        public SudokuState _sudokuState;

        public Dictionary<int, HashSet<Cell>> _rows;
        public Dictionary<int, HashSet<Cell>> _cols;
        public Dictionary<int, HashSet<Cell>> _sectors;

        public Sudoku(char[][] shit)
        {
            _shit = shit;
            _cells = new Cell[9, 9];
            int[] newCell = new int[9] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            _rows = new Dictionary<int, HashSet<Cell>>();
            _cols = new Dictionary<int, HashSet<Cell>>();
            _sectors = new Dictionary<int, HashSet<Cell>>();

            for (int i = 0; i < 9; i++)
            {
                _rows[i] = new HashSet<Cell>();
                _cols[i] = new HashSet<Cell>();
                _sectors[i] = new HashSet<Cell>();
            }

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    char ch = shit[y][x];
                    if (ch == '.')
                    {
                        _cells[x, y] = new Cell(x, y, newCell);
                    }
                    else
                    {
                        int value = (int)Char.GetNumericValue(shit[y][x]);
                        _cells[x, y] = new Cell(x, y, new int[] { value });
                    }

                    _rows[y].Add(_cells[x, y]);
                    _cols[x].Add(_cells[x, y]);
                    int sectorIndex = y / 3 * 3 + x / 3;
                    _sectors[sectorIndex].Add(_cells[x, y]);
                }
            }

            _sudokuState = SudokuState.InProgress;
        }

        public Sudoku(Sudoku sudoku)
        {
            _shit = sudoku._shit;
            _cells = new Cell[9, 9];
            _rows = new Dictionary<int, HashSet<Cell>>();
            _cols = new Dictionary<int, HashSet<Cell>>();
            _sectors = new Dictionary<int, HashSet<Cell>>();

            for (int i = 0; i < 9; i++)
            {
                _rows[i] = new HashSet<Cell>();
                _cols[i] = new HashSet<Cell>();
                _sectors[i] = new HashSet<Cell>();
            }

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    _cells[x, y] = new(sudoku._cells[x, y]);
                    _rows[y].Add(_cells[x, y]);
                    _cols[x].Add(_cells[x, y]);
                    int sectorIndex = y / 3 * 3 + x / 3;
                    _sectors[sectorIndex].Add(_cells[x, y]);
                }
            }

            _sudokuState = sudoku._sudokuState;
        }

        public static int GetSectorIndex(int x, int y)
        {
            return y / 3 * 3 + x / 3;
        }

        public SudokuState GetState()
        {
            bool areAllCellsDefined = true;
            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (_cells[x, y].GetLength() != 1)
                    {
                        areAllCellsDefined = false;
                    }
                    else if (_cells[x, y].GetLength() == 0)
                    {
                        _sudokuState = SudokuState.Failed;
                        return _sudokuState;
                    }
                }
            }

            if (areAllCellsDefined)
            {
                _sudokuState = SudokuState.Solved;
            }
            else
            {
                _sudokuState = SudokuState.InProgress;
            }

            return _sudokuState;
        }

        public bool TryToShit(ref char[][] result)
        {
            char[][] possibleResult = _shit;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (_cells[x, y].TryGetChar(out char ch))
                    {
                        _shit[y][x] = ch;
                    }
                    else
                    {
                        result = _shit;
                        return false;
                    }
                }
            }

            result = possibleResult;
            return true;
        }

        public int PropagateDefinedCells()
        {
            int changed = 0;

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    Cell currentCell = _cells[x, y];
                    if (currentCell.IsDefined)
                    {
                        var involvedCells = new HashSet<Cell>();
                        involvedCells.UnionWith(_rows[y]);
                        involvedCells.UnionWith(_cols[x]);
                        involvedCells.UnionWith(_sectors[GetSectorIndex(x, y)]);
                        involvedCells.Remove(currentCell);

                        if (currentCell.TryGetFirst(out int p))
                        {
                            foreach (Cell cell in involvedCells)
                            {
                                if (cell.TryRemovePossibility(p))
                                {
                                    changed++;
                                }
                            }
                        }
                    }
                }
            }

            return changed;
        }

        public bool TryGetUndefinedCell(out Cell result)
        {
            result = new Cell(0, 0, Array.Empty<int>());

            for (int y = 0; y < 9; y++)
            {
                for (int x = 0; x < 9; x++)
                {
                    if (!_cells[x, y].IsDefined)
                    {
                        result = _cells[x, y];
                        return true;
                    }
                }
            }

            return false;
        }

        public Sudoku DefineCell(int x, int y, int p)
        {
            var result = new Sudoku(this);
            result._cells[x, y] = new Cell(x, y, new int[] { p });
            return result;
        }
    }

    public class Cell
    {
        public int _x;
        public int _y;
        public HashSet<int> _possibilities;

        public Cell(int x, int y, int[] possibilities)
        {
            _x = x;
            _y = y;
            _possibilities = new HashSet<int>(possibilities);
        }

        public Cell(Cell cell)
        {
            _x = cell._x;
            _y = cell._y;
            _possibilities = new HashSet<int>(cell._possibilities);
        }

        public bool TryGetFirst(out int p)
        {
            if (_possibilities.Count > 0)
            {
                p = _possibilities.First();
                return true;
            }
            else
            {
                p = 0;
                return false;
            }
        }

        public bool IsDefined
        {
            get { return _possibilities.Count == 1; }
        }

        public bool TryRemovePossibility(int p)
        {
            if (_possibilities.Contains(p))
            {
                _possibilities.Remove(p);
                return true;
            }
            return false;
        }

        public int GetLength()
        {
            return _possibilities.Count;
        }

        public bool TryGetChar(out char ch)
        {
            if (_possibilities.Count == 1)
            {
                ch = (char)(_possibilities.First() + 48);
                return true;
            }

            ch = '.';
            return false;
        }
    }
}
