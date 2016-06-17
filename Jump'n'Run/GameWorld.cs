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
        private Body player;
        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 1));
            player = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(30), ConvertUnits.ToSimUnits(60), 10);
            player.Position = new Vector2(ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(0));
            player.BodyType = BodyType.Dynamic;
            player.LinearVelocity = new Vector2(1, 0);
            TileMapBuilder tmb = new TileMapBuilder(world);
            debug = new DebugDraw(world, window);
        }

        public View setCameraToPlayer(RenderTarget target)
        {
            SFML.System.Vector2f defaultSize = target.DefaultView.Size;
            return new View(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(player.Position.X), ConvertUnits.ToDisplayUnits(player.Position.Y)), defaultSize);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.SetView(setCameraToPlayer(target));
            debug.DrawDebugData();
        }

        public void Update()
        {
            world.Step(.01639344262f);
        }
    }
}
