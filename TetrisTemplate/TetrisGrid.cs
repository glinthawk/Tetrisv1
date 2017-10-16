using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

/*
 * a class for representing the Tetris playing grid
 */
class TetrisGrid
{
    Color[,] table;
    List<TetrisBlock> BlockList;

    public TetrisGrid(Texture2D b)
    {
        gridblock = b;
        position = Vector2.Zero;
        this.Clear();
        table = new Color[20, 12];
        for (int y = 0; y < Height; y++)
            for (int x = 0; x < Width; x++)
                table[y, x] = Color.White;
        BlockList = new List<TetrisBlock>();
        
        BlockList.Add(new LBlock());

    }

    /*
     * sprite for representing a single grid block
     */
    public static Texture2D gridblock;

    /*
     * the position of the tetris grid
     */
    Vector2 position;

    /*
     * width in terms of grid elements
     */
    public static int Width
    {
        get { return 12; }
    }

    /*
     * height in terms of grid elements
     */
    public static int Height
    {
        get { return 20; }
    }

    /*
     * clears the grid
     */
    public void Clear()
    {
    }

    public void HandleInput(GameTime gameTime, InputHelper inputHelper)
    {
        foreach(TetrisBlock t in BlockList)
        {
            t.HandleInput(gameTime, inputHelper);
        }
    }

    /*
     * draws the grid on the screen
     */
    public void Draw(GameTime gameTime, SpriteBatch s)
    {
        int x, y;
        for (y = 0; y < Height; y++)
            for (x = 0; x < Width; x++)
            {
                s.Draw(gridblock, new Vector2(gridblock.Width * x, gridblock.Height * y), table[y,x]);
            }
        foreach(TetrisBlock t in BlockList)
        {
            t.Draw(gameTime, s);
        }
        

    }
}

