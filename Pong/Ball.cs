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

        public Ball(Vector2f windowSize, Vector2f deltaXY, Vector2f position, float radius)
        {
            
            Circle = new CircleShape(radius);
            BoundingBox = new RectangleShape();
            BoundingBox.Size = new Vector2f(radius*2,radius*2);
            this.radius = radius;
            this.durchmesser = radius * 2;
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * Circle.Radius), position.Y - (1 / 2 * Circle.Radius));
            this.windowSize = windowSize;
            ballSpeed = 10;
            scoreState = 0;
            //touched Last: 0 is left, 1 is right.
            TouchedLast = -1;
        }

        public void setBoundingBox()
        {
            BoundingBox.Position = circle.Position;
        }

        //returns the player who touched the ball last.
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

            playerCollision(YBoundMin, YBoundMax, playerXPosition, KiYBoundMin, KiYBoundMax, KiXPosition);
            
            position.X += deltaXY.X;
            position.Y += deltaXY.Y;
            Circle.Position = position;
            setBoundingBox();
            CheckOutOfBounds();
            

        }

        

        private void playerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {
            if (position.X <= playerXPosition && position.Y+radius > YBoundMin && position.Y < YBoundMax)
            {
                
                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                ManageSound.Instance.hit();
                TouchedLast = 0;
            }

            if (position.X + radius >= KiXPosition && position.Y+radius > KiYBoundMin && position.Y < KiYBoundMax)
            {
                
                double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                ManageSound.Instance.hit();
                TouchedLast = 1;
            }
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

    }
}