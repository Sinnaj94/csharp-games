using FarseerPhysics.Dynamics;
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
        public Football(Body b, Vector2 position)
        {
            Init(b);
            b.BodyType = BodyType.Dynamic;
            //b.LinearDamping = .5f;
            //b.AngularDamping = .5f;
            b.Restitution = .8f;
            Sprite = new Sprite(new Texture(@"Resources\sprites\football.png"));
            b.Position = position;
            b.CollisionCategories = Category.Cat4;
        }
        public override void Update()
        {
            //throw new NotImplementedException();
        }


    }
}
