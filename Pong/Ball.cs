using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;


namespace Pong
{
    class Ball
    {
        public Ball(RenderWindow window, Vector2f deltaXY, Vector2f position)
        {
            Circle = new CircleShape(10);
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * Circle.Radius), position.Y - (1 / 2 * Circle.Radius));
            this.window = window;
        }
        private CircleShape circle;
        private float ballSpeed = 10;
        private Vector2f deltaXY;
        private Vector2f position;
        private RenderWindow window;

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

        public void updatePosition(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {

            if (position.Y < 1 || position.Y > window.Size.Y)
            {
                deltaXY.Y = -deltaXY.Y;
            }
            
            if (position.X < 1 || position.X > window.Size.X)
            {
                ballSpeed = 10;

                if (position.X < 1)
                {
                    deltaXY.X =  1 * ballSpeed;
                    deltaXY.Y =  0;

                } else
                {
                    deltaXY.X = -1 * ballSpeed;
                    deltaXY.Y = 0;
                }
                position.X = window.Size.X / 2;
                position.Y = window.Size.Y / 2;             
            }
            
                if (position.X <= playerXPosition && position.Y > YBoundMin && position.Y < YBoundMax)
                {
                    double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                    double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                    double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                    deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                    deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                    ballSpeed += 1;

                }

                if (position.X >= KiXPosition && position.Y > KiYBoundMin && position.Y < KiYBoundMax)
                {
                    double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                    double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                    double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                    deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                    deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                    ballSpeed += 1;
            }

            position.X += deltaXY.X;
                position.Y += deltaXY.Y;
                Circle.Position = position;
               
        }

    }



}