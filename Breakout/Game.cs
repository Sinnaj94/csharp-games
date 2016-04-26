﻿using System;
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
        Grid grid;
        ScoreBoard board;

        Vector2f direction = new Vector2f(.5f, .5f);

        public Game(Vector2f renderWindowSize)
        {
            gamestate = 1;
            windowSize = renderWindowSize;
            board = new ScoreBoard();
            grid = new Grid(windowSize, board);
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50));
            ball = new Ball(new Vector2f(100, 100), 10, windowSize, grid, Player, board);
        }

        public void updateGame()
        {

            if(board.Lives < 0)
            {
                gamestate = 2;
            }

            Player.update();
            ball.update();
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
    }
}