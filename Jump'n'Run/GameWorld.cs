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
        Enemy enemy; 
        InputHandler input;
        List<Command> currentCommands;
        TileMapBuilder tmb;
        Map map;
        Manhatten<Tile, Object> aStar;

        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 0));
            Vector2 playerSize = new Vector2(16, 16);
            player = new Player(BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(10), 1));
            player.body.Position = new Vector2(ConvertUnits.ToSimUnits(200), ConvertUnits.ToSimUnits(200));
            enemy = new Enemy(BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(10), 1));
            enemy.body.Position = new Vector2(ConvertUnits.ToSimUnits(128), ConvertUnits.ToSimUnits(128));
            map = new Map(32, 32, 32);
            tmb = new TileMapBuilder(world, map);
            debug = new DebugDraw(world, window);
            input = new InputHandler(window);
            recalculatePath(player, new EventArgs());
            aStar = new Manhatten<Tile, Object>(map.TileArray);
        }

        public View setCameraToPlayer(RenderTarget target)
        {
            SFML.System.Vector2f defaultSize = target.DefaultView.Size;

            View v = new View(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(player.body.Position.X), ConvertUnits.ToDisplayUnits(player.body.Position.Y)), defaultSize);

            return v;
        }

        public void recalculatePath(object sender, EventArgs e)
        {
            player.onPositionChanged -= this.Player_onPositionChanged;
            player.onPositionChanged += this.Player_onPositionChanged;
        }

        private void Player_onPositionChanged(object sender, EventArgs e)
        {
            // enemy.calculatePathToSimTarget(player.body.Position, map);
            enemy.calculatePathToSimTargetUsingAStart(player.body.Position, aStar);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.SetView(setCameraToPlayer(target));
            debug.DrawDebugData();
            tmb.Draw(target, states);
            player.Draw(target, states);
            enemy.Draw(target, states);
            map.Draw(target, states);
            enemy.DebugDraw(target, states);
        }

        private void HandleInputCommands()
        {
            currentCommands = input.HandleInput();
            foreach(Command c in currentCommands)
            {
                c.Execute(player);
            }
            input.ResetInput();
        }

        public void Update()
        {
            enemy.Update();
            HandleInputCommands();
            player.Update();
            world.Step(.01639344262f);
        }
    }
}
