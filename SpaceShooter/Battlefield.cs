using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.Graphics;
using Microsoft.Xna.Framework;
using SpaceShooter.Factories;
using SpaceShooter.GameObjects;

namespace SpaceShooter
{
    // "Level" shall be loaded from file in the future
    class Battlefield : SFML.Graphics.Drawable
    {
        private World world;
        private EnemyShipContainer c;
        private InputHandler input;
        private List<Command> currentCommands;
        private Ship player;

        public Battlefield()
        {
            world = new World(new Microsoft.Xna.Framework.Vector2(0, 0));
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            player = ShipFactory.CreateShip("Battlestar", 200, 200, world);
            c = new EnemyShipContainer();
            c.AddShip(ShipFactory.CreateShip("Falcon", 100, 0, world));
            c.AddShip(ShipFactory.CreateShip("Battlestar", 100, 300, world));
        }

        public void update()
        {    
            currentCommands = input.HandleInput();
            foreach (Command com in currentCommands)
            {
                com.Execute(player);
            }
            currentCommands.Clear();

            c.Update();
            player.Update();
            world.Step(.016666f);
        }


        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            player.Draw(target, states);
            c.Draw(target, states);
        }
    }
}
