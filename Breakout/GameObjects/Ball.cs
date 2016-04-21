using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong.GameObjects
{
    class Ball : GameObject
    {
        Vector2f velocity;
        Vector2f direction;

        // TODO Move somewhere else
        RectangleShape topWall;
        RectangleShape bottomWall;
        RectangleShape leftWall;
        RectangleShape rightWall;

        private CircleShape circle;
        public Ball(Vector2f position, float radius, Vector2f windowSize)
        {
            velocity = new Vector2f(10, 10);
            direction = new Vector2f(-1, -1);
            this.Position = position;
            circle = new CircleShape(radius);
            circle.Position = Position;

            // TODO Move somewhere else
            topWall = new RectangleShape(windowSize);
            bottomWall = new RectangleShape(windowSize);
            leftWall = new RectangleShape(windowSize);
            rightWall = new RectangleShape(windowSize);
            topWall.Position = new Vector2f(0, -windowSize.Y);
            bottomWall.Position = new Vector2f(0, windowSize.Y);
            leftWall.Position = new Vector2f(-windowSize.X, 0);
            rightWall.Position = new Vector2f(windowSize.X, 0);

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)Circle).Draw(target, states);
        }

        public override void update()
        {
            if (ManagerCollision.Instance.collide(leftWall, circle) || ManagerCollision.Instance.collide(rightWall, circle))
            {
                direction.X = -direction.X;
            }

            if (ManagerCollision.Instance.collide(topWall, circle) || ManagerCollision.Instance.collide(bottomWall, circle))
            {
                direction.Y = -direction.Y;
            }

            Circle.Position += new Vector2f(direction.X * velocity.X, direction.Y * velocity.Y);
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

        public Vector2f Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }
    }
}




