using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;

namespace Pong
{
    class Game : SFML.Graphics.Drawable
    {
        Ball ball;
        Player player;
        newPlayer asd;
        Vector2f windowSize;
        Item item;
        int gamestate;
        int difficulty;
        bool twoPlayerGame;

        RectangleShape topWall;
        RectangleShape bottomWall;
        RectangleShape leftWall;
        RectangleShape rightWall;

      

        Vector2f direction = new Vector2f(.5f, .5f);
        float speed = 10;


        public Game(Vector2f renderWindowSize)
        {
            difficulty = 11;
            windowSize = renderWindowSize;
            initBounds();
            asd = new newPlayer(new Vector2f(100, 20), new Vector2f(0, windowSize.Y-50));
            ball = new Ball(10);
            ball.Circle.Position = (new Vector2f(100, 100));
            
        }

        private void initBounds()
        {
            topWall = new RectangleShape(windowSize);
            bottomWall = new RectangleShape(windowSize);
            leftWall = new RectangleShape(windowSize);
            rightWall = new RectangleShape(windowSize);

            topWall.FillColor = new Color(255, 0, 0, 128);
            bottomWall.FillColor = new Color(0, 255, 0, 128);
            leftWall.FillColor = new Color(255, 255, 255, 128);
            rightWall.FillColor = new Color(0, 0, 0, 255);

            topWall.Position = new Vector2f(0, -windowSize.Y);
            bottomWall.Position = new Vector2f(0, windowSize.Y);
            leftWall.Position = new Vector2f(-windowSize.X, 0);
            rightWall.Position = new Vector2f(windowSize.X, 0);
        }

        public void init()
        {
            player = new Player(windowSize, new Vector2f(windowSize.X * 0.5f, windowSize.Y * 0.95f));
            ball = new Ball(10);
            item = new Item(windowSize);
        }

        private void updateInput()
        {

            if (ManageInput.Instance.Left())
            {
                if(!ManagerCollision.Instance.collide(asd.PlayerShape, leftWall))
                {
                    asd.PlayerShape.Position += new Vector2f(-10, 0);
                }

            }

            if (ManageInput.Instance.Right())
            {
                if (!ManagerCollision.Instance.collide(asd.PlayerShape, rightWall))
                {
                    asd.PlayerShape.Position += new Vector2f(10, 0);

                }

            }

           /*if (ManageInput.Instance.Up())
            {
                if (!ManagerCollision.Instance.collide(asd.PlayerShape, topWall))
                {
                    asd.PlayerShape.Position += new Vector2f(0, -10);
                }
            }

            if (ManageInput.Instance.Down())
            {
                if (!ManagerCollision.Instance.collide(asd.PlayerShape, bottomWall))
                {
                    asd.PlayerShape.Position += new Vector2f(0, 10);
                }

            }*/
        }

        public void updateGame()
        {
            gamestate = 1;
            updateInput();
            updateBall();
           // player.update();
        }

        void updateBall()
        {

            if(ManagerCollision.Instance.collide(leftWall, ball.Circle) || ManagerCollision.Instance.collide(rightWall, ball.Circle))
            {
                direction.X = -direction.X;
            }

            if(ManagerCollision.Instance.collide(topWall, ball.Circle) || ManagerCollision.Instance.collide(bottomWall, ball.Circle) || ManagerCollision.Instance.collide(asd.PlayerShape,ball.Circle))
            {
                direction.Y = -direction.Y;
            }

            ball.Circle.Position += direction * speed;
        }

        void resetGame()
        {
            gamestate = 2;
            player = null;
          //  ball = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
               // player.Draw(target, states);
                ball.Draw(target, states);
               // item.Draw(target, states);
                //topWall.Draw(target, states);

                asd.Draw(target, states);

                //bottomWall.Draw(target, states);
                //leftWall.Draw(target, states);
                //rightWall.Draw(target, states);
        
    }

        public int Gamestate
        {
            get
            {
                return gamestate;
            }

            set
            {
                gamestate = value;
            }
        }

        public int Difficulty
        {
            get
            {
                return difficulty;
            }

            set
            {
                difficulty = value;
            }
        }

        public bool TwoPlayerGame
        {
            get
            {
                return twoPlayerGame;
            }

            set
            {
                twoPlayerGame = value;
            }
        }
    }
}