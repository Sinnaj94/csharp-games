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
        private float sizeFactor;
        private bool hasFeature;

        public Player(Vector2f windowSize, Vector2f startPosition)
        {
            playerSize = new Vector2f(10, 200);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            this.windowSize = windowSize;
            PlayerSpeed = 10;
            SpeedFactor = 1;
            sizeFactor = 1;
            hasFeature = false;
        }

        public void update()
        {
            YBoundMin = playerPosition.Y;                       // upper bounds
            YBoundMax = playerPosition.Y + playerSize.Y;     // lower bounds
            shape.Position = playerPosition;
        }

        public void changePlayerSize(float newSize)
        {
            Shape.Size = new Vector2f(Shape.Size.X, newSize);
            playerSize.Y = newSize;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
        }

        public void giveFeature(int nr)
        {
            //Feature List:
            //Racket: 1-4
            //Good Features:
            //Feature 1: Bigger Racket 
            //Feature 2: Faster Racket
            //Bad Features:
            //Feature 3: Smaller Racket
            //Feature 4: Slower Racket
            if (nr == 1)
            {
                changePlayerSize(500);
            }else if(nr == 2)
            {
                //Geschwindigkeit aendern
            }else if(nr == 3)
            {

            }else if (nr == 4)
            {

            }
            
            hasFeature = true;
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
