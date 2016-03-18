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



        static void Main(string[] args)
        {
            int X = 500
            Player PlayerOne = new Player();
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(500, 500), "HelloWorld");
            InputHandler inputHandler = new InputHandler(window);
            Ball ball = new Ball(new Vector2f(10, 0), new Vector2f(250, 250));
            window.SetFramerateLimit(50);

            while (window.IsOpen)
            {
                //Refreshing the window and drawing the shape
                window.Clear();
                inputHandler.listenToEvents();
                window.Draw(PlayerOne.shape);
                window.Draw(ball.circle);
               

                if (PlayerOne.PlayerOnePosition.Y > 0 && inputHandler.Direction == -10)
                {
                    PlayerOne.PlayerOnePosition += new Vector2f(0, inputHandler.Direction);
               
                }

                if (PlayerOne.PlayerOnePosition.Y < 400 && inputHandler.Direction == 10)
                {
                    PlayerOne.PlayerOnePosition += new Vector2f(0, inputHandler.Direction);
                    
                }
                PlayerOne.update();

                ball.updatePosition();



                window.Display();
            }
        }
    }
}
