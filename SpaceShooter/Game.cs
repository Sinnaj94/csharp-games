using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.Factories;
using SpaceShooter.GameObjects;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;

namespace SpaceShooter
{
    class Game
    {
        static SFML.Graphics.RenderWindow InitWindow()
        {
            SFML.Graphics.RenderWindow window = new SFML.Graphics.RenderWindow(VideoMode.DesktopMode, "Space Shooter", Styles.Fullscreen);
            window.SetFramerateLimit(61);
            return window;
        }


        static void Main(string[] args)
        {
            SFML.Graphics.RenderWindow window = InitWindow();
            BackgroundManager bg = new BackgroundManager();
            EnemyShipContainer c = new EnemyShipContainer();

            c.AddShip(ShipFactory.CreateShip("Falcon", 100, 100));
            c.AddShip(ShipFactory.CreateShip("Destroyer", 200, 250));
            c.AddShip(ShipFactory.CreateShip("Destroyer", 500, 500));

            Battlefield battle = new Battlefield();

            InputHandler input = new InputHandler();
            while (window.IsOpen)
            {
                window.Clear();
                Command current = input.HandleInput();

                if (current != null)
                {
                    current.execute(c.Container[0]);
                }

                battle.update();


                window.Draw(bg);
                window.Draw(c);
                window.Draw(battle);
                window.Display();
            }

        }


    }
}