using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;
using Microsoft.Xna.Framework;

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
            Player player = new Player(new Vector2(100, 100));
            Obsticle round = new Obsticle(100);
            Container cont = new Container();

            while (window.IsOpen)
            {
                window.Clear();
                /*
                window.Draw(player);
                window.Draw(round);
*/
                window.Draw(cont);
                window.Display();
            }

        }
    }
}
