using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCs;

public class Grid
{
    public Grid() 
    {
        Initialize();
        colors = Def.GetCellColors();
    }
    public int[,] grid = new int[Def.Rows, Def.Columns];
    private Color[] colors;

    public void Initialize()
    {
        // init the grid to contain full zeros
        for (int r = 0; r < Def.Rows; r++)
        {
            for (int c = 0; c < Def.Columns; c++)
            {
                grid[r,c] = 0;
            }
        }
    }
    public void Draw()
    {
        for (int r = Def.BufferRows; r < Def.Rows; r++)
        {
            for (int c = 0; c < Def.Columns; c++)
            {
                int cellValue = grid[r, c];
                Raylib.DrawRectangle(c * Def.CellSize + Def.GapSize + Def.OffSet, (r - Def.BufferRows) * Def.CellSize + Def.GapSize + Def.OffSet, Def.CellSize - Def.GapSize, Def.CellSize - Def.GapSize, colors[cellValue]);
            }
        }
    }
    public bool IsCellOutside(int row, int column)
    {
        if (row >= 0 && row < Def.Rows && column >= 0 && column < Def.Columns)
        {
            return false;
        }
        return true;
    }
    public bool IsCellEmpty(int row, int column)
    {
        if (grid[row,column] == 0)
        {
            return true;
        }
        return false;
    }
    public int ClearFullRows()
    {
        int completed = 0;
        for (int row = (Def.Rows - 1); row >= 0; row--)
        {
            if (IsRowFull(row))
            {
                ClearRow(row);
                completed++;
            }
            else if (completed > 0)
            {
                MoveRowDown(row, completed);
            }
        }
        return completed;
    }
    private bool IsRowFull(int row)
    {
        for (int column = 0; column < Def.Columns; column++)
        {
            if (grid[row, column] == 0)
            {
                return false;
            }
        }
        return true;
    }
    private void ClearRow(int row)
    {
        for (int column = 0; column < Def.Columns; column++)
        {
            grid[row, column] = 0;
        }
    }
    private void MoveRowDown(int row, int amount)
    {
        for (int column = 0; column < Def.Columns; column++)
        {
            grid[row + amount, column] = grid[row, column];
            grid[row, column] = 0;
        }
    }
}
