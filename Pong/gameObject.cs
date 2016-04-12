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
        KIHandler watson;
        Score score;
        Ball ball;
        Player player;
        Player Ki;
        Vector2f windowSize;
        Item item;
        int gamestate;
        int difficulty;
        Player touchedLast;
        public GameObject(Vector2f renderWindowSize)
        {
            difficulty = 11;
            windowSize = renderWindowSize;
        }

        public void init()
        {
            watson = new KIHandler();
            player = new Player(windowSize, new Vector2f(windowSize.X * 0.05f, windowSize.Y * 0.5f));
            Ki = new Player(windowSize, new Vector2f(windowSize.X * 0.95f, windowSize.Y * 0.5f));
            score = new Score(3);
            ball = new Ball(windowSize, new Vector2f(4, 4), new Vector2f(windowSize.X/2, windowSize.Y/2), 10);
            gamestate = 1;
            item = new Item(windowSize);
            //touched Last: who touched the ball last.
            touchedLast = null;
        }

        public void updateGame()
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
                player.PlayerPosition += new Vector2f(0, player.PlayerSpeed *player.SpeedFactor);
            }


            //KI Handler
            Ki.PlayerPosition += watson.moveToDirection(Ki.PlayerPosition, ball.Circle.Position, Ki.YBoundMin, Ki.YBoundMax, (int)(difficulty*Ki.SpeedFactor));

            //Updates
            player.update();
            Ki.update();
            ball.updatePosition(player,Ki);
            
            //Get last touched player
            if(ball.TouchedLast == 0)
            {
                touchedLast = player;
            }else if(ball.TouchedLast == 1)
            {
                touchedLast = Ki;
            }
            else
            {
                touchedLast = null;
            }


            if (item.Active) { 
                if (Collision.Instance.collide(item.Rectangle, ball.Circle))
                {

                    //Feature List:
                    //Racket: 1-4
                    //Good Features:
                    //Feature 1: Bigger Racket 
                    //Feature 2: Faster Racket
                    //Bad Features:
                    //Feature 3: Smaller Racket
                    //Feature 4: Slower Racket
                    int feature = item.FeatureNr;
                    if(feature <= 4)
                    {
                        touchedLast.giveFeature(feature);
                    }
                    else
                    {

                    }


                    //Ball: 5-8:
                    //Neither good nor bad:
                    //Feature 5: Bigger Ball
                    //Feature 6: Smaller Ball
                    //Feature 7: Slower Ball
                    //Feature 8: Faster Ball

                    //Item wieder erstellen.
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
        }



        void resetGame()
        {
            gamestate = 2;
            watson = null;
            player = null;
            Ki = null;
            score = null;
            ball = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
                player.Draw(target, states);
                Ki.Draw(target, states);
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
    }
}