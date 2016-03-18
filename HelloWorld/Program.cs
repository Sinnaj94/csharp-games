using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace HelloWorld
{
    class Program
    {
        private static object f;

        static void Main(string[] args)
        {
            //Create a new Circle with color and position
            CircleShape shape = new CircleShape(100);
            shape.FillColor = Color.Cyan;
            shape.Position = new SFML.System.Vector2f(150, 150);

            //Create the renderwindow with a size and height
            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(500, 500), "HelloWorld");
            while (window.IsOpen)
            {
                //Refreshing the window and drawing the shape
                window.Clear();
                window.Draw(shape);
                window.Display();
    

            }
        }
    }
}
