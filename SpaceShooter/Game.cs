using SFML.Graphics;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.Factories;
using SpaceShooter.GameObjects;



namespace SpaceShooter
{
    class Game
    {
        static RenderWindow InitWindow()
        {
            RenderWindow window = new RenderWindow(VideoMode.DesktopMode, "Space Shooter", Styles.Fullscreen);
            window.SetFramerateLimit(61);
            return window;
        }

        static void Main(string[] args)
        {
            RenderWindow window = InitWindow();
            BackgroundManager bg = new BackgroundManager();
            EnemyShipContainer c = new EnemyShipContainer();

            c.AddShip(ShipFactory.CreateShip("Falcon", 100, 100));
            c.AddShip(ShipFactory.CreateShip("Destroyer", 200, 250));
            c.AddShip(ShipFactory.CreateShip("Destroyer", 500, 500));

            while (window.IsOpen)
            {
                window.Clear();
                window.Draw(bg);
                window.Draw(c);
                window.Display();
            }

        }
    }
}