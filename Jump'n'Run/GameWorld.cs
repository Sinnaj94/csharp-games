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
        public GameWorld(RenderWindow window)
        {
            world = new World(new Vector2(0, 0));
            Body body1 = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(300), ConvertUnits.ToSimUnits(300), 10);
            body1.Position = new Vector2(ConvertUnits.ToSimUnits(0), ConvertUnits.ToSimUnits(500));
            body1.BodyType = BodyType.Static;

            world = new World(new Vector2(0, 0));
            Body body2 = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(30), ConvertUnits.ToSimUnits(60), 10);
            body1.Position = new Vector2(ConvertUnits.ToSimUnits(0), ConvertUnits.ToSimUnits(200));
            body1.BodyType = BodyType.Dynamic;

            debug = new DebugDraw(world, window);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            debug.DrawDebugData();
        }

        public void Update()
        {
            world.Step(.01639344262f);
        }
    }
}
