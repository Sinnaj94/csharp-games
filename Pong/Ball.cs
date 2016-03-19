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

        public void updatePosition(float YBoundMin, float YBoundMax, float playerXPosition)
        {
            if (position.X < 1)
            {  
                position.X = 400;
                position.Y = 400;
                Random rnd = new Random();
                deltaXY.X = (float) rnd.NextDouble() * 10;
                deltaXY.Y = (float) rnd.NextDouble() * 10;
            }

            if (position.X < 1 || position.X > window.Size.X)
            {
                deltaXY.X = -deltaXY.X;
            }

            if (position.Y < 1 || position.Y > window.Size.Y)
            {
                deltaXY.Y = -deltaXY.Y;
            }

            if (position.X <= playerXPosition && position.Y > YBoundMin && position.Y < YBoundMax)
            {
                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = 10 * (float) Math.Cos(bounceAngle);
                deltaXY.Y = 10 * (float) -Math.Sin(bounceAngle);
            }

            position.X += deltaXY.X;
            position.Y += deltaXY.Y;
            Circle.Position = position;
        }

    }



}
