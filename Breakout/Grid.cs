using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Pong.GameObjects;

namespace Pong
{
    class Grid : Drawable
    {
        Box[,] array2D;

        public Grid(Vector2f windowSize)
        {
            array2D = new Box[10, 5];
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 5; j++)
                {
                    array2D[i, j] = new Box(new Vector2f(windowSize.X / 12 * i + windowSize.X / 12, windowSize.Y / 12 * j + windowSize.Y / 12), new Vector2f(windowSize.X / 12, windowSize.Y / 12));
                }
            }
        }

        public Vector2f CollideWithBlock(CircleShape shape)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (array2D[i, j] != null)
                    {
                        Vector2f tmp = ManagerCollision.Instance.collideWithDirection(array2D[i, j].BoxShape, shape);
                        if (tmp.X == -1 || tmp.Y == -1){
                            array2D[i, j] = null;
                            return tmp;
                        }                               
                    }
                }
            }
            return new Vector2f(1,1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if(array2D[i, j] != null)
                    {
                        array2D[i, j].Draw(target, states);
                    }
                }
            }
        }
    }
}
