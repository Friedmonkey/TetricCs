using Raylib_cs;
using System.Text;
using TetrisCs;
using Vector2 = System.Numerics.Vector2;

namespace TetrisCs;

public static class Program 
{
    public static double lastGameTickTime = 0;
    public static double lastMovementTickTime = 0;

    public static bool TickGameSpeed(double interval)
    {
        double currentTime = Raylib.GetTime();

        if (currentTime - lastGameTickTime >= interval)
        {
            lastGameTickTime = currentTime;
            return true;
        }
        return false;
    }

    public static bool TickMovementSpeed(double interval)
    {
        double currentTime = Raylib.GetTime();

        if (currentTime - lastMovementTickTime >= interval)
        {
            lastMovementTickTime = currentTime;
            return true;
        }
        return false;
    }
    public static int sidepanel = ((Def.Columns+1) * Def.CellSize) - 10;
    public static void Main() 
    {
        Raylib.InitWindow(Def.SW + 200, Def.SH + 20, "Tetris game lol");
        var (iconExt, iconBytes) = ResourceLoader.GetMemoryLoader("Assets/Images/tetris.png");
        Image icon = Raylib.LoadImageFromMemory(iconExt, iconBytes);
        //Image icon = Raylib.LoadImage("Assets/Images/tetris.png");
        Raylib.SetWindowIcon(icon);
        Raylib.SetTargetFPS(60);

        var (fontExt, fontBytes) = ResourceLoader.GetMemoryLoader("Assets/Fonts/block.ttf");
        Font font = Raylib.LoadFontFromMemory(fontExt, fontBytes, 64, null, 0);
        //Font font = Raylib.LoadFontEx("Assets/Fonts/block.ttf", 64, null, 0);
        Game game = new Game();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.UpdateMusicStream(game.music);
            // updating

            game.HandleInput();

            if (TickGameSpeed(0.2))
            {
                game.MoveBlockDown();
            }

            if (TickMovementSpeed(0.05))
            {
                game.HandleMovement();
            }

            Raylib.BeginDrawing();
            // drawing
            Raylib.ClearBackground(Def.DARKBLUE);
            //score text
            Raylib.DrawTextEx(font, "Score", new( sidepanel, 15), 25, 2, Color.White);

            //score field
            Rectangle rect = new(sidepanel - Def.GapSize, 55, 170, 60);
            Raylib.DrawRectangleRounded(rect, 0.3f , 6, Def.LIGHTBLUE);

            string scoreStr = game.score.ToString();
            Raylib.DrawTextEx(font, scoreStr, new(sidepanel, 80), 25, 2, Color.White);

            //Next text
            Raylib.DrawTextEx(font, "Next", new( sidepanel, 130 ), 25, 2, Color.White);

            //next field
            Rectangle rect2 = new( sidepanel - Def.GapSize, 160, 170, 150 );
            Raylib.DrawRectangleRounded(rect2, 0.3f, 6, Def.LIGHTBLUE);

            if (game.gameOver)
            {
                //Gameover text
                Raylib.DrawTextEx(font, "Game over", new ( sidepanel, 325 ), 25, 2, Color.White);
                Raylib.DrawTextEx(font, "Press \"U\"", new ( sidepanel, 350 ), 25, 2, Color.White);
                Raylib.DrawTextEx(font, "to restart", new ( sidepanel, 375 ), 25, 2, Color.White);
            }


            game.Draw();

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}