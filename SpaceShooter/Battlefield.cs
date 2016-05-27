using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.Graphics;
using SFML.Window;
using Microsoft.Xna.Framework;
using SpaceShooter.Factories;
using SpaceShooter.GameObjects;
using SFML.System;
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
        private SFML.System.Clock timer;
        private Time time;
        private Time deltaTime;

        HUD playerHud;
        public Battlefield()
        {
            globalBounds = new Vector2(ConvertUnits.ToSimUnits(1920) , ConvertUnits.ToSimUnits(1080));
            world = new World(new Vector2(0, 0));
            InitGlobalBounds();
            
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            player = ShipFactory.CreateShip("Battlestar", 200, 200, world);
            c = new EnemyShipContainer(player.body);
            //c.AddShip(ShipFactory.CreateShip("Falcon", 100, 0, world));
           // c.AddShip(ShipFactory.CreateShip("Battlestar", 100, 300, world));
            input.P = player;
            playerHud = new HUD(player);
            timer = new SFML.System.Clock();
            deltaTime = SFML.System.Time.FromSeconds(3);
            
        }

        public void InitGlobalBounds()
        {
            // FIND A MORE CLEVER WAY

        }

        public void Update()
        {    
            currentCommands = input.HandlePlayerInput();
            foreach (Command com in currentCommands)
            {
                com.Execute(player);
            }
            currentCommands.Clear();


        //    time += timer.ElapsedTime;

            if(timer.ElapsedTime > deltaTime)
            {
                c.AddShip(ShipFactory.CreateRandomShip(world));
                time = new Time();
                timer.Restart();
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
            playerHud.Draw(target, states);
        }
    }
}
