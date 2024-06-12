using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TetricCs;

public class Game
{
    public Game() 
    {
        grid = new Grid();
        gameOver = false;
        blocks = GetAllBlocks();
        currentBlock = GetRandomBlock();
        ApplyShadow();
        nextBlock = GetRandomBlock();
        Raylib.InitAudioDevice();
        music = Raylib.LoadMusicStream("Assets/Audio/Tetris.mp3");
        Raylib.PlayMusicStream(music);

        rotateSound = Raylib.LoadSound("Assets/Audio/rotate.wav");
        clearSound = Raylib.LoadSound("Assets/Audio/clear.wav");
        loseSound = Raylib.LoadSound("Assets/Audio/lose.wav");
        winSound = Raylib.LoadSound("Assets/Audio/win.wav");
        dropSound = Raylib.LoadSound("Assets/Audio/drop.wav");
        lockSound = Raylib.LoadSound("Assets/Audio/lock.wav");
        cantSound = Raylib.LoadSound("Assets/Audio/cant.wav");
        clickSound = Raylib.LoadSound("Assets/Audio/click.wav");
    }

    ~Game() 
    {
        Raylib.UnloadSound(rotateSound);
        Raylib.UnloadSound(clearSound);
        Raylib.UnloadSound(loseSound);
        Raylib.UnloadSound(winSound);
        Raylib.UnloadSound(dropSound);
        Raylib.UnloadSound(lockSound);
        Raylib.UnloadSound(cantSound);
        Raylib.UnloadSound(clickSound);
        Raylib.UnloadMusicStream(music);
        Raylib.CloseAudioDevice();
    }
    public bool gameOver;
    public bool paused;
    public int score;
    public Music music;

    private Grid grid;
    private List<Block> blocks;
    private Block currentBlock;
    private Block currentBlockShadow;
    private Block nextBlock;

