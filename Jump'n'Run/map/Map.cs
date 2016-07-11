using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    public class Map
    {
        Vector2 mapSize;
        Tile[,] tileArray;
        int tileSize;

        public Map(int width, int height, int tileSize)
        {
            TileArray = new Tile[width, height];
            this.tileSize = tileSize;
        }

        public Tile[,] TileArray
        {
            get
            {
                return tileArray;
            }

            set
            {
                tileArray = value;
            }
        }

        public void AddTile(int x, int y, bool isCollidable)
        {
            TileArray[x, y] = new Tile(x, y, isCollidable);
        }


    }
}
