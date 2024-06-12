using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCs;

public class Block
{
    public Block(Block source)
    {
        id = source.id;
        cells = new(source.cells);
        wallkick = new(source.wallkick);

        rotationState = source.rotationState;

        colummnOffset = source.colummnOffset;
        rowOffset = source.rowOffset;

        colors = Def.GetCellColors();
    }
    public Block() 
    {
        rotationState = 0;
        colors = Def.GetCellColors();

        id = 0;
        colummnOffset = 0;
        rowOffset = 0;
    }

    public int id;
    public Dictionary<int, Position[]> cells = new Dictionary<int, Position[]>();
    public Dictionary<int, Position[]> wallkick = new Dictionary<int, Position[]>();
    public int rotationState;

    public int rowOffset;
    public int colummnOffset;
    private Color[] colors;
    public void Draw(int offsetX, int offsetY)
    {
        Position[] tiles = GetCellPositions();
        foreach (Position item in tiles)
        {
            if (item.row < Def.BufferRows)
            {
                continue;
            }
            Raylib.DrawRectangle(
                item.column * Def.CellSize + Def.GapSize + Def.OffSet,
                (item.row - Def.BufferRows) * Def.CellSize + Def.GapSize + Def.OffSet,
                Def.CellSize - Def.GapSize,
                Def.CellSize - Def.GapSize,
                colors[id]
            );
        }
    }     
    public void DrawUI(int offsetX, int offsetY)
    {
        Position[] tiles = GetCellPositions();
        foreach (Position item in tiles)
        {
            Raylib.DrawRectangle(
                item.column * Def.CellSize + Def.GapSize + Def.OffSet + offsetX,
                item.row * Def.CellSize + Def.GapSize + Def.OffSet + offsetY,
                Def.CellSize - Def.GapSize,
                Def.CellSize - Def.GapSize,
                colors[id]
            );
        }
    }
    public void Move(int rows, int columns)
    {
        rowOffset += rows;
        colummnOffset += columns;
    }
    public Position[] GetCellPositions() 
    {
        Position[] tiles = cells[rotationState];
        List<Position> movedTiles = new List<Position>();
        foreach (Position item in tiles)
        {
            Position newPos = new Position(item.row + rowOffset, item.column + colummnOffset);
            movedTiles.Add(newPos);
        }
        return movedTiles.ToArray();
    }
    public void RotateRight()
    {
        rotationState++;
        if (rotationState == (int)cells.Count())
        {
            rotationState = 0;
        }
    } 
    public void RotateLeft()
    {
        rotationState--;
        if (rotationState == -1)
        {
            rotationState = (int)cells.Count() - 1;
        }
    }  
};
