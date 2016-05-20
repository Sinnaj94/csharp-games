using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using SFML.Graphics;
using Microsoft.Xna.Framework;

namespace SpaceShooter
{
    // "Level" shall be loaded from file in the future
    class Battlefield : SFML.Graphics.Drawable
    {
        private World world;
        private Body globalBounds;
        private Body circle;

        private SFML.Graphics.CircleShape cshape;
        private SFML.Graphics.RectangleShape rect;
        
        float radius = 10;
        float width = 500;
        float height = 100;

        Vector2 pos1 = new Vector2(0, 600);
        Vector2 pos2 = new Vector2(0, 0);

        public Battlefield()
        {
            width = ConvertUnits.ToSimUnits(width);
            height = ConvertUnits.ToSimUnits(height);
            radius = ConvertUnits.ToSimUnits(radius);
            pos1 = ConvertUnits.ToSimUnits(pos1);
            pos2 = ConvertUnits.ToSimUnits(pos2);

            cshape = new CircleShape(ConvertUnits.ToDisplayUnits(radius));
            rect = new RectangleShape(new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(width), ConvertUnits.ToDisplayUnits(height)));
            cshape.FillColor = new Color(100, 100, 100, 100);
            rect.FillColor = new Color(100, 100, 100, 100);

            world = new World(new Microsoft.Xna.Framework.Vector2(0,0.5f));

            circle = BodyFactory.CreateCircle(world, radius, 10f);
            circle.BodyType = BodyType.Dynamic;
            globalBounds = BodyFactory.CreateRectangle(world, width, height, 10);
            circle.Position = pos2;
            globalBounds.Position = pos1;

        }

        public void update()
        {
            Console.Out.WriteLine("PosY: " + ConvertUnits.ToDisplayUnits(circle.Position.Y));
            Console.Out.WriteLine("size: " + ConvertUnits.ToDisplayUnits(globalBounds.Position.Y));
            cshape.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(circle.Position.X), ConvertUnits.ToDisplayUnits(circle.Position.Y - radius));
            rect.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(globalBounds.Position.X), ConvertUnits.ToDisplayUnits(globalBounds.Position.Y - height/2));
            world.Step(.016666f);
        }


        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            cshape.Draw(target, states);
            rect.Draw(target, states);
        }
    }
}
