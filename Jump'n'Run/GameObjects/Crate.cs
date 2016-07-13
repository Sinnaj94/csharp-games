using FarseerPhysics.Dynamics;
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
        public Crate(Body b)
        {
            Init(b);
            b.BodyType = BodyType.Dynamic;
            b.LinearDamping = 1.5f;
            b.AngularDamping = 1.5f;
            Sprite = new Sprite(new Texture(@"Resources\sprites\crate.png"));
        }
        public override void Update()
        {
            //throw new NotImplementedException();
        }


    }
}
