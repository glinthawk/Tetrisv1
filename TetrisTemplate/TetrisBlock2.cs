using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//test



class TetrisBlock
{
    protected bool[,] blockTable;
    protected bool[,] turnRight;
    protected bool[,] turnLeft;
    public Point gridPosition;

    public TetrisBlock()
    {
        gridPosition = new Point(4, 0);
        blockTable = new bool[4, 4];
        turnRight = new bool[4, 4];
        turnLeft = new bool[4, 4];
        for (int y = 0; y < 4; y++)
        {
            for (int x = 0; x < 4; x++)
                blockTable[x, y] = false;
        }
    }
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        Point screenPosition = new Point(gridPosition.X * 30, gridPosition.Y * 30);
        int x, y;
        int linkerRand = LinkerKant() + gridPosition.X;
        int rechterRand = RechterKant() + gridPosition.X;

        if (rechterRand < TetrisGrid.Width + 1 && linkerRand > -1)
        {
            for (y = 0; y < 4; y++)
                for (x = 0; x < 4; x++)
                    if (blockTable[y, x])
                    {
                        s.Draw(TetrisGrid.gridblock, new Vector2(((TetrisGrid.gridblock.Width * x) + screenPosition.X), ((TetrisGrid.gridblock.Height * y) + screenPosition.Y)), Color.Blue);
                    }
        }

    }


    public int RechterKant()
    {
        for (int x = 3; x >= 0; x--)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockTable[y, x] == true)
                    return x + 1;
            }

        }
        return 0;

    }

    public int LinkerKant()
    {
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 0; y < 4; y++)
            {
                if (blockTable[y, x] == true)
                    return x;
            }

        }
        return 0;
    }
  
    public int OnderKant()
    {
        for (int x = 0; x <= 3; x++)
        {
            for (int y = 3; y >= 0; y--)
            {
                if (blockTable[y, x] == true)
                    return y + 1;
            }

        }
        return 0;
    }




    int moveInterval = 300;
    int currInterval = 0;
    int fallInterval = 700;
    int currFallInterval = 0;


    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        int linkerRand = LinkerKant() + gridPosition.X;
        int rechterRand = RechterKant() + gridPosition.X;
        if (inputHelper.IsKeyDown(Keys.Right))
        {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;
            

            if (inputHelper.KeyPressed(Keys.Right, false) && rechterRand < TetrisGrid.Width)
            {
                gridPosition.X += 1;
                currInterval = moveInterval;
            }
            else if (currInterval < 0 && rechterRand < TetrisGrid.Width)
            {
                gridPosition.X += 1;
                currInterval = moveInterval;
            }

        }
        if (inputHelper.IsKeyDown(Keys.Left))
        {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;
            
            if (inputHelper.KeyPressed(Keys.Left, false) && linkerRand > 0)
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }
            else if (currInterval < 0 && linkerRand > 0)
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }

        }
        if (inputHelper.KeyPressed(Keys.D) || linkerRand < 0)
        {
            bool[,] turnRight = new bool[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    turnRight[j , i] = blockTable[3 - i , j];
                }
            }
            blockTable = turnRight;
           
            
        }
        if (inputHelper.KeyPressed(Keys.A) || rechterRand > TetrisGrid.Width)
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



            currFallInterval -= gameTime.ElapsedGameTime.Milliseconds;
        int onderRand = OnderKant() + gridPosition.Y;
        if (currFallInterval< 0 && onderRand<TetrisGrid.Height)
        {
            gridPosition.Y += 1;
            currFallInterval = fallInterval;
        }

    }
 
    public void Update(GameTime gameTime)
{


}
            

    }

    
    class LongBlock : TetrisBlock
{
    public LongBlock()
    {
        blockTable[0, 0] = true;
        blockTable[1, 0] = true;
        blockTable[2, 0] = true;
        blockTable[3, 0] = true;
    }


}

class LBlock : TetrisBlock
{
    public LBlock()
    {
        blockTable[0, 0] = true;
        blockTable[1, 0] = true;
        blockTable[2, 0] = true;
        blockTable[2, 1] = true;
    }
}

class ReverseLBlock : TetrisBlock
{
    public ReverseLBlock()
    {
        blockTable[0, 1] = true;
        blockTable[1, 1] = true;
        blockTable[2, 0] = true;
        blockTable[2, 1] = true;
    }
}

class TBlock : TetrisBlock
{
    public TBlock()
    {
        blockTable[0, 0] = true;
        blockTable[0, 1] = true;
        blockTable[0, 2] = true;
        blockTable[1, 1] = true;
    }
}

class ZBlock : TetrisBlock
{
    public ZBlock()
    {
        blockTable[0, 0] = true;
        blockTable[1, 0] = true;
        blockTable[1, 1] = true;
        blockTable[2, 1] = true;
    }
}

class ReverseZBlock : TetrisBlock
{
    public ReverseZBlock()
    {
        blockTable[0, 1] = true;
        blockTable[1, 0] = true;
        blockTable[1, 1] = true;
        blockTable[2, 0] = true;
    }
}

class SquareBlock : TetrisBlock
{
    public SquareBlock()
    {
        blockTable[0, 0] = true;
        blockTable[0, 1] = true;
        blockTable[1, 0] = true;
        blockTable[1, 1] = true;
    }
}

