using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public class MeleeProjectile : AbstractProjectile
    {
        
        public MeleeProjectile(AbstractCaracter caracter)
        {
            this.Caracter = caracter;
            this.world = Caracter.world;
            this.body = new Body(world, caracter.body.Position);
            CircleShape circle = new CircleShape(ConvertUnits.ToSimUnits(10), 10);
            circle.Position += Caracter.getBodyDirection() * 2.2f * circle.Radius;
            body.CreateFixture(circle);
            body.IsSensor = true;
            // Cat3 for projectiles
            body.CollisionCategories = Category.Cat3;
        }

        public override void Update()
        {
            this.body.Position = Caracter.body.Position;
        }
    }
}
