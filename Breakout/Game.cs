using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;
using SFML.Audio;
using Breakout.GameObjects;

namespace Breakout
{
    class Game : SFML.Graphics.Drawable
    {
        Ball ball;
        Paddle Player;
        Vector2f windowSize;

        int gamestate;
        int difficulty;
        bool twoPlayerGame;
        Grid grid;
        RectangleShape topWall;
        RectangleShape bottomWall;
        RectangleShape leftWall;
        RectangleShape rightWall;
        List<Item> itemList;

        Vector2f direction = new Vector2f(.5f, .5f);

        public Game(Vector2f renderWindowSize)
        {
            difficulty = 11;
            windowSize = renderWindowSize;
            initBounds();
            itemList = new List<Item>();
            grid = new Grid(windowSize, itemList);
            
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50));
            ball = new Ball(new Vector2f(100, 100), 10, windowSize, grid, Player);
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

        

        public void updateGame()
        {
            gamestate = 1;
            Player.update();
            ball.update();
            Item deleteThis = null;
            foreach (Item i in itemList)
            {
                i.update();
                if (i.outOfRange())
                {
                    deleteThis = i;
                }
            }

            if(deleteThis!= null)
            {
                itemList.Remove(deleteThis);
            }

        }

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
            foreach(Item i in itemList)
            {
                i.Draw(target,states);
            }

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