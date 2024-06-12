using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisCs;

public class Blocks
{
    public static Position pos(int a, int b) => new(a, b);
    public static int rot(int a, int b) => ((a + 1) * 10) + (b + 1);
    public static Position kick(int a, int b) => new(b * -1, a);
    public static int center = ((Def.Columns / 2) - 2);

    public class LBlock : Block
    {
        public LBlock()
        {
            id = 1; //green
            cells[0] = [pos(0, 2), pos(1, 0), pos(1, 1), pos(1, 2)];
            cells[1] = [pos(0, 1), pos(1, 1), pos(2, 1), pos(2, 2)];
            cells[2] = [pos(1, 0), pos(1, 1), pos(1, 2), pos(2, 0)];
            cells[3] = [pos(0, 0), pos(0, 1), pos(1, 1), pos(2, 1)];
            Move(0, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
        }
    }
    public class JBlock : Block
    {
        public JBlock()
        {
            id = 2; //red
            cells[0] = [pos(0, 0), pos(1, 0), pos(1, 1), pos(1, 2)];
            cells[1] = [pos(0, 1), pos(0, 2), pos(1, 1), pos(2, 1)];
            cells[2] = [pos(1, 0), pos(1, 1), pos(1, 2), pos(2, 2)];
            cells[3] = [pos(0, 1), pos(1, 1), pos(2, 0), pos(2, 1)];
            Move(0, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
        }
    }

    public class IBlock : Block
    {
        public IBlock()
        {
            id = 3; //orange
            cells[0] = [pos(1, 0), pos(1, 1), pos(1, 2), pos(1, 3)];
            cells[1] = [pos(0, 2), pos(1, 2), pos(2, 2), pos(3, 2)];
            cells[2] = [pos(2, 0), pos(2, 1), pos(2, 2), pos(2, 3)];
            cells[3] = [pos(0, 1), pos(1, 1), pos(2, 1), pos(3, 1)];
            Move(-1, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-2, 0), kick(+1, 0), kick(-2, +1), kick(+1, -2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+2, 0), kick(-1, 0), kick(+2, -1), kick(-1, +2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(-1, 0), kick(+2, 0), kick(-1, -2), kick(+2, +1)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(+1, 0), kick(-2, 0), kick(+1, +2), kick(-2, -1)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+2, 0), kick(-1, 0), kick(+2, -1), kick(-1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-2, 0), kick(+1, 0), kick(-2, +1), kick(+1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(+1, 0), kick(-2, 0), kick(+1, +2), kick(-2, -1)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(-1, 0), kick(+2, 0), kick(-1, -2), kick(+2, +1)];

        }
    }

    public class OBlock : Block
    {
        public OBlock()
        {
            id = 4; //yellow
            cells[0] = [pos(0, 0), pos(0, 1), pos(1, 0), pos(1, 1)];
            //cells[1] = [ pos(0,0), pos(0,1), pos(1,0), pos(1,1) ];
            //cells[2] = [ pos(0,0), pos(0,1), pos(1,0), pos(1,1) ];
            //cells[3] = [ pos(0,0), pos(0,1), pos(1,0), pos(1,1) ];
            Move(0, center + 1);

            wallkick[rot(0, 1)] = [kick(0, 0)];
            wallkick[rot(1, 0)] = [kick(0, 0)];
            wallkick[rot(1, 2)] = [kick(0, 0)];
            wallkick[rot(2, 1)] = [kick(0, 0)];
            wallkick[rot(2, 3)] = [kick(0, 0)];
            wallkick[rot(3, 2)] = [kick(0, 0)];
            wallkick[rot(3, 0)] = [kick(0, 0)];
            wallkick[rot(0, 3)] = [kick(0, 0)];
        }
    }

    public class SBlock : Block
    {
        public SBlock()
        {
            id = 5; //purple
            cells[0] = [pos(0, 1), pos(0, 2), pos(1, 0), pos(1, 1)];
            cells[1] = [pos(0, 1), pos(1, 1), pos(1, 2), pos(2, 2)];
            cells[2] = [pos(1, 1), pos(1, 2), pos(2, 0), pos(2, 1)];
            cells[3] = [pos(0, 0), pos(1, 0), pos(1, 1), pos(2, 1)];
            Move(0, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
        }
    }

    public class TBlock : Block
    {
        public TBlock()
        {
            id = 6; //cyan
            cells[0] = [pos(0, 1), pos(1, 0), pos(1, 1), pos(1, 2)];
            cells[1] = [pos(0, 1), pos(1, 1), pos(1, 2), pos(2, 1)];
            cells[2] = [pos(1, 0), pos(1, 1), pos(1, 2), pos(2, 1)];
            cells[3] = [pos(0, 1), pos(1, 0), pos(1, 1), pos(2, 1)];
            Move(0, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
        }
    }

    public class ZBlock : Block
    {
        public ZBlock()
        {
            id = 7; //blue
            cells[0] = [pos(0, 0), pos(0, 1), pos(1, 1), pos(1, 2)];
            cells[1] = [pos(0, 2), pos(1, 1), pos(1, 2), pos(2, 1)];
            cells[2] = [pos(1, 0), pos(1, 1), pos(2, 1), pos(2, 2)];
            cells[3] = [pos(0, 1), pos(1, 0), pos(1, 1), pos(2, 0)];
            Move(0, center);

            wallkick[rot(0, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(1, 0)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(1, 2)] = [kick(0, 0), kick(+1, 0), kick(+1, +1), kick(0, -2), kick(+1, -2)];
            wallkick[rot(2, 1)] = [kick(0, 0), kick(-1, 0), kick(-1, -1), kick(0, +2), kick(-1, +2)];
            wallkick[rot(2, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
            wallkick[rot(3, 2)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(3, 0)] = [kick(0, 0), kick(-1, 0), kick(-1, +1), kick(0, -2), kick(-1, -2)];
            wallkick[rot(0, 3)] = [kick(0, 0), kick(+1, 0), kick(+1, -1), kick(0, +2), kick(+1, +2)];
        }
    }
}