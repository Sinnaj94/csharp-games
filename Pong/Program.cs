using System;
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
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(ResX, ResY), "Pong",Styles.Fullscreen);
            window.SetFramerateLimit(50);
            return window;
        }

        static void Main(string[] args)
        {
            RenderWindow window = initWindow(800, 800);
            SoundManager soundManage = new SoundManager();
            InputHandler inputHandler = new InputHandler();
            Menu menu = new Menu();
            GameObject gameObject = new GameObject(window, soundManage, inputHandler);
            int gamestate = 0;

            while (window.IsOpen)
            {
                window.Clear();
                inputHandler.listenToEvents();

                if (gamestate == 0)
                {
                    window.Draw(menu);

                    if (inputHandler.ReturnIsPressed)
                    {
                        gamestate = menu.returnNewGamestate();

                    }
                } 

                else if(gamestate == 1)
                {
                    gameObject.updateGame();
                    window.Draw(gameObject);
                    gamestate = gameObject.Gamestate;
                } 

                window.Display();
            }
            
        }
    }
}
