using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Program
    {

        static RenderWindow initWindow(uint ResolutionX, uint ResolutionY)
        {
            uint ResX = ResolutionX;
            uint ResY = ResolutionY;
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(ResX, ResY), "Pong");
            window.SetFramerateLimit(50);
            return window;
        }

        static void Main(string[] args)
        {

            RenderWindow window = initWindow(800,800);
            InputHandler inputHandler = new InputHandler();
            Player player = new Player(window);
            Ball ball = new Ball(window, new Vector2f(10, 5), new Vector2f(250, 250));

            while (window.IsOpen)
            {
                //Refreshing the window and drawing the shape
                window.Clear();
                inputHandler.listenToEvents();
                window.Draw(player.shape);
                window.Draw(ball.Circle);


                if (inputHandler.PlayerIsMoving)
                {
                    if (player.PlayerOnePosition.Y > 0 && inputHandler.deltaY < 0)
                    {
                        player.PlayerOnePosition += new Vector2f(0, inputHandler.deltaY);
                    }

                    if (player.PlayerOnePosition.Y < window.Size.Y - player.PlayerOneSize.Y && inputHandler.deltaY > 0)
                    {
                        player.PlayerOnePosition += new Vector2f(0, inputHandler.deltaY);
                    }
                }



                player.update();
                ball.updatePosition(player.YBoundMin, player.YBoundMax, player.PlayerOnePosition.X);
                window.Display();


                System.Diagnostics.Debug.Write(" Min: " + player.YBoundMin + " Max: " + player.YBoundMax);

            }
        }
    }
}
