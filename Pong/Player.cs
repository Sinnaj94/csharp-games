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
    class Player : Drawable
    {
        private Vector2f playerPosition;
        private Vector2f playerSize;
        private RectangleShape shape;
        private float yBoundMin;
        private float yBoundMax;
        private Vector2f windowSize;
        private float playerSpeed;
        private float speedFactor;
        public Player(Vector2f windowSize, Vector2f startPosition)
        {
            playerSize = new Vector2f(10, 200);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            this.windowSize = windowSize;
            PlayerSpeed = 10;
            SpeedFactor = 1;
        }

        public void update()
        {
            YBoundMin = playerPosition.Y;                       // upper bounds
            YBoundMax = playerPosition.Y + playerSize.Y;     // lower bounds
            shape.Position = playerPosition;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }

        public void giveFeature()
        {
            SpeedFactor = 2;
        }

        public Vector2f PlayerPosition
        {
            get { return playerPosition; }
            set {
                if(value.Y < 0)
                {
                    playerPosition.Y = 0;
                }else if(value.Y > windowSize.Y - playerSize.Y)
                {
                    playerPosition.Y = windowSize.Y - playerSize.Y;
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

        public float PlayerSpeed
        {
            get
            {
                return playerSpeed;
            }

            set
            {
                playerSpeed = value;
            }
        }

        public float SpeedFactor
        {
            get
            {
                return speedFactor;
            }

            set
            {
                speedFactor = value;
            }
        }
    }
}
