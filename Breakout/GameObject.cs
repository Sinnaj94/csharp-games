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
    class GameObject : SFML.Graphics.Drawable
    {
        Score score;
        Ball ball;
        Player player;
        Player playerTwo;
        Vector2f windowSize;
        Item item;
        int gamestate;
        int difficulty;
        Player touchedLast;
        bool twoPlayerGame;
        public GameObject(Vector2f renderWindowSize)

        {
            difficulty = 11;
            windowSize = renderWindowSize;
        }

        public void init()
        {
            player = new Player(windowSize, new Vector2f(windowSize.X * 0.05f, windowSize.Y * 0.5f));
            playerTwo = new Player(windowSize, new Vector2f(windowSize.X * 0.95f, windowSize.Y * 0.5f));
            score = new Score(3);
            ball = new Ball(windowSize, new Vector2f(4, 4), new Vector2f(windowSize.X/2, windowSize.Y/2), 10);
            item = new Item(windowSize);
            //touched Last: who touched the ball last.
            touchedLast = null;
        }

        private void updateInput()
        {
            if (player == null)
            {
                init();
            }

            if (ManageInput.Instance.Up())
            {
                player.PlayerPosition += new Vector2f(0, -player.PlayerSpeed * player.SpeedFactor);
            }

            if (ManageInput.Instance.Down())
            {
                player.PlayerPosition += new Vector2f(0, player.PlayerSpeed * player.SpeedFactor);
            }

        }

        private void updateInputTwoPlayer()
        {
            if (player == null)
            {
                init();
            }

            if (ManageInput.Instance.W())
            {
                player.PlayerPosition += new Vector2f(0, -player.PlayerSpeed * player.SpeedFactor);
            }

            if (ManageInput.Instance.S())
            {
                player.PlayerPosition += new Vector2f(0, player.PlayerSpeed * player.SpeedFactor);
            }


            if (ManageInput.Instance.Up())
            {
                playerTwo.PlayerPosition += new Vector2f(0, -playerTwo.PlayerSpeed * playerTwo.SpeedFactor);
            }

            if (ManageInput.Instance.Down())
            {
                playerTwo.PlayerPosition += new Vector2f(0, playerTwo.PlayerSpeed * playerTwo.SpeedFactor);
            }

        }

        public void updateGame()
        {

            if (!twoPlayerGame)
            {
                gamestate = 1;
                updateInput();
            } else
            {
                gamestate = 6;
                updateInputTwoPlayer();
            }

            //Updates
            player.update();
            playerTwo.update();
            ball.updatePosition(player,playerTwo);
            
            //Get last touched player
            if(ball.TouchedLast == 0)
            {
                touchedLast = player;
            }else if(ball.TouchedLast == 1)
            {
                touchedLast = playerTwo;
            }
            else
            {
                touchedLast = null;
            }


            //Item tests
            if (item.Active) { 
                if (Collision.Instance.collide(item.Rectangle, ball.Circle))
                {

                    int feature = item.FeatureNr;
                    if(feature <= 4)
                    {
                        touchedLast.giveFeature(feature);
                    }
                    else
                    {
                        //hier müssen die features für den ball gemacht werden.
                        ball.giveFeature(feature);
                    }
                    

                    item = new Item(windowSize);
                }
            }
            else
            {
                if (item.timeOver())
                {
                    
                    item.Active = true;
                }
            }


            applyToPlayer(player);
            applyToPlayer(playerTwo);
            applyToBall();
        }

        public void applyToPlayer(Player p)
        {
            //Apply to player
            if (p.hasFeature())
            {
                if (p.timesUp())
                {
                    p.removeFeature();
                }
            }
        }

        public void applyToBall()
        {
            if (ball.hasFeature())
            {
                if (ball.timesUp())
                {
                    ball.removeFeature();
                }
            }
        }


        void resetGame()
        {
            gamestate = 2;
            player = null;
            playerTwo = null;
            score = null;
            ball = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
                player.Draw(target, states);
                playerTwo.Draw(target, states);
                ball.Draw(target, states);
                score.Draw(target, states);
                item.Draw(target, states);
                

            if (score.GameOver(ball.ScoreState))
            {
                resetGame();
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