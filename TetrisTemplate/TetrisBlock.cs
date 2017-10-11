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
        protected bool[,] blockTable;
        public Point gridPosition;

        public TetrisBlock()
        {
            gridPosition = new Point(4,0);
            blockTable = new bool[4, 4];
            for(int y = 0; y<4; y++)
            {
                for (int x = 0; x < 4; x++)
                    blockTable[x, y] = false;
            } 
        }
        public void Draw(GameTime gameTime, SpriteBatch s)
        {
        Point screenPosition = new Point(gridPosition.X * 30, gridPosition.Y * 30);
            int x, y;
            for (y = 0; y < 4; y++)
                for (x = 0; x < 4; x++)
                    if (blockTable[y,x])
                    {
                        s.Draw(TetrisGrid.gridblock, new Vector2((TetrisGrid.gridblock.Width * x) + screenPosition.X, TetrisGrid.gridblock.Height * y), Color.Blue);
                    }
            if(RechterKant() > TetrisGrid.Width)
            {
                
            }
        }

        public int RechterKant()
        {
            for(int x=3;x>=0;x--)
            {
                for(int y=0; y<4;y++)
                {
                if (blockTable[x, y] == true)
                    return x + 1;
                }

            }
        return 0;
            
        }

        int moveInterval = 300;
        int currInterval = 0;

        public void HandleInput(GameTime gameTime, InputHelper inputHelper)
        {
            if (inputHelper.IsKeyDown(Keys.Right))
            {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;
                if (inputHelper.KeyPressed(Keys.Right, false) && RechterKant() + gridPosition.X < TetrisGrid.Width)
                {
                    gridPosition.X += 1;
                    currInterval = moveInterval;
                }
                else if (currInterval < 0)
                {
                    gridPosition.X += 1;
                    currInterval = moveInterval;
                }

            }
        if (inputHelper.IsKeyDown(Keys.Left))
        {
            currInterval -= gameTime.ElapsedGameTime.Milliseconds;
            if (inputHelper.KeyPressed(Keys.Left, false))
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }
            else if (currInterval < 0)
            {
                gridPosition.X -= 1;
                currInterval = moveInterval;
            }

        }

        //if (currentKeyboardState.IsKeyDown(Keys.Left))
        //{
        //    xPosition -= 30;
        //}
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

