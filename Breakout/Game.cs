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
            Player = new Paddle(new Vector2f(100, 20), new Vector2f(0, windowSize.Y - 50), windowSize,itemList);
            grid = new Grid(windowSize, board, itemList, Player, 1);
            ball = new Ball(new Vector2f(Player.PaddleShape.Position.X, Player.PaddleShape.Position.Y-15), 10, windowSize, grid, Player, board);
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
                //TODO: find a way to count every level in levels, now max level is hardcoded
                if ( grid.Level <= 7)
                {
                    grid.initNewLevel();
                } else
                {
                    Gamestate = 2;
                }
            }

            //Items hinzufuegen
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
                    Feature f = i.Feature;
                    bool sameFeature = false;
                    foreach(Feature has in featureList)
                    {
                        if (has.FeatureNumber == f.FeatureNumber) sameFeature = true;
                    }
                    if (sameFeature)
                    {
                        f.resetClock();
                    }else
                    {
                        featureList.Add(f);
                    }
                }
                
            }
            foreach(Item i in toRemove)
            {
                itemList.Remove(i);
            }
            List<Feature> toRemoveFeature = new List<Feature>();
            foreach(Feature f in featureList)
            {
                
                if (!f.timesUp())
                {
                    f.giveFeature();
                }
                else
                {
                    f.takeFeature();
                    if (f.Finished)
                    {
                        toRemoveFeature.Add(f);
                    }
                }
            }
            foreach(Feature f in toRemoveFeature)
            {
                featureList.Remove(f);
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