
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
using SpaceShooter.Menuscreens;

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
        private Vector2 globalBounds;
        private SFML.System.Clock timer;
        private Time time;
        private Time deltaTime;
        private DebugPhysics debug;
        private RenderWindow window;
        private HUD playerHud;
        private bool pause = false;
        private pickShip pauseScreen;

        public Battlefield(RenderWindow window)
        {
            this.window = window;
            globalBounds = new Vector2(ConvertUnits.ToSimUnits(1920) , ConvertUnits.ToSimUnits(1080));
            world = new World(new Vector2(0, 0));
            InitGlobalBounds();            
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            initPlayer();
            c = new EnemyShipContainer(player.body);     
            timer = new SFML.System.Clock();
            deltaTime = SFML.System.Time.FromSeconds(3);
            debug = new DebugPhysics(world, window);
            pauseScreen = new pickShip(player);
        }
        public void initPlayer()
        {
            player = ShipFactory.CreateShip("Battlestar", 200, 200, world);
            input.P = player;
            playerHud = new HUD(player);
        }
        public void InitGlobalBounds()
        {
            // FIND A MORE CLEVER WAY
  
        }
        public void HandlePlayerCommands()
        {
            currentCommands = input.HandlePlayerInput();
            foreach (Command com in currentCommands)
            {
                com.Execute(player);
            }
            currentCommands.Clear();

        }
        public void SpawnEnemy()
        {
            if (timer.ElapsedTime > deltaTime)
            {
                c.AddShip(ShipFactory.CreateRandomShip(world));
                time = new Time();
                timer.Restart();
            }
        }
        public void Update()
        {
            if(player.Life < 0)
            {
                Pause = true;
            }

            if (!Pause)
            {
                HandlePlayerCommands();
                SpawnEnemy();
                c.Update();
                player.Update();
                playerHud.Update();
                
            } else {
               
            }
            world.Step(.01639344262f);
        }
        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (!Pause)
            {
                debug.DrawDebugData();
                player.Draw(target, states);
                c.Draw(target, states);
                playerHud.Draw(target, states);
            } else
            {
                pauseScreen.Draw(target, states);
            }

            
            //debug.DrawDebugData();

        }
        public bool Pause
        {
            get
            {
                return pause;
            }

            set
            {
                pause = value;
            }
        }
    }
}
