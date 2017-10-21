using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



class TetrisBlock
{
    public bool[,] blockTable; // the array that holds the shape of each block
    protected bool[,] turnRight; // the array that is used to draw any block turned to the right
    protected bool[,] turnLeft; // the same for left
    public static Point gridPosition; // the position of the top left corner of the blockTable array within the TetrisGrid array.
    public Color blokKleur;
    

    public  TetrisBlock()
    {
        blokKleur = new Color();
        gridPosition = new Point(4, 0); //the starting position of the block, about top center
        blockTable = new bool[4, 4];
        turnRight = new bool[4, 4];
        turnLeft = new bool[4, 4];
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
                blockTable[x, y] = false; // every position in the blocktable is false unless declared true in the subclass
        }
         

    }

    

    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        Point screenPosition = new Point(gridPosition.X * 30, gridPosition.Y * 30); // the position on screen is the position in the grid * the width/height of each square
        int x, y;
        int linkerRand = LinkerKant() + gridPosition.X;  // the coordinates of the left most side of the current block
        int rechterRand = RechterKant() + gridPosition.X; // same for right

        if (rechterRand < TetrisGrid.Width + 1 && linkerRand > -1) // if the block is between the sides of the field
        {
            for (y = 0; y < 4; y++)
                for (x = 0; x < 4; x++)
                    if (blockTable[x, y])
                    {
                        s.Draw(TetrisGrid.gridblock, new Vector2(((TetrisGrid.gridblock.Width * x) + screenPosition.X), ((TetrisGrid.gridblock.Height * y) + screenPosition.Y)), blokKleur);
                    } // draws the current block on screen in allignment with the tetrisgrid but in front of it
        }}


    public virtual int RechterKant() // defines the right X coordinate of the current block within the blockTable (the other 'Kanten' do the same for their respective sides)
    {
        for (int x = 3; x >= 0; x--)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockTable[x, y] == true)
                    return x + 1;
            }

        }
        return 0;

    }
    public virtual int RechterRand() // Translates RechterKant to a position within the TetrisGrid (
    {
        return RechterKant() + gridPosition.X;
    }

    public int LinkerKant()
    {
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockTable[x, y] == true)
                    return x;
            }

        }
        return 0;
    }
    public virtual int LinkerRand()
    {
        return LinkerKant() + gridPosition.X;
    }

    public int OnderKant()
    {
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y >= 0; y--)
            {
                if (blockTable[x, y] == true)
                    return y + 1;
            }

        }
        return 0;
    }

    public virtual int OnderRand()
    {
        return OnderKant()+gridPosition.Y;
    }

    public bool RightCollision() // checks if a position to the right of any of the right most blocks in the currently controlled block are already filled in in the tetrisgrid
    {
        int x = RechterKant() - 1;
        bool rightCollision = false;
        for (int y = 0; y < 4; y++)
        {
            if (blockTable[x, y] && TetrisGrid.gridTable[TetrisBlock.gridPosition.X + x + 1, TetrisBlock.gridPosition.Y + y] != Color.White)
            {
                rightCollision = true;
                return rightCollision;
            }
        }
        return rightCollision;
    }

    public bool LeftCollision() // checks if a position to the left of any of the left most blocks in the currently controlled block are already filled in in the tetrisgrid
    {
        int x = LinkerKant();
        bool leftCollision = false;
        for (int y = 0; y < 4; y++)
        {
            if (blockTable[x, y] && TetrisGrid.gridTable[TetrisBlock.gridPosition.X + x - 1, TetrisBlock.gridPosition.Y + y] != Color.White)
            {
                leftCollision = true;
                return leftCollision;
            }
        }
        return leftCollision;
    }



    int moveInterval = 300; //the interval at which the block moves left or right when the left or right arrow buttons are pressed
    int currInterval = 0;
    int fallInterval = 400; // the interval at which the block falls down
    int currFallInterval = 0;


    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        
        if (inputHelper.IsKeyDown(Keys.Right)) // moves the block right
        {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;

            if (inputHelper.KeyPressed(Keys.Right, false) && RechterRand() < TetrisGrid.Width && RightCollision() == false) //the block will move one step immediately when the button is first pressed
            {
                gridPosition.X += 1;
                currInterval = moveInterval; // the interval is set to the desired time
            }
            else if (currInterval < 0 && RechterRand() < TetrisGrid.Width && RightCollision() == false) // when the interval has counted down to 0 and the button is still held the block moves another step
            {
                gridPosition.X += 1;
                currInterval = moveInterval;
            }

        }
        if (inputHelper.IsKeyDown(Keys.Left)) // Same for left
        {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;
            
            if (inputHelper.KeyPressed(Keys.Left, false) && LinkerRand() > 0 && LeftCollision() == false)
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }
            else if (currInterval < 0 && LinkerRand() > 0 && LeftCollision() == false)
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }

        }
        if (inputHelper.KeyPressed(Keys.D) || LinkerRand() < 0) // turns the block right if D is pressed or if it was just turned left out of the screen
        {
            bool[,] turnRight = new bool[4, 4]; //creates a new table that represents the block but turned right
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    turnRight[j , i] = blockTable[3 - i , j];
                }
            }
            blockTable = turnRight; // turns blocktable into the turned table
           
            
        }
        if (inputHelper.KeyPressed(Keys.A) || RechterRand() > TetrisGrid.Width) //same for turning left
        {
            bool[,] turnLeft = new bool[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    turnLeft[j, i] = blockTable[i, 3 - j];
                }
            }
            blockTable = turnLeft;
        }


        // creates interval steps similar to when moving left or right for moving the block down
        currFallInterval -= gameTime.ElapsedGameTime.Milliseconds;
        if (currFallInterval< 0 && OnderRand()<TetrisGrid.Height)
        {
            gridPosition.Y += 1;
            currFallInterval = fallInterval;
        }


    }

    
 
    public void Update(GameTime gameTime)
{
        
}
            

    }

    
    class LongBlock : TetrisBlock //these classes create enter the true values into the blockTables for each block
{
    public LongBlock()
    {
        blockTable[0, 0] = true;
        blockTable[0, 1] = true;
        blockTable[0, 2] = true;
        blockTable[0, 3] = true;
        this.blokKleur = Color.Red;
    }


}

