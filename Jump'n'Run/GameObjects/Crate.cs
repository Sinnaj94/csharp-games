using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Crate : AbstractPhysicsObject
    {
        public Crate(World world, Vector2 position)
        {
            Body b = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(32), ConvertUnits.ToSimUnits(32), 1);
            Init(b);
            b.BodyType = BodyType.Dynamic;
            b.LinearDamping = 1.5f;
            b.AngularDamping = 1.5f;
            Sprite = new Sprite(new Texture(@"Resources\sprites\crate.png"));
            b.Position = position;
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }


    }
}
