using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Cell_02 { None, X, O }

public class Board_02
{
    Cell_02[] cells;

    public Board_02()
    {
        cells = new Cell_02[9];

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = Cell_02.None;
        }
    }

    public bool UpdateCell(int row, int col, bool isPlayer)
    {
        int index = row * 3 + col;
        cells[index] = isPlayer ? Cell_02.X : Cell_02.O;
        return IsWinner(row, col);
    }

    private bool IsWinner(int row, int col)
    {
        Cell_02 mycell = cells[row * 3 + col];
        return IsHorizontal(row, mycell) || IsVertical(col, mycell) || IsDiagonal(row * 2 + col, mycell);
    }

    private bool IsHorizontal(int row, Cell_02 cell)
    {
        return Match(row * 3, cell) && Match(row * 3 + 1, cell) && Match(row * 3 + 2, cell);
    }

    private bool IsVertical(int col, Cell_02 cell)
    {
        return Match(col, cell) && Match(col + 3, cell) && Match(col + 6, cell);
    }

    bool IsDiagonal(int index, Cell_02 cell)
    {
        if (index == 0 || index == 4 || index == 8)
            return Match(0, cell) && Match(4, cell) && Match(8, cell);

        if (index == 2 || index == 4 || index == 6)
            return Match(2, cell) && Match(4, cell) && Match(6, cell);

        return false;
    }

    bool Match(int index, Cell_02 cell)
    {
        return cells[index] == cell;
    }
}
