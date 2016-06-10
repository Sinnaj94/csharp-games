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
        private Body body2;
        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 1));
            Body body1 = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(300), ConvertUnits.ToSimUnits(300), 10);
            body1.Position = new Vector2(ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(600));
            body1.BodyType = BodyType.Static;
            body1.Restitution = .5f;

            body2 = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(30), ConvertUnits.ToSimUnits(60), 10);
            body2.Position = new Vector2(ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(200));
            body2.BodyType = BodyType.Dynamic;

            TileMapBuilder tmb = new TileMapBuilder(world);

            debug = new DebugDraw(world, window);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
          //  Console.Write("" + body2.Friction);
            debug.DrawDebugData();
        }

        public void Update()
        {
            world.Step(.01639344262f);
        }
    }
}
