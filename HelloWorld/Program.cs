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
        static void Main(string[] args)
        {

            CircleShape shape = new CircleShape(100);
            shape.FillColor = Color.Cyan;
            shape.Position = new SFML.System.Vector2f(150, 150);



            RenderWindow window = new RenderWindow(new SFML.Window.VideoMode(500, 500), "HelloWorld");
            while (window.IsOpen)
            {
                window.Clear();
                window.Draw(shape);
                window.Display();
                

            }
        }
    }
}
