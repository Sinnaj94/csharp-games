using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Windows.Forms;

namespace Breakout
{
    class Program
    {
        
        enum canvas {canvasWidth = 600, canvasHight = 768}
        enum settigs { easy = 0, medium = 1, hard = 2, Sound0 = 0, Sound25 = 1, Sound50 = 2, Sound75 = 3, Sound100 = 4}
        StartScreen a;
        int theIndex;
        bool fullScreen;
        public Program()
        {
            theIndex = 0;
            fullScreen = false;
        }

        private RenderWindow InitWindow()
        {
            RenderWindow window;
            if (fullScreen)
            {
                window = new RenderWindow(VideoMode.FullscreenModes[theIndex], "Breakout", Styles.Fullscreen);
            }
            else
            {
                window = new RenderWindow(VideoMode.DesktopMode, "Breakout");
            }
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(61);
            return window;
        }

        static void Main(string[] args)
        {
            Program a = new Program();
            a.DisposeTheSettings();
        }

        public void DisposeTheSettings()
        {
            a = new StartScreen();
            a.button1.Click += StartTheGame;
            for(int i = 0; i<VideoMode.FullscreenModes.Length; i++)
            {
                a.comboBox1.Items.Add(VideoMode.FullscreenModes[i].Width + " x " + VideoMode.FullscreenModes[i].Height);
            }
            a.comboBox1.SelectedIndexChanged += RefreshIndex;
            a.checkBox1.CheckedChanged += RefreshFullscreen;
            a.ShowDialog();
        }


        private void RefreshFullscreen(object sender, EventArgs e)
        {
            CheckBox a = (CheckBox)sender;
            fullScreen = a.Checked;
        }

        private void RefreshIndex(object sender, EventArgs e)
        {
            
            ComboBox a = (ComboBox)sender;
            Console.Out.WriteLine(a.SelectedIndex);
            theIndex = a.SelectedIndex;

        }

        private void StartTheGame(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            RenderWindow window = InitWindow();

            SFML.Graphics.View GameView = window.GetView();
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
