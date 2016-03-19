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
                deltaXY.X = -deltaXY.X;
            }

            position.X += deltaXY.X;
            position.Y += deltaXY.Y;
            Circle.Position = position;
        }

    }



}
