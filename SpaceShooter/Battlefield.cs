
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
        private List<PauseCommand> currentPauseCommands;
        private List<GameCommand> currentGameCommands;
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

        public void upgradePlayer(Battlefield b)
        {
            float tempX = ConvertUnits.ToDisplayUnits(player.body.Position.X);
            float tempY = ConvertUnits.ToDisplayUnits(player.body.Position.Y);
            player.body.Dispose();
            b.player = ShipFactory.CreateShip("Destroyer", tempX, tempY, world);
            input.P = player;
            playerHud = new HUD(player);
            player.body.CollisionCategories = Category.Cat6;
        }

        public Battlefield(RenderWindow window)
        {
            this.window = window;
            world = new World(new Vector2(0, 0));
            InitGlobalBounds();            
            input = new InputHandler();
            currentCommands = new List<Command>(10);
            currentGameCommands = new List<GameCommand>(10);
            currentPauseCommands = new List<PauseCommand>(10);
            initPlayer();
            c = new EnemyShipContainer(player.body);     
            timer = new SFML.System.Clock();
            deltaTime = SFML.System.Time.FromSeconds(3);
            debug = new DebugPhysics(world, window);
            pauseScreen = new pickShip(player);
        }
        public void initPlayer()
        {
            player = ShipFactory.CreateShip("Battlestar", window.Size.X / 4, window.Size.Y / 2, world);
            input.P = player;
            playerHud = new HUD(player);
            player.body.CollisionCategories = Category.Cat6;
        }
        public void InitGlobalBounds()
        {
            globalBounds = new Vector2(ConvertUnits.ToSimUnits(window.Size.X), ConvertUnits.ToSimUnits(window.Size.Y));
            Body globalBoundsCollision = BodyFactory.CreateBody(world);
            FixtureFactory.AttachEdge(new Vector2(0, 0), new Vector2(0, globalBounds.Y), globalBoundsCollision);
            FixtureFactory.AttachEdge(new Vector2(0, globalBounds.Y), new Vector2(globalBounds.X, globalBounds.Y), globalBoundsCollision);
            FixtureFactory.AttachEdge(new Vector2(globalBounds.X, globalBounds.Y), new Vector2(globalBounds.X, 0), globalBoundsCollision);
            FixtureFactory.AttachEdge(new Vector2(globalBounds.X, 0), new Vector2(0, 0), globalBoundsCollision);
            globalBoundsCollision.CollidesWith = Category.Cat6;
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
        public void HandleGameCommands()
        {
            currentGameCommands = input.HandleInputGame();
            foreach (GameCommand com in currentGameCommands)
            {
                com.Execute(this);
            }
            currentGameCommands.Clear();

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
            HandlePauseCommands();
            HandleGameCommands();

            if (!pauseScreen.IsPaused)
            {
                HandlePlayerCommands();
                SpawnEnemy();
                c.Update();
                player.Update();
                playerHud.Update();
                world.Step(.01639344262f);
            } else {
                pauseScreen.Update();
            }

        }
        public void HandlePauseCommands()
        {
            currentPauseCommands = input.HandleInputPause();
            foreach (PauseCommand com in currentPauseCommands)
            {
                com.Execute(pauseScreen);
            }
            currentPauseCommands.Clear();
        }
        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            if (!pauseScreen.IsPaused)
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
