using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout.GameObjects
{
    class Ball : GameObject
    {
        Vector2f velocity;
        Vector2f direction;
        Vector2f windowsize;
        Grid grid;
        Paddle paddle;
        float radius;
        Sprite ballSprite;
        Texture ballTexture;
        private CircleShape circle;
        bool isSticky;
        ScoreBoard board;

        public Ball(Vector2f position, float radius, Vector2f windowSize, Grid grid, Paddle paddle, ScoreBoard scoreboard)
        {
            velocity = new Vector2f(5, 5);
            direction = new Vector2f(-1, -1);
            this.Position = position;
            circle = new CircleShape(radius);
            circle.Position = Position;
            this.windowsize = windowSize;
            this.grid = grid;
            this.paddle = paddle;
            this.radius = radius;
          //  IntRect temp = new IntRect(0,0,(int)radius * 2,(int)radius * 2);
            ballTexture = new Texture(@"Resources/ball.png");
            IntRect tmp = new IntRect(0, 0, (int)ballTexture.Size.X, (int)ballTexture.Size.Y);
            ballSprite = new Sprite(ballTexture, tmp);
            ballSprite.Scale = new Vector2f(Radius * 2 / ballTexture.Size.X, Radius * 2 / ballTexture.Size.Y);
            board = scoreboard;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //((Drawable)Circle).Draw(target, states);
            ballSprite.Draw(target, states);
            
        }

        public void isStickyUpdate()
        {
            Position = new Vector2f((paddle.Position.X + (paddle.Size.X / 2)) - Radius, paddle.Position.Y - 2*Radius);
            Circle.Position = Position;
            if (ManageInput.Instance.Space())
            {
                IsSticky = false;
                direction = new Vector2f(0, -1);
            }
        }

        public void regularUpdate()
        {
            // collide with paddle
            direction = ManagerCollision.Instance.precisePlayerCollision(paddle.Position.X, paddle.Position.X + paddle.Size.X, paddle.Position.Y, Position, velocity, Radius, direction);
             
            // collide with walls
            if (Position.X < 0 || Position.X + circle.Radius * 2 >= windowsize.X)
            {
                direction.X = -direction.X;
            }

            if (Position.Y < 0)
            {
                direction.Y = -direction.Y;
            }

            if(Position.Y >= windowsize.Y - 2*Radius)
            {
                isSticky = true;
                board.Lives--;
            }

            // collide with boxes
            Vector2f tmp = grid.CollideWithBlock(circle);

            direction.X = direction.X * tmp.X;
            direction.Y = direction.Y * tmp.Y;
            Position += new Vector2f(direction.X * velocity.X, direction.Y * velocity.Y);
            Circle.Position = Position;
            ballSprite.Position = Position;
            
        }

        public override void update()
        {
            if (isSticky)
            {
                isStickyUpdate();
            }
            else
            {
                regularUpdate();
            }
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

        public Vector2f Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }

        public float Radius
        {
            get
            {
                return radius;
            }

            set
            {
                radius = value;
            }
        }

        public bool IsSticky
        {
            get
            {
                return isSticky;
            }

            set
            {
                isSticky = value;
            }
        }
    }
}




