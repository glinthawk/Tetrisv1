using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Content
{
    class TetrisBlock
    {
        protected bool[,] blockTable;

        public TetrisBlock()
        {
            blockTable = new bool[4, 4];
        }
        
    }

    class LongBlock : TetrisBlock
    {
       public LongBlock()
        {
            for(int y = 0; y<4; y++)
            {
                int x = 2;
                    blockTable[x, y] = true;
            }
            
        }
     
    }
}
