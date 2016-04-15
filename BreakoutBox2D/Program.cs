using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;


namespace BreakoutBox2D
{
    class Program
    {
        static RenderWindow initWindow()
        {
            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "Pong", Styles.Fullscreen);
            window.SetFramerateLimit(50);
            return window;
        }

        static void Main(string[] args)
        {

            RenderWindow window = initWindow();
            View GameView = window.GetView();
            Player player = new Player();
     

            while (window.IsOpen)
            {
                window.Clear();
                window.Draw(player);
                window.Display();
            }

        }
    }
}