    private Sound rotateSound;
    private Sound clearSound;
    private Sound loseSound;
    private Sound winSound;
    private Sound dropSound;
    private Sound lockSound;
    private Sound cantSound;
    private Sound clickSound;
    public void Draw()
    {
        grid.Draw();
        currentBlock.Draw(0, 0);
        currentBlockShadow.Draw(0, 0);
        switch (nextBlock.id)
        {
            case 3: //i block
                nextBlock.DrawUI(((Def.Columns - 2) * Def.CellSize) - 5, (6 * Def.CellSize) - 5);
                break;
            case 4: //o block
                nextBlock.DrawUI(((Def.Columns - 2) * Def.CellSize) - 5, (5 * Def.CellSize) + 10);
                break;
            default:
                nextBlock.DrawUI(((Def.Columns - 2) * Def.CellSize) + 10, (5 * Def.CellSize) + 10);
                break;
        }
    }
    public void HandleInput()
    {
        KeyboardKey keyPressed = (KeyboardKey)Raylib.GetKeyPressed();

        if (keyPressed == KeyboardKey.P)
        {
            paused = !paused;
        }
        if (keyPressed == KeyboardKey.U)
        {
            Reset();
        }

        if (gameOver || paused)
        {
            return;
        }

        switch (keyPressed)
        {
            case KeyboardKey.Left:
                MoveBlockLeft();
                break;
            case KeyboardKey.Right:
                MoveBlockRight();
                break;
            //case KEY_DOWN:
            //	UpdateScore(0,1);
            //	MoveBlockDown();
            //	break;
            case KeyboardKey.Space:
                DropBlockDown();
                break;

            case KeyboardKey.Q:
                RotateLeft();
                break;
            case KeyboardKey.E:
            case KeyboardKey.Up:
                RotateRight();
                break;

            default:
                break;
        }
    }
    public void HandleMovement()
    {
        if (gameOver || paused)
        {
            return;
        }
        if (Raylib.IsKeyDown(KeyboardKey.LeftControl) || Raylib.IsKeyDown(KeyboardKey.RightControl) || 
            Raylib.IsKeyDown(KeyboardKey.RightShift) || Raylib.IsKeyDown(KeyboardKey.LeftShift))
        {
            if (Raylib.IsKeyDown(KeyboardKey.Left))
            {
                MoveBlockLeft();
                Raylib.PlaySound(clickSound);
            }
            if (Raylib.IsKeyDown(KeyboardKey.Right))
            {
                MoveBlockRight();
                Raylib.PlaySound(clickSound);
            }
        }
        if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            UpdateScore(0, 1);
            MoveBlockDown();
            Raylib.PlaySound(clickSound);
        }
    }
    public void MoveBlockDown()
    {
        if (gameOver || paused)
        {
            return;
        }
        Move(1, 0);
        if (IsBlockOutside(currentBlock) || !BlockFits(currentBlock))
        {
            Move(-1, 0);
            LockBlock();
        }
    }
    public void DropBlockDown()
    {
        if (gameOver || paused)
        {
            return;
        }
        DropShadow(); //drop the shadow again to make sure it is up to date

        //calculate the distance it will drop
        int dropDistance = currentBlockShadow.rowOffset - currentBlock.rowOffset;

        Raylib.PlaySound(dropSound);
        //drop the block by overwriting its y value
        currentBlock.rowOffset = currentBlockShadow.rowOffset;

        //update the socre by twice the dropdistance because you did it instantly
        UpdateScore(0, dropDistance * 2);
    }
    private Block GetRandomBlock()
    {
        if (!blocks.Any())
        {
            blocks = GetAllBlocks();
        }
        int randomIndex = Raylib.GetRandomValue(0, blocks.Count() - 1);
        Block block = blocks[randomIndex];
        blocks.RemoveAt(randomIndex);
        return block;
    }
    private List<Block> GetAllBlocks()
    {
        return [new Blocks.IBlock(), new Blocks.JBlock(), new Blocks.LBlock(),
                new Blocks.OBlock(), new Blocks.SBlock(), new Blocks.TBlock(), new Blocks.ZBlock()];
    }
    private void MoveBlockLeft()
    {
        if (gameOver || paused)
        {
            return;
        }
        Move(0, -1);
        if (IsBlockOutside(currentBlock) || !BlockFits(currentBlock))
        {
            Move(0, 1);
            Raylib.PlaySound(cantSound);
        }
    }
    private void MoveBlockRight()
    {
        if (gameOver || paused)
        {
            return;
        }
        Move(0, 1);
        if (IsBlockOutside(currentBlock) || !BlockFits(currentBlock))
        {
            Move(0, -1);
            Raylib.PlaySound(cantSound);
        }
    }
    private void RotateLeft()
    {
        if (gameOver || paused)
        {
            return;
        }
        bool success = SimpleRotateLeft(currentBlock);
        //bool success = SRSRotateLeft(currentBlock);
        if (success)
        {
            Raylib.PlaySound(rotateSound);
            currentBlockShadow.RotateLeft();
            DropShadow();
        }
        else
        {
            Raylib.PlaySound(cantSound);
        }
    }
    private void RotateRight()
    {
        if (gameOver || paused)
        {
            return;
        }
        bool success = SimpleRotateRight(currentBlock);
        //bool success = SRSRotateRight(currentBlock);
        if (success)
        {
            Raylib.PlaySound(rotateSound);
            currentBlockShadow.RotateRight();
            DropShadow();
        }
        else
        {
            Raylib.PlaySound(cantSound);
        }
    }

    private bool SimpleRotateLeft(Block Block)
    {
        Block.RotateLeft();
        if (IsBlockOutside(Block) || !BlockFits(Block))
        {
            Block.RotateRight();
            return false;
        }
        return true;
    }
    private bool SimpleRotateRight(Block Block)
    {
        Block.RotateRight();
        if (IsBlockOutside(Block) || !BlockFits(Block))
        {
            Block.RotateLeft();
            return false;
        }
        return true;
    }
    private bool SRSRotateLeft(Block Block)
    {
        int rot(int a, int b) => ((a + 1) * 10) + (b + 1);


        int start = Block.rotationState;
        int end = start;
        end--;
        if (end == -1)
        {
            end = (int)Block.cells.Count() - 1;
        }

        var kick = rot(start, end);
        var kickdata = Block.wallkick[kick];

        int orgRow = Block.rowOffset;
        int orgColumn = Block.colummnOffset;

        Block.RotateLeft();
        bool success = false;
        //int tries = 0;
        foreach (Position offset in kickdata)
        {
            //tries++;
            Block.Move(offset.row, offset.column);
            if (IsBlockOutside(Block) || !BlockFits(Block))
            {
                Block.rowOffset = orgRow;
                Block.colummnOffset = orgColumn;
                continue;
            }
            else
            {
                success = true;
                break;
            }
        }
        //std::cout << "it took " << tries << " tries" << std::endl;

        if (!success)
        {
            Block.RotateRight();
        }

        return success;
    }
    private bool SRSRotateRight(Block Block)
    {
        int rot(int a, int b) => ((a + 1) * 10) + (b + 1);

        int start = Block.rotationState;
        int end = start;
        end++;
        if (end == (int)Block.cells.Count())
        {
            end = 0;
        }

        var kick = rot(start, end);
        var kickdata = Block.wallkick[kick];

        int orgRow = Block.rowOffset;
        int orgColumn = Block.colummnOffset;

        Block.RotateRight();
        bool success = false;
        foreach (Position offset in kickdata)
        {
            //tries++;
            Block.Move(offset.row, offset.column);
            if (IsBlockOutside(Block) || !BlockFits(Block))
            {
                Block.rowOffset = orgRow;
                Block.colummnOffset = orgColumn;
                continue;
            }
            else
            {
                success = true;
                break;
            }
        }
        //std::cout << "it took " << tries << " tries" << std::endl;

        if (!success)
        {
            Block.RotateLeft();
        }


        return success;
    }

    private void Move(int rows, int columns)
    {
        currentBlock.Move(rows, columns);
        DropShadow();
    }

    private bool IsBlockOutside(Block Block)
    {
        Position[] tiles = currentBlock.GetCellPositions();
        foreach (Position item in tiles)
        {
            if (grid.IsCellOutside(item.row, item.column))
            {
                return true;
            }
        }
        return false;
    }
    private bool BlockFits(Block Block)
    {
        Position[] tiles = currentBlock.GetCellPositions();
        foreach (Position item in tiles)
        {
            if (!grid.IsCellEmpty(item.row, item.column))
            {
                return false;
            }
        }
        return true;
    }
    private void LockBlock()
    {
        Position[] tiles = currentBlock.GetCellPositions();
        foreach (Position item in tiles)
        {
            grid.grid[item.row,item.column] = currentBlock.id;
        }

        currentBlock = nextBlock;
        ApplyShadow();
        if (!BlockFits(currentBlock))
        {
            gameOver = true;
            Raylib.StopMusicStream(music);
            Raylib.PlaySound(loseSound);
        }
        Raylib.PlaySound(lockSound);
        nextBlock = GetRandomBlock();

        int rowsCleared = grid.ClearFullRows();
        UpdateScore(rowsCleared, 0);
        if (rowsCleared > 2)
        {
            Raylib.PlaySound(winSound);
            Raylib.PlaySound(clearSound);
        }
        else if (rowsCleared > 0)
        {
            Raylib.PlaySound(clearSound);
        }
    }
    private void Reset()
    {
        grid.Initialize();
        gameOver = false;
        paused = false;
        blocks = GetAllBlocks();
        currentBlock = GetRandomBlock();
        nextBlock = GetRandomBlock();
        ApplyShadow();
        score = 0;
        Raylib.PlayMusicStream(music);
    }
    private void UpdateScore(int linesCleared, int moveDownPoints)
    {
        switch (linesCleared)
        {
            case 1:
                score += 100;
                break;
            case 2:
                score += 300;
                break;
            case 3:
                score += 500;
                break;
            case 4:
                score += 1000;
                break;
            default:
                break;
        }

        score += moveDownPoints;
    }
    private void ApplyShadow()
    {
        //set the shadow to be the same type of block as currentBlock
        currentBlockShadow = new Block(currentBlock);
        currentBlockShadow.id += 8; //add 8 to the id to give it a darker color
        DropShadow();
    }

    void DropShadow()
    {
        return;
        //set the shadow to the exact position as the block
        currentBlockShadow.rowOffset = currentBlock.rowOffset;
        currentBlockShadow.colummnOffset = currentBlock.colummnOffset;

        //keep moving the shadow down until it hit something
        while (!IsBlockOutside(currentBlockShadow) && BlockFits(currentBlockShadow))
        {
            currentBlockShadow.Move(1, 0);
        }
        //move it back up one to unclip it
        currentBlockShadow.Move(-1, 0);
    }
}
