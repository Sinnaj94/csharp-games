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
        RectangleShape paddleShape;

        Vector2f velocity;
        Vector2f direction;
        Vector2f windowSize;
        List<Item> itemList;
        
        public RectangleShape PaddleShape
        {
            get
            {
                return paddleShape;
            }

            set
            {
                paddleShape = value;
            }
        }

        public Paddle(Vector2f size, Vector2f position, Vector2f windowSize,List<Item> itemList)
        {
            this.Size = size;
            this.Position = position;
            this.windowSize = windowSize;
            direction = new Vector2f(0, 0);
            velocity = new Vector2f(10, 0);
            PaddleShape = new RectangleShape(Size);
            PaddleShape.Position = Position;
            PaddleShape.FillColor = new Color(255, 255, 255, 255);
            this.itemList = itemList;
        }

        
        public void setSize(float x)
        {
            Vector2f oldSize = PaddleShape.Size;
            Vector2f newSize = new Vector2f(x, PaddleShape.Size.Y);
            PaddleShape.Size = newSize;
            Size = newSize;
            Vector2f delta = new Vector2f((newSize.X-oldSize.X)/2 , 0);
            PaddleShape.Position -= delta;
            Position -= delta;
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
            if (ManageInput.Instance.Left() && Position.X > 0)
            {
                direction.X = -1;
            } else if
                (ManageInput.Instance.Right() && Position.X < windowSize.X - Size.X)
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
