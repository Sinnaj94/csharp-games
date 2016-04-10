using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Pong.Properties;

namespace Pong
{
    class Player
    {
        private Vector2f playerPosition;
        private Vector2f playerSize;
        private RectangleShape shape;
        private float yBoundMin;
        private float yBoundMax;
        private RenderWindow window;

        public Player(RenderWindow window, Vector2f startPosition)
        {
            playerSize = new Vector2f(10, 200);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            this.window = window;
        }

        public void update()
        {
            YBoundMin = playerPosition.Y;                       // upper bounds
            YBoundMax = playerPosition.Y + playerSize.Y;     // lower bounds
            shape.Position = playerPosition;
        }

        public Vector2f PlayerPosition
        {
            get { return playerPosition; }
            set {
                if(value.Y < 0)
                {
                    playerPosition.Y = 0;
                }else if(value.Y > window.Size.Y - playerSize.Y)
                {
                    playerPosition.Y = window.Size.Y - playerSize.Y;
                }
                else
                {
                    playerPosition = value;
                }
                
            }
        }

        public RectangleShape Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        public float YBoundMin
        {
            get
            {
                return yBoundMin;
            }

            set
            {
                yBoundMin = value;
            }
        }

        public float YBoundMax
        {
            get
            {
                return yBoundMax;
            }

            set
            {
                yBoundMax = value;
            }
        }
    }
}
