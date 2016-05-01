using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using System.Diagnostics;

namespace Breakout.GameObjects
{
    class Paddle : GameObject
    {
        bool isHuman = true;
        RectangleShape PaddleShape;

        Vector2f velocity;
        Vector2f direction;
        Vector2f windowSize;
        public Paddle(Vector2f size, Vector2f position, Vector2f windowSize)
        {
            this.Size = size;
            this.Position = position;
            this.windowSize = windowSize;
            direction = new Vector2f(0, 0);
            velocity = new Vector2f(10, 0);
            PaddleShape = new RectangleShape(Size);
            PaddleShape.Position = Position;
            PaddleShape.FillColor = new Color(255, 255, 255, 255);
        }

        public override void update()
        {
            if (isHuman)
            {
                // TODO: put that somewhere else!
                handleInput();
            }
            Position += new Vector2f(direction.X * velocity.X, 0);
            PaddleShape.Position = Position;
        }

        public Vector2f collideWithPaddle(CircleShape ball)
        {
            Vector2f tmp = ManagerCollision.Instance.collideWithDirection(PaddleShape, ball);
            if (tmp.X == -1 || tmp.Y == -1)
            {
                return tmp;
            }
            return new Vector2f(1, 1);
        }

        void handleInput()
        {
            if (ManageInput.Instance.Left()) {
                direction.X = -1;
            } else if
                (ManageInput.Instance.Right())
            {
                direction.X = 1;
            }
            else
            {
                direction.X = 0;
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            PaddleShape.Draw(target, states);
        }

    }
}
