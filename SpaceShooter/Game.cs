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
            SFML.Graphics.RenderWindow window = new SFML.Graphics.RenderWindow(VideoMode.FullscreenModes[0], "Space Shooter", Styles.Fullscreen);
            window.SetVerticalSyncEnabled(true);
            window.SetFramerateLimit(61);
            return window;
        }


        static void Main(string[] args)
        {
            SFML.Graphics.RenderWindow window = InitWindow();
            BackgroundManager bg = new BackgroundManager();

           // EnemyShipContainer c = new EnemyShipContainer();
            //Ship player;

            /*
            c.AddShip(ShipFactory.CreateShip("Falcon", 100, 100, world);
            c.AddShip(ShipFactory.CreateShip("Destroyer", 200, 250));
            c.AddShip(ShipFactory.CreateShip("Destroyer", 500, 250));

            player = ShipFactory.CreateShip("Battlestar",200,200);
            */
            Battlefield battle = new Battlefield();
            Menu menu = new Menu();

            while (window.IsOpen)
            {
                window.Clear();

                if (menu.Active)
                {
                    menu.Update();
                }
                else
                {
                    battle.Update();
                }


                window.Draw(bg);
                window.Draw(battle);
                if (menu.Active)
                {
                    window.Draw(menu);

                }
                window.Display();
            }


        }


    }
}