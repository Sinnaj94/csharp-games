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

            // Shipfactory test
            ShipFactory.CreateShip("Battlestar");
            ShipFactory.CreateShip("Thunderbolt");
            ShipFactory.CreateShip("asd");
            while (window.IsOpen)
            {
                // Start game with gamestate ...
            
            }

        }
    }
}