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
    class Football : AbstractPhysicsObject
    {


        public Football(World world, Vector2 position)
        {
            Body b = BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(16), 1f,position);
            Init(b);
            b.BodyType = BodyType.Dynamic;
            //b.LinearDamping = .5f;
            //b.AngularDamping = .5f;
            b.Restitution = .8f;
            Sprite = new Sprite(new Texture(@"Resources\sprites\football.png"));
            b.Position = position;
            b.CollisionCategories = Category.Cat4;
            this.body = b;
        }
        public override void Update()
        {
            //throw new NotImplementedException();
        }


    }
}
