
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
        private PauseScreen pauseScreen;
        private int score;

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
            c = new EnemyShipContainer(this);
            timer = new SFML.System.Clock();
            deltaTime = SFML.System.Time.FromSeconds(3);
            debug = new DebugPhysics(world, window);
            pauseScreen = new PauseScreen();
        }
        public void UpgradePlayer(Battlefield b)
        {
            if(Score >= player.price && player.price != 0)
            {
                Score -= player.price;
                float tempX = ConvertUnits.ToDisplayUnits(Player.body.Position.X);
                float tempY = ConvertUnits.ToDisplayUnits(Player.body.Position.Y);
                float tempRotation = Player.body.Rotation;
                Player.body.Dispose();
                b.Player = ShipFactory.UpgradeShip(b.Player.name, tempX, tempY, world);
                b.Player.body.Rotation = tempRotation;
                b.Player.body.BodyId = 1;
                input.P = Player;
                playerHud = new HUD(Player);
                Player.body.CollisionCategories = Category.Cat6;
                c.updatePlayerBody(Player);
                pause = false;           
            }

        }
        public void initPlayer()
        {
            Player = ShipFactory.CreateShip("Falcon", window.Size.X / 4, window.Size.Y / 2, world);
            input.P = Player;
            playerHud = new HUD(Player);
            Player.body.CollisionCategories = Category.Cat6;
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
                com.Execute(Player);
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
                Player.Update();
                playerHud.Update(score);
                world.Step(.01639344262f);
            } else {
                pauseScreen.Update(player);
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
            debug.DrawDebugData();
            Player.Draw(target, states);
            c.Draw(target, states);

            if (pauseScreen.IsPaused)
            {
                pauseScreen.Draw(target, states);
            }
            
            playerHud.Draw(target, states);
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
        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
            }
        }
        internal Ship Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
    }
}