class LBlock : TetrisBlock
{
    public LBlock()
    {
        blockTable[0, 0] = true;
        blockTable[0, 1] = true;
        blockTable[0, 2] = true;
        blockTable[1, 2] = true;
        this.blokKleur = Color.Orange;
    }
}

class ReverseLBlock : TetrisBlock
{
    public ReverseLBlock()
    {
        blockTable[1, 0] = true;
        blockTable[1, 1] = true;
        blockTable[0, 2] = true;
        blockTable[1, 2] = true;
        this.blokKleur = Color.Yellow;
    }
}

class TBlock : TetrisBlock
{
    public TBlock()
    {
        blockTable[0, 0] = true;
        blockTable[1, 0] = true;
        blockTable[2, 0] = true;
        blockTable[1, 1] = true;
        this.blokKleur = Color.Green;
    }
}

class ZBlock : TetrisBlock
{
    public ZBlock()
    {
        blockTable[0, 0] = true;
        blockTable[0, 1] = true;
        blockTable[1, 1] = true;
        blockTable[1, 2] = true;
        this.blokKleur = Color.Blue;
    }
}

class ReverseZBlock : TetrisBlock
{
    public ReverseZBlock()
    {
        blockTable[1, 0] = true;
        blockTable[0, 1] = true;
        blockTable[1, 1] = true;
        blockTable[0, 2] = true;
        this.blokKleur = Color.Purple;
    }
}

class SquareBlock : TetrisBlock
{
    public SquareBlock()
    {
        blockTable[0, 0] = true;
        blockTable[1, 0] = true;
        blockTable[0, 1] = true;
        blockTable[1, 1] = true;
        this.blokKleur = Color.HotPink;
    }
}

