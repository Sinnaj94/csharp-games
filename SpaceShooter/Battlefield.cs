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
        private BulletContainer bc;
        private InputHandler input;
        private List<Command> currentCommands;
        private Ship player;
        private Vector2 globalBounds;

        public Battlefield()
        {
            globalBounds = new Vector2(ConvertUnits.ToSimUnits(1920) , ConvertUnits.ToSimUnits(1080));
            world = new World(new Vector2(0, 0));
            InitGlobalBounds();
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            player = ShipFactory.CreateShip("Battlestar", 200, 200, world);
            c = new EnemyShipContainer(player.body);
            c.AddShip(ShipFactory.CreateShip("Falcon", 100, 0, world));
            c.AddShip(ShipFactory.CreateShip("Battlestar", 100, 300, world));
            input.P = player;
        }

        public void InitGlobalBounds()
        {
            // FIND A MORE CLEVER WAY
            BodyFactory.CreateRectangle(world, globalBounds.X, globalBounds.Y, 100).Position = new Vector2(ConvertUnits.ToSimUnits(-1920 + 960), ConvertUnits.ToSimUnits(540));
            BodyFactory.CreateRectangle(world, globalBounds.X, globalBounds.Y, 100).Position = new Vector2(ConvertUnits.ToSimUnits(1920 + 960), ConvertUnits.ToSimUnits(540));
            BodyFactory.CreateRectangle(world, globalBounds.X, globalBounds.Y, 100).Position = new Vector2(ConvertUnits.ToSimUnits(960), ConvertUnits.ToSimUnits(1080 + 540));
        }

        public void Update()
        {    
            currentCommands = input.HandlePlayerInput();
            foreach (Command com in currentCommands)
            {
                com.Execute(player);
            }
            currentCommands.Clear();

            
          //  player.body.Rotation += ConvertUnits.ToSimUnits(1);
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
