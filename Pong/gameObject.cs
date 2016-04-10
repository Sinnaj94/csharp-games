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
        RenderWindow window;
        Item item;
        int gamestate;

        public GameObject(RenderWindow renderWindow)
        {
            window = renderWindow;
        }

        public void init()
        {
            watson = new KIHandler();
            player = new Player(window, new Vector2f(window.Size.X * 0.05f, window.Size.Y * 0.5f));
            Ki = new Player(window, new Vector2f(window.Size.X * 0.95f, window.Size.Y * 0.5f));
            score = new Score(3);
            ball = new Ball(window, new Vector2f(10, 5), new Vector2f(250, 250), 10);
            gamestate = 1;
            item = new Item(window);
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
            Ki.PlayerPosition += watson.moveToDirection(Ki.PlayerPosition, ball.Circle.Position, Ki.YBoundMin, Ki.YBoundMax);

            //Updates
            player.update();
            Ki.update();
            ball.updatePosition(player.YBoundMin, player.YBoundMax, player.PlayerPosition.X, Ki.YBoundMin, Ki.YBoundMax, Ki.PlayerPosition.X);

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
            try
            {
                player.Shape.Draw(target, states);
                Ki.Shape.Draw(target, states);
                ball.Circle.Draw(target, states);
                score.Draw(target, states);
            
                item.Rectangle.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("Gameobject draw failed");
            }

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

    }
}