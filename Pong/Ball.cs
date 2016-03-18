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
        public Ball(Vector2f deltaXY, Vector2f position)
        {
            circle = new CircleShape(10);
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * circle.Radius), position.Y - (1 / 2 * circle.Radius));
        }
        public CircleShape circle;
        Vector2f deltaXY;
        Vector2f position;

        public void updatePosition()
        {
            if (position.X < 1 || position.X > 499)
            {
                deltaXY.X = -deltaXY.X;
            }
            position.X = position.X + deltaXY.X;
            circle.Position = position;
        }

    }



}
