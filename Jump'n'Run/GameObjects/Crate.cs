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
    class Crate : AbstractPhysicsObject
    {
        public Crate(Body b, Vector2 position)
        {
            Init(b);
            b.BodyType = BodyType.Dynamic;
            b.LinearDamping = 1.5f;
            b.AngularDamping = 1.5f;
            Sprite = new Sprite(new Texture(@"Resources\sprites\crate.png"));
            b.Position = position;
        }
        public override void Update()
        {
            //throw new NotImplementedException();
        }


    }
}
