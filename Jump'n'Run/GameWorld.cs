using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using SFML.Graphics;

namespace JumpAndRun
{
    class GameWorld : SFML.Graphics.Drawable
    {
        private World world;
        DebugDraw debug;
        private Player player;
        InputHandler input;
        List<Command> currentCommands;
        List<GameCommand> currentGameCommands;
        TileMapBuilder tmb;
        Background background;
        Manhatten<Tile, Object> aStar;
        EnemyContainer eContrainer;
        SFML.Graphics.RenderWindow window;
        List<AbstractPhysicsObject> _test;
        Lightcone lightcone;
        CollectableContainer collectables;
        StatusBar statusbar;
        GameOver gameover;



        public GameWorld(RenderWindow window)
        {
            this.window = window;
            Tmb = new TileMapBuilder();
            gameover = new GameOver();
            initLevel();
            lightcone = new Lightcone(window);
        }

        public void initLevel()
        {
            Tmb.initLevel(ref world, ref eContrainer, ref player, ref collectables);
            recalculatePath(player, new EventArgs());
            background = new Background();
            debug = new DebugDraw(world, window);
            input = new InputHandler(window);
            aStar = new Manhatten<Tile, Object>(Tmb.Map.TileArray);
            statusbar = new StatusBar();
            _test = Tmb.GetObjectPositions(world);
        }

        public View setCameraToPlayer(RenderTarget target)
        {
            SFML.System.Vector2f defaultSize = target.DefaultView.Size;
            View v = new View(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(player.body.Position.X), ConvertUnits.ToDisplayUnits(player.body.Position.Y)), defaultSize);
            background.Update(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(player.body.Position.X) - target.Size.X * .5f, ConvertUnits.ToDisplayUnits(player.body.Position.Y) - target.Size.Y * .5f));
            return v;
        }

        public void recalculatePath(object sender, EventArgs e)
        {
            player.onPositionChanged -= this.Player_onPositionChanged;
            player.onPositionChanged += this.Player_onPositionChanged;
        }

        private void Player_onPositionChanged(object sender, EventArgs e)
        {
            eContrainer.updatePaths(player.body.Position, aStar);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            background.Draw(target, states);
            if (!player.isDead)
            {

                //debug.DrawDebugData();
                tmb.Draw(target, states);
                collectables.Draw(target, states);
                eContrainer.Draw(target, states);
                player.Draw(target, states);
                //map.Draw(target, states);
                //enemy.DebugDraw(target, states);
                foreach (AbstractPhysicsObject c in _test)
                {
                    c.Draw(target, states);

                }

                statusbar.Draw(target, states);
            }
            else
            {
                gameover.Draw(target, states);

            }
            lightcone.Draw(target, states);
        }

        private void HandleInputCommands()
        {
            currentCommands = input.HandleInput();
            currentGameCommands = input.GameCommandList;
            foreach (Command c in currentCommands)
            {
                if (!player.isDead)
                {
                    c.Execute(player);
                }
            }

            foreach (GameCommand c in currentGameCommands)
            {
                c.Execute(this);
            }

            input.ResetInput();
        }

        public void Update()
        {

            window.SetView(setCameraToPlayer(window));
            if (eContrainer.AllEnemyDead && !player.isDead)
            {
                initLevel();
            }
            eContrainer.Update();
            HandleInputCommands();
            player.Update();
            statusbar.Update(player.BulletCount, eContrainer.EnemyCount);
            world.Step(.01639344262f);
        }

        internal TileMapBuilder Tmb
        {
            get
            {
                return tmb;
            }

            set
            {
                tmb = value;
            }
        }
    }
}
