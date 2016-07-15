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
        TileMapBuilder tmb;
        Background background;
        Manhatten<Tile, Object> aStar;
        EnemyContainer eContrainer;
        SFML.Graphics.RenderWindow window;
        List<Football> _test;
        Lightcone lightcone;
        StatusBar statusbar = new StatusBar();

        public GameWorld(RenderWindow window)
        {
            _test = new List<Football>();
            this.window = window;
            tmb = new TileMapBuilder();

            initLevel();
            lightcone = new Lightcone(window);
            List<Vector2> a = tmb.GetObjectPositions();

            foreach (Vector2 t in a)
            {
                _test.Add(new Football(BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(16), 1f), t));
            }
        }

        public void initLevel()
        {
            tmb.initLevel(ref world, ref eContrainer, ref player);
            recalculatePath(player, new EventArgs());
            background = new Background();
            debug = new DebugDraw(world, window);
            input = new InputHandler(window);
            aStar = new Manhatten<Tile, Object>(tmb.Map.TileArray);
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

            debug.DrawDebugData();


            //tmb.Draw(target, states);
            eContrainer.Draw(target, states);
            player.Draw(target, states);
            //map.Draw(target, states);
            //enemy.DebugDraw(target, states);


            foreach (Football c in _test)
            {
                c.Draw(target, states);

            }
            lightcone.Draw(target, states);
            statusbar.Draw(target, states);
        }

        private void HandleInputCommands()
        {
            currentCommands = input.HandleInput();
            foreach (Command c in currentCommands)
            {
                if (!player.isDead)
                {
                    c.Execute(player);
                }
            }
            input.ResetInput();
        }

        public void Update()
        {
            window.SetView(setCameraToPlayer(window));
            if (eContrainer.AllEnemyDead)
            {
                initLevel();
            }
            eContrainer.Update();
            HandleInputCommands();
            player.Update();
            world.Step(.01639344262f);
        }
    }
}
