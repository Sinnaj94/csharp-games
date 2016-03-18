using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong
{
    class Program
    {

        static void Main(string[] args)
        {

            Player PlayerOne = new Player();
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(500, 500), "HelloWorld");
            while (window.IsOpen)
            {
                //Refreshing the window and drawing the shape
                window.Clear();
                window.Draw(PlayerOne.shape);
                window.Display();


            }
        }
    }
}
