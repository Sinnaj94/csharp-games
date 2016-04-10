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

        }

        public void updateGame()
        {
            if (player == null)
            {
                init();
            }

            if (ManageInput.Instance.Up())
            {
                player.PlayerPosition += new Vector2f(0, -10);
            }

            if (ManageInput.Instance.Down())
            {
                player.PlayerPosition += new Vector2f(0, 10);
            }


            //KI Handler
            Ki.PlayerPosition += watson.moveToDirection(Ki.PlayerPosition, ball.Circle.Position, Ki.YBoundMin, Ki.YBoundMax, difficulty);

            //Updates
            player.update();
            Ki.update();
            ball.updatePosition(player.YBoundMin, player.YBoundMax, player.PlayerPosition.X, Ki.YBoundMin, Ki.YBoundMax, Ki.PlayerPosition.X,player.Shape,Ki.Shape);

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