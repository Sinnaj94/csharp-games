using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;


namespace Breakout
{
    class Program
    {

        enum canvas {canvasWidth = 600, canvasHight = 768}
        enum settigs { easy = 0, medium = 1, hard = 2, Sound0 = 0, Sound25 = 1, Sound50 = 2, Sound75 = 3, Sound100 = 4}

        static RenderWindow initWindow()
        {
            RenderWindow window = new RenderWindow(VideoMode.FullscreenModes[0], "Breakout", Styles.Fullscreen);
            window.SetFramerateLimit(50);
            return window;
        }

        static void Main(string[] args)
        {
            //Taskbar.Hide();
            RenderWindow window = initWindow();

            View GameView = window.GetView();
            GameView.Center = new Vector2f((float)canvas.canvasWidth / 2, (float)canvas.canvasHight / 2);
            window.SetView(GameView);

            Menu menu = new Menu();
            Game game = new Game(new Vector2f((float)canvas.canvasWidth, (float)canvas.canvasHight));
            GameOver gameOverScreen = new GameOver();
            Settings settings = new Settings((int)settigs.Sound100, (int)settigs.medium);
            Intro intro = new Intro();
            Background bg = new Background(new Vector2f((float)canvas.canvasWidth, (float)canvas.canvasHight));

            

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
                        if (game == null)
                        {
                            game = new Game(new Vector2f((float)canvas.canvasWidth, (float)canvas.canvasHight));
                        }
                        window.Draw(bg);
                        game.updateGame();
                        window.Draw(game);
                        gamestate = game.Gamestate;
                        break;

                    case 2:
                        if (game != null)
                        {
                            game = null;
                        }
                        window.Draw(gameOverScreen);
                        gamestate = gameOverScreen.updateGameState();
                        break;

                    case 3:
                        window.Draw(settings);
                        gamestate = settings.update();
                        break;

                    case 4:
                        //Taskbar.Show();
                        System.Environment.Exit(1);
                        break;

                    case 5:
                        gamestate = intro.updateIntro();
                        window.Draw(intro);
                        break;
                }

                window.Display();

                if (ManageInput.Instance.Escape())
                {
                    System.Environment.Exit(0);
                }

            }
            
        }
    }
}
