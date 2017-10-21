using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

 // a class for representing the Tetris playing grid
class TetrisGrid
{
    public static Color[,] gridTable;  //the array that contains all blocks in the grid, which all blocks except the currently controlled one plus the background or 'empty' blocks
    List<TetrisBlock> blockList; // list of the different tetris blocks
    TetrisBlock tetrisBlock;
    Random random;

    public TetrisGrid(Texture2D b) 
    {
        tetrisBlock = new TetrisBlock();
        gridblock = b;
        position = Vector2.Zero;
        this.Clear();
        blockList = new List<TetrisBlock>();
        gridTable = new Color[12, 20];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                gridTable[x, y] = Color.White;  // makes all blocks in the gridTable initially white (seen as grey in game)
        blockList.Add(RandomBlock()); // adds the first block to the game
        

        }

    public virtual TetrisBlock RandomBlock() // this method returns a random block that can be added to the game
    {
        random = new Random();
        int blockIndex = random.Next(1, 8);
        switch (blockIndex)
        {
            default:
                return new LBlock();
            case 1:
                return new ZBlock();
            case 2:
                return new ReverseLBlock();
            case 3:
                return new ReverseZBlock();
            case 4:
                return new SquareBlock();
            case 5:
                return new TBlock();
            case 6:
                return new LongBlock();
        }
    }




    public void NewBlock(Color c) //this method is used to draw the currently controlled block into the gridtable and reset the blocktable to the top of the screen with a new random block
    {
        c = blockList[0].blokKleur;
        for (int x = 0; x < 4; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockList[0].blockTable[x, y])
                {
                    gridTable[TetrisBlock.gridPosition.X + x, TetrisBlock.gridPosition.Y + y] = c;  // makes the squares in the grid where the block landed the color of that block
                }
            }
        }
        TetrisBlock.gridPosition.X = 4; // reset the blockTable to the top of the screen with a new random block
        TetrisBlock.gridPosition.Y = 0;
        blockList[0] = RandomBlock();
       
    }

    public bool VerticalCollision() //checks if a position below the currently controlled block is already filled in with another block in the gridtable
    {
        int y = blockList[0].OnderKant() - 1;
        bool verticalCollision = false;
            for (int x = 0; x < 4; x++)
        {
            if (blockList[0].blockTable[x, y] && blockList[0].OnderRand() < TetrisGrid.Height && gridTable[TetrisBlock.gridPosition.X + x, TetrisBlock.gridPosition.Y + y + 1] != Color.White)
            {
                verticalCollision = true;
                return verticalCollision;
            }
        }
        return verticalCollision;
    }

    

    // sprite for representing a single grid block
    public static Texture2D gridblock;

     // the position of the tetris grid
    Vector2 position;

     // width in terms of grid elements
    public static int Width
    {
        get { return 12; }
    }

     // height in terms of grid elements
    public static int Height
    {
        get { return 20; }
    }

     // clears the grid
    public void Clear()
    {
    }

    //handles the input for the controlled block
    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        foreach(TetrisBlock t in blockList)
        {
            t.HandleInput(gameTime, inputHelper);
        }
    }


    public void Update(GameTime gameTime) // starts the NewBlock method whenever the currently controlled block lands on either the bottom of the screen or another block
    {
        
        if (blockList[0].OnderRand() == TetrisGrid.Height || VerticalCollision())
        {

            NewBlock(Color.Blue);

        }
    }

     // draws the grid on the screen
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        int x, y;
        for (y = 0; y < Height; y++)
            for (x = 0; x < Width; x++)
            {
                s.Draw(gridblock, new Vector2(gridblock.Width * x, gridblock.Height * y), gridTable[x,y]);
            }

        

        foreach(TetrisBlock t in blockList)
        {
            t.Draw(gameTime, s);
        }
        
        

    }

   
}

