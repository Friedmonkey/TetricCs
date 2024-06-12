using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCs;

public class Position
{
    public Position(int row, int column)
    { 
        this.row = row;
        this.column = column;
    }

    public int row = 0;
    public int column = 0;
}
