using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class RangeProjectile : AbstractProjectile
    {
        Vector2 direction;
        Vector2 speed;
        public RangeProjectile(AbstractCaracter caracter)
        {
            this.Caracter = caracter;
            this.world = Caracter.world;
            float radius = ConvertUnits.ToSimUnits(10);
            this.body = BodyFactory.CreateCircle(world, radius, 1, caracter.body.Position);
            this.body.Position += Caracter.getBodyDirection() * 2.2f * radius;
            body.IsBullet = true;
            body.IsSensor = true;
            body.CollisionCategories = Category.Cat3;
            direction = Caracter.getBodyDirection();
            speed = new Vector2(.2f, .2f);
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
        }

        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Vector2 contactNormal;
            FarseerPhysics.Common.FixedArray2<Vector2> contactPoints;
            contact.GetWorldManifold(out contactNormal, out contactPoints);
            Console.WriteLine("Trig");
            body.Awake = false;
            return true;
        }

        public override void Update()
        {
            if(this.body.Awake)
            {
                this.body.Position += direction * speed;
            }     
        }
    }
}
