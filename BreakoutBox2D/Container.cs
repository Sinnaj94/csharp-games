using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
//using SFML.Graphics;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using Microsoft.Xna.Framework;


namespace BreakoutBox2D
{
    class Container : SFML.Graphics.Drawable
    {

        SFML.Graphics.Drawable rnd;
        Body myBody;
        World world;
        CircleShape shape;
        Fixture fixture;

        public Container()
        {
            world = new World(new Vector2(0, 10), new AABB(new Vector2(0, 0), new Vector2(500, 500)));
            myBody = new Body(world);
            myBody.BodyType = BodyType.Dynamic;
            shape = new CircleShape(10f, 10f);
            fixture = myBody.CreateFixture(shape);
            myBody.LinearVelocity = new Vector2(1, 1);
            myBody.Position = new Vector2(100, 100);
            world.Step(0.033333f);
            
            // BodyFactory.CreateBody(world, new Vector2(100, 100));
        }

        void SFML.Graphics.Drawable.Draw(SFML.Graphics.RenderTarget target, SFML.Graphics.RenderStates states)
        {
            rnd = new SFML.Graphics.CircleShape(10);
            ((SFML.Graphics.CircleShape)rnd).Position = new SFML.System.Vector2f(myBody.Position.X, myBody.Position.Y);
            ((SFML.Graphics.CircleShape)rnd).FillColor = new SFML.Graphics.Color(255, 255, 255, 255);
            ((SFML.Graphics.CircleShape)rnd).Draw(target, states);
            Console.Out.Write("Y: " + myBody.Position.Y);
        }
    }
}
