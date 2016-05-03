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
        List<Item> itemList;
        List<Feature> featureList;
        ScoreBoard board;

        Vector2f direction = new Vector2f(.5f, .5f);
        public Game(Vector2f renderWindowSize)
        {
            gamestate = 1;
            windowSize = renderWindowSize;
            itemList = new List<Item>();
            featureList = new List<Feature>();
            board = new ScoreBoard();
            grid = new Grid(windowSize, board, itemList);
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50), windowSize,itemList);
            ball = new Ball(new Vector2f(100, 100), 10, windowSize, grid, Player, board);
        }

        public void updateGame()
        {

            if (board.Lives < 0)
            {
                gamestate = 2;
            }

            Player.update();
            ball.update();
            if (grid.AllGone)
            {
                //TODO: Game Ende implementieren
            }

            List<Item> toRemove = new List<Item>();
            foreach (Item i in itemList)
            {
                i.update();
                if (i.outOfRange())
                {
                    toRemove.Add(i);
                }
                if (ManagerCollision.Instance.collide(Player.PaddleShape, i.Rectangle))
                {
                    toRemove.Add(i);
                    Feature f = new Feature(0,Player);
                    featureList.Add(f);
                }
                
            }
            foreach(Item i in toRemove)
            {
                itemList.Remove(i);
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
            foreach (Item i in itemList)
            {
                i.Draw(target, states);
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
    }
}