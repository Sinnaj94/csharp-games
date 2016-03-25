﻿using System;
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
            KIHandler watson = new KIHandler();
            Player player = new Player(window, new Vector2f(window.Size.X * 0.05f, window.Size.Y * 0.5f), true );
            Player Ki = new Player(window, new Vector2f(window.Size.X * 0.95f, window.Size.Y * 0.5f), false);
            Ball ball = new Ball(window, new Vector2f(10, 5), new Vector2f(250, 250),10);

            while (window.IsOpen)
            {
                //Refreshing the window and drawing the shape
                window.Clear();
                inputHandler.listenToEvents();
                window.Draw(player.Shape);
                window.Draw(Ki.Shape);
                window.Draw(ball.Circle);

                if (inputHandler.PlayerIsMoving)
                {
                    if (player.PlayerPosition.Y > 0 && inputHandler.deltaY < 0)
                    {
                        player.PlayerPosition += new Vector2f(0, inputHandler.deltaY);
                    }

                    if (player.PlayerPosition.Y < window.Size.Y - player.PlayerSize.Y && inputHandler.deltaY > 0)
                    {
                        player.PlayerPosition += new Vector2f(0, inputHandler.deltaY);
                    }
                }

                Ki.PlayerPosition += watson.moveToDirection(Ki.PlayerPosition, ball.Circle.Position, Ki.YBoundMin, Ki.YBoundMax);
                player.update();
                Ki.update();
                ball.updatePosition(player.YBoundMin, player.YBoundMax, player.PlayerPosition.X, Ki.YBoundMin, Ki.YBoundMax, Ki.PlayerPosition.X);
                window.Display();
            }
        }
    }
}
