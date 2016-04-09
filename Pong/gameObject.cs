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
        InputHandler inputHandler;
        KIHandler watson;
        Logic rulesystem;
        Ball ball;
        Player player;
        Player Ki;
        RenderWindow window;
        SoundManager soundManage;
        Item item;
        int gamestate;

        public GameObject(RenderWindow renderWindow, SoundManager soundManager, InputHandler input)
        {
            window = renderWindow;
            inputHandler = input;
            soundManage = soundManager;
        }

        public void init()
        {
            watson = new KIHandler();
            player = new Player(window, new Vector2f(window.Size.X * 0.05f, window.Size.Y * 0.5f), true);
            Ki = new Player(window, new Vector2f(window.Size.X * 0.95f, window.Size.Y * 0.5f), false);
            rulesystem = new Logic(player, Ki, 2, window);
            ball = new Ball(window, new Vector2f(10, 5), new Vector2f(250, 250), 10, rulesystem, soundManage);
            gamestate = 1;
            item = new Item(window);
        }

        public void updateGame()
        {

            if(player == null)
            {
                init();
            }

            if (inputHandler.PlayerIsMoving)
            {
                if (inputHandler.deltaY < 0)
                {
                    player.PlayerPosition += new Vector2f(0, inputHandler.deltaY);
                }

                if (inputHandler.deltaY > 0)
                {
                    player.PlayerPosition += new Vector2f(0, inputHandler.deltaY);
                }
            }

            //KI Handler
            Ki.PlayerPosition += watson.moveToDirection(Ki.PlayerPosition, ball.Circle.Position, Ki.YBoundMin, Ki.YBoundMax);

            //Updates
            player.update();
            Ki.update();
            ball.updatePosition(player.YBoundMin, player.YBoundMax, player.PlayerPosition.X, Ki.YBoundMin, Ki.YBoundMax, Ki.PlayerPosition.X);

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                player.Shape.Draw(target, states);
                Ki.Shape.Draw(target, states);
                ball.Circle.Draw(target, states);
                player.ScoreText.Draw(target, states);
                Ki.ScoreText.Draw(target, states);
                item.Rectangle.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("Gameobject draw failed");
            }

            // check for gameover after rendering
            if (rulesystem.GameOver)
            {
                resetGame();
            }

        }

        void resetGame()
        {
            gamestate = 0;
            watson = null;
            player = null;
            Ki = null;
            rulesystem = null;
            ball = null;
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