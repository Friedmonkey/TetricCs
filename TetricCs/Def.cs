using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

namespace TetricCs;

public class Def
{
    public static int BufferRows = 3;
    public static int Rows = (20 + BufferRows);
    public static int Columns = 10;

    public static int GapSize = 3;
    public static int OffSet = 10;
    
    public static int CellSize = 35;

    public static int SW = Columns * CellSize + GapSize;
    public static int SH = (Rows-BufferRows)*CellSize+GapSize; 
    
    
    
    public static Color LIGHTBLUE = new (59, 85, 162, 255);
    public static Color DARKBLUE = new (44, 44, 127, 255);
    
    public static int normal = 255;
    public static int shadow = 100;

    public static Color darkGrey_normal = new (26, 31, 40, normal);	    
    public static Color green_normal = new (47, 230, 23, normal);
    public static Color red_normal = new (232, 18, 18, normal);
    public static Color orange_normal = new (226, 116, 17, normal);	
    public static Color yellow_normal = new (237, 234, 4, normal);
    public static Color purple_normal = new (166, 0, 247, normal);
    public static Color cyan_normal = new (21, 204, 209, normal);
    public static Color blue_normal = new(13, 64, 216, normal);

    public static Color darkGrey_shadow = new(26, 31, 40, shadow); 
    public static Color green_shadow = new(47, 230, 23, shadow);
    public static Color red_shadow = new(232, 18, 18, shadow);
    public static Color orange_shadow = new(226, 116, 17, shadow);
    public static Color yellow_shadow = new(237, 234, 4, shadow);
    public static Color purple_shadow = new(166, 0, 247, shadow);
    public static Color cyan_shadow = new(21, 204, 209, shadow);
    public static Color blue_shadow = new(13, 64, 216, shadow);




    public static Color[] GetCellColors()
    {
        return new Color[]
        {
            darkGrey_normal, green_normal, red_normal, orange_normal, yellow_normal, purple_normal, cyan_normal, blue_normal,
	        darkGrey_shadow, green_shadow, red_shadow, orange_shadow, yellow_shadow, purple_shadow, cyan_shadow, blue_shadow
        };
    }
}
