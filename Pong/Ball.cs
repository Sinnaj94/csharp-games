using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;


namespace Pong
{
    class Ball : Drawable
    {
        private CircleShape circle;
        private float ballSpeed;
        private Vector2f deltaXY;
        private Vector2f position;
        private Vector2f windowSize;
        private float radius;
        private float durchmesser;
        private int scoreState;
        private RectangleShape boundingBox;
        private int touchedLast;
        private int featureNr;
        private Clock featureTime;
        private float standardSize;
        public Ball(Vector2f windowSize, Vector2f deltaXY, Vector2f position, float radius)
        {
            
            Circle = new CircleShape(radius);
            BoundingBox = new RectangleShape();
            BoundingBox.Size = new Vector2f(radius*2,radius*2);
            this.radius = radius;
            this.durchmesser = radius * 2;
            this.standardSize = radius;
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * Circle.Radius), position.Y - (1 / 2 * Circle.Radius));
            this.windowSize = windowSize;
            ballSpeed = 10;
            scoreState = 0;
            //touched Last: 0 is left, 1 is right.
            TouchedLast = -1;
            featureNr = 0;
        }

        public void setBoundingBox()
        {
            BoundingBox.Position = circle.Position;
        }


        public void updatePosition(Player player, Player ki)
        {

            float YBoundMin = player.YBoundMin;
            float YBoundMax = player.YBoundMax;
            float playerXPosition = player.PlayerPosition.X;

            float KiYBoundMin = ki.YBoundMin;
            float KiYBoundMax = ki.YBoundMax;
            float KiXPosition = ki.PlayerPosition.X;

            if (position.Y < 0 || position.Y + durchmesser > windowSize.Y)
            {
                if(position.Y < 0)
                {
                    position.Y = 0;
                }
                else
                {
                    position.Y = windowSize.Y - durchmesser;
                }
                deltaXY.Y = -deltaXY.Y;
                ManageSound.Instance.side();
            }

            if (precisePlayerCollision(YBoundMin, YBoundMax, playerXPosition, KiYBoundMin, KiYBoundMax, KiXPosition))
            {
                ballSpeed += 1;
                ManageSound.Instance.hit();
            }

            CheckOutOfBounds();     
            position.X += deltaXY.X;
            position.Y += deltaXY.Y;      
            Circle.Position = position;
            setBoundingBox();
        }


        private bool precisePlayerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {
            ////

            Vector2f delta = deltaXY;
            Vector2f deltaAbsolut = delta;
            Vector2f precisePosition = position;
            deltaAbsolut.X = Math.Abs(deltaAbsolut.X);
            deltaAbsolut.Y = Math.Abs(deltaAbsolut.Y);
            float factor;

            if (deltaAbsolut.X >= deltaAbsolut.Y)
            {
                factor = deltaAbsolut.X;
                delta.Y = delta.Y / deltaAbsolut.X;
                delta.X = delta.X / deltaAbsolut.X;

            }
            else
            {
                factor = deltaAbsolut.Y;
                delta.X = delta.X / deltaAbsolut.Y;
                delta.Y = delta.Y / deltaAbsolut.Y;
            }

            while ((int)factor >= 0)
            {
                precisePosition.X += delta.X;
                precisePosition.Y += delta.X;

               
                

                if (precisePosition.X <= playerXPosition && precisePosition.X >= playerXPosition - 10 && precisePosition.Y + radius > YBoundMin && precisePosition.Y < YBoundMax)
                {
                    double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - precisePosition.Y;
                    double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                    double bounceAngle = normalizedRelativeIntersectionY * ((3 * Math.PI) / 12);
                    deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                    deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                    TouchedLast = 0;

                    Console.Out.Write("TRIGGERD");

                    return true;
                }

                if (precisePosition.X + radius >= KiXPosition && precisePosition.X <= KiXPosition && precisePosition.Y + radius > KiYBoundMin && precisePosition.Y < KiYBoundMax)
                {
                    double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - precisePosition.Y;
                    double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                    double bounceAngle = normalizedRelativeIntersectionY * ((3 * Math.PI) / 12);
                    deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                    deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                    TouchedLast = 1;

                    Console.Out.Write("TRIGGERD 2");

                    return true;
                }

                factor--;

            }

            ////

            return false;
        }

        

        private bool playerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {
            if (position.X <= playerXPosition && position.X >= playerXPosition - 100 && position.Y+radius > YBoundMin && position.Y < YBoundMax)
            {
                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((3* Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                TouchedLast = 0;
                return true;
            }

            if (position.X + radius >= KiXPosition && position.X <= KiXPosition + 100 && position.Y+radius > KiYBoundMin && position.Y < KiYBoundMax)
            {          
                double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((3* Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                TouchedLast = 1;
                return true;
            }
            return false;
        }

        public int CheckOutOfBounds()
        {
            // Ball out of Bounds
            if (position.X <= 0 || position.X + radius >= windowSize.X)
            {
                ballSpeed = 10;

                if (position.X <= 0)
                {
                    ManageSound.Instance.lose();
                    deltaXY.X = 1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = windowSize.X / 2;
                    position.Y = windowSize.Y / 2;
                    scoreState = 2;
                    return 0;

                }
                else if (position.X + radius >= windowSize.X)
                {                  
                    deltaXY.X = -1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = windowSize.X / 2;
                    position.Y = windowSize.Y / 2;
                    scoreState = 1;
                    return 1;

                }

            }
            scoreState = 0;
            return -1;

        }



        public CircleShape Circle
        {
            get
            {
                return circle;
            }

            set
            {
                circle = value;
            }
        }

        public RectangleShape BoundingBox
        {
            get
            {
                return boundingBox;
            }

            set
            {
                boundingBox = value;
            }
        }

        public int ScoreState
        {
            get
            {
                return scoreState;
            }

            set
            {
                scoreState = value;
            }
        }

        public int TouchedLast
        {
            get
            {
                return touchedLast;
            }

            set
            {
                touchedLast = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)circle).Draw(target, states);
            //((Drawable)boundingBox).Draw(target, states);
        }
        


        //FEATURES
        public void giveFeature(int nr)
        {
            //Ball: 5-8:
            //Neither good nor bad:
            //Feature 5: Bigger Ball
            //Feature 6: Smaller Ball
            //Feature 7: Slower Ball
            //Feature 8: Faster Ball

            applyFeature(nr, false);
            featureNr = nr;



            featureTime = new Clock();
        }

        public void removeFeature()
        {

            applyFeature(featureNr, true);
            featureNr = 0;
        }

        private void changeBallRadius(float newRadius)
        {
            radius = newRadius;
            Circle.Radius = newRadius;
            durchmesser = newRadius * 2;
        }

        private void applyFeature(int nr, bool revert)
        {
            switch (nr)
            {
                case 5:
                    if (revert)
                    {
                        changeBallRadius(standardSize);

                    }
                    else
                    {
                        changeBallRadius(standardSize*3);
                    }
                    break;
                case 6:
                    if (revert)
                    {
                        changeBallRadius(standardSize);
                    }
                    else
                    {
                        changeBallRadius(standardSize - standardSize * .5f);
                    }
                    break;
                case 7:
                    if (revert)
                    {
                        
                    }
                    else
                    {
                        deltaXY = new Vector2f(deltaXY.X * .1f, deltaXY.Y * .1f);
                    }
                    break;
                case 8:
                    if (revert)
                    {
                        
                    }
                    else
                    {
                        deltaXY = new Vector2f(deltaXY.X * 1.5f, deltaXY.Y * 1.5f);
                    }
                    break;
            }
            
        }

        public bool hasFeature()
        {
            if (featureNr != 0)
            {
                return true;
            }
            return false;
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

    }
}