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
        static RenderWindow initWindow()
        {
            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "Breakout", Styles.Fullscreen);
            window.SetFramerateLimit(50);
            return window;
        }

        static void Main(string[] args)
        {
            //Taskbar.Hide();
            RenderWindow window = initWindow();

            View GameView = window.GetView();
            GameView.Center = new Vector2f(960 - 277,540 - 156);
            window.SetView(GameView);

            Menu menu = new Menu();
            Game gameObject = new Game(new Vector2f(1366, 768));
            GameOver gameOverScreen = new GameOver();
            Settings settings = new Settings(4,1);
            Intro intro = new Intro();
            Background bg = new Background(new Vector2f(1366, 768));



            // gamestates 0: menu, 1: game, 2: gameover, 3: settings, 4: exit, 5: Intro

            int gamestate = 0;

            while (window.IsOpen)
            {
                window.Clear();


                switch (gamestate)
                {
                    case 0:
                        window.Draw(menu);
                        gamestate = menu.updateGameState();
                        break;

                    case 1:
                        window.Draw(bg);
                        gameObject.TwoPlayerGame = false;
                        gameObject.updateGame();
                        window.Draw(gameObject);
                        gamestate = gameObject.Gamestate;
                        break;

                    case 2:
                        window.Draw(gameOverScreen);
                        gamestate = gameOverScreen.updateGameState();
                        break;

                    case 3:
                        window.Draw(settings);
                        gamestate = settings.update();
                        gameObject.Difficulty = settings.updateDifficulty();
                        break;

                    case 4:
                        //Taskbar.Show();
                        System.Environment.Exit(1);
                        break;

                    case 5:
                        gamestate = intro.updateIntro();
                        window.Draw(intro);
                        break;

                    case 6:
                        window.Draw(bg);
                        gameObject.TwoPlayerGame = true;
                        gameObject.updateGame();
                        window.Draw(gameObject);
                        gamestate = gameObject.Gamestate;
                        break;
                }

                window.Display();
            }
            
        }
    }
}
