using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using Pong.GameObjects;

namespace Pong
{
    class Game : SFML.Graphics.Drawable
    {
        Ball ball;
        Paddle Player;
        Vector2f windowSize;
        Item item;
        int gamestate;
        int difficulty;
        bool twoPlayerGame;
        Grid grid;
        RectangleShape topWall;
        RectangleShape bottomWall;
        RectangleShape leftWall;
        RectangleShape rightWall;

        float speed = 10;

        Vector2f direction = new Vector2f(.5f, .5f);

        public Game(Vector2f renderWindowSize)
        {
            difficulty = 11;
            windowSize = renderWindowSize;
            initBounds();
            grid = new Grid(windowSize);
            ball = new Ball(new Vector2f(700, 600), 10, windowSize, grid);
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50));
  
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
            ball = new Ball(new Vector2f(700, 600), 10, windowSize, grid);
            item = new Item(windowSize);
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50));
        }

        public void updateGame()
        {
            gamestate = 1;
            Player.update();
            ball.update();
            
        }

        /*
        void updateBall()
        {

            if(ManagerCollision.Instance.collide(leftWall, ball.Circle) || ManagerCollision.Instance.collide(rightWall, ball.Circle))
            {
                direction.X = -direction.X;
            }

            if(ManagerCollision.Instance.collide(topWall, ball.Circle) || ManagerCollision.Instance.collide(bottomWall, ball.Circle))
            {
                direction.Y = -direction.Y;
            }

            ball.Circle.Position += direction * speed;
        }
        */


        void resetGame()
        {
            gamestate = 2;
            Player = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Player.Draw(target, states);
            grid.Draw(target, states);
            ball.Draw(target, states);
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