using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Breakout.Properties;

namespace Breakout
{
    class Player : Drawable
    {
        private Vector2f playerPosition;
        private Vector2f playerSize;
        private RectangleShape shape;
        private float yBoundMin;
        private float yBoundMax;
        private float xBoundMin;
        private float xBoundMax;
        private Vector2f windowSize;
        private float playerSpeed;
        private float speedFactor;
        private float sizeFactor;
        private int featureNr;
        private RectangleShape featureRect;
        private Clock featureTime;
        private float standardSize;
        private String featureText;

        public Player(Vector2f windowSize, Vector2f startPosition)
        {
            playerSize = new Vector2f(200, 10);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            this.windowSize = windowSize;
            PlayerSpeed = 10;
            SpeedFactor = 1;
            sizeFactor = 1;
            featureNr = 0;
            featureRect = new RectangleShape(new Vector2f(20, 20));
            featureRect.Position = new Vector2f(shape.Position.X, windowSize.Y * .8f);
            featureRect.OutlineColor = Color.Black;
            standardSize = playerSize.Y;

            featureText = "";

        }

        public bool hasFeature()
        {
            if (featureNr == 0)
            {
                return false;
            }
            return true;
        }

        public void update()
        {
           // shape.Position = playerPosition;
        }

        public void changePlayerSize(float newSize)
        {
            Shape.Size = new Vector2f(Shape.Size.X, newSize);
            playerSize.Y = newSize;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)shape).Draw(target, states);
            if (hasFeature())
            {

                ((Drawable)featureRect).Draw(target, states);
            }
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

            applyFeature(nr, false);
            featureNr = nr;



            featureTime = new Clock();
        }

        private void setFeatureText(int featureNr)
        {
            if (featureNr == 0)
            {
                featureText = "";
            }
            else if (featureNr == 1)
            {
                featureText = "Big Racket";
            }
            else if (featureNr == 2)
            {
                featureText = "Fast Racket";
            }
            else if (featureNr == 3)
            {
                featureText = "Small Racket";
            }
            else if (featureNr == 4)
            {
                featureText = "Slow Racket";
            }
            Console.Out.WriteLine(featureText);
        }

        private void applyFeature(int nr, bool revert)
        {
            switch (nr)
            {
                case 1:
                    if (revert)
                    {
                        changePlayerSize(standardSize);
                        featureText = "";
                    }
                    else
                    {
                        changePlayerSize(standardSize + standardSize * .5f);
                    }
                    break;
                case 2:
                    if (revert)
                    {
                        speedFactor = 1;
                    }
                    else
                    {
                        speedFactor = 2;
                    }
                    break;
                case 3:
                    if (revert)
                    {
                        changePlayerSize(standardSize);
                    }
                    else
                    {
                        changePlayerSize(standardSize - standardSize * .5f);
                    }
                    break;
                case 4:
                    if (revert)
                    {
                        speedFactor = 1;
                    }
                    else
                    {
                        speedFactor = .5f;
                    }
                    break;
            }
            setFeatureText(nr);
        }


        public bool timesUp()
        {
            Time now = featureTime.ElapsedTime;

            if (now.AsSeconds() >= 8)
            {
                return true;
            }
            return false;
        }

        /*
        public Vector2f PlayerPosition
        {
            get { return playerPosition; }
            set
            {
                if (value.Y < 0)
                {
                    playerPosition.Y = 0;
                }
                else if (value.Y > windowSize.Y - playerSize.Y)
                {
                    playerPosition.Y = windowSize.Y - playerSize.Y;
                }
                else
                {
                    playerPosition = value;
                }

            }
        }
        */

        public void removeFeature()
        {

            applyFeature(featureNr, true);
            featureNr = 0;
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

        public float XBoundMin
        {
            get
            {
                return xBoundMin;
            }

            set
            {
                xBoundMin = value;
            }
        }

        public float XBoundMax
        {
            get
            {
                return xBoundMax;
            }

            set
            {
                xBoundMax = value;
            }
        }

        public Vector2f PlayerPosition
        {
            get
            {
                return playerPosition;
            }

            set
            {
                playerPosition = value;
            }
        }
    }
}
