﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Pong.Properties;

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

            RenderWindow window = initWindow(800, 800);
            SoundManager soundManage = new SoundManager();
            InputHandler inputHandler = new InputHandler();
            KIHandler watson = new KIHandler();
            Player player = new Player(window, new Vector2f(window.Size.X * 0.05f, window.Size.Y * 0.5f), true);
            Player Ki = new Player(window, new Vector2f(window.Size.X * 0.95f, window.Size.Y * 0.5f), false);
            Logic rulesystem = new Logic(player, Ki, 10,window);
            Ball ball = new Ball(window, new Vector2f(10, 5), new Vector2f(250, 250), 10, rulesystem,soundManage);



            soundManage.addSound(@"Resources\hit.wav");
            soundManage.addSound(@"Resources\lose.wav");
            soundManage.addSound(@"Resources\side.wav");

            Menu menu = new Menu();

            int gamestate = 1;

            while (window.IsOpen)
            {

                if (gamestate == 0)
                {
                  
                }
                else
                {



                    //Input Handler
                    inputHandler.listenToEvents();
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

                    //Draw the screen
                    window.Clear();
                    window.Draw(player.Shape);
                    window.Draw(Ki.Shape);
                    window.Draw(ball.Circle);
                    window.Draw(menu);
                    window.Draw(player.ScoreText);
                    window.Draw(Ki.ScoreText);
                    
                    //window.Draw(menu);
                    window.Display();

                }

            }
            
        }
    }
}
