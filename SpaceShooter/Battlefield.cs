
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
        private DebugPhysics debug;
        private RenderWindow window;

        HUD playerHud;
        public Battlefield(RenderWindow window)
        {
            this.window = window;
            globalBounds = new Vector2(ConvertUnits.ToSimUnits(1920) , ConvertUnits.ToSimUnits(1080));
            world = new World(new Vector2(0, 0));
            InitGlobalBounds();
            
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            player = ShipFactory.CreateShip("Battlestar", 200, 200, world);
            c = new EnemyShipContainer(player.body);
            input.P = player;
            playerHud = new HUD(player);
            timer = new SFML.System.Clock();
            deltaTime = SFML.System.Time.FromSeconds(3);
            // player.body.
            debug = new DebugPhysics(world, window);

            //
            Body Spline = new Body(world, new Vector2(ConvertUnits.ToSimUnits(400), ConvertUnits.ToSimUnits(400)));
            Spline.BodyType = BodyType.Static;
            Fixture edge = FixtureFactory.AttachEdge(new Vector2(ConvertUnits.ToSimUnits(400), ConvertUnits.ToSimUnits(400)), new Vector2(ConvertUnits.ToSimUnits(800), ConvertUnits.ToSimUnits(400)), Spline);
            
            //
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
            playerHud.Update();
            world.Step(.016666f);
        }


        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            debug.DrawDebugData();
            player.Draw(target, states);
            c.Draw(target, states);
            playerHud.Draw(target, states);
            
        }
    }
}
