using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace SpaceShooter.GameObjects
{
    class Bullet : GameObject, IRenderable, IMovable
    {
        public double angle;
        public float bulletRadius;
        public int timeToLive = 0;
        public int timeToLiveMax = 1000;
        CircleShape tmp;
        Body parent;
        bool col;

        public bool Col
        {
            get
            {
                return col;
            }

            set
            {
                col = value;
            }
        }

        public Bullet(Body parent)
        {
            this.parent = parent;
        }

        public void BulletForce()
        {
            body.ApplyForce(new Vector2(ConvertUnits.ToSimUnits(dx*speed), ConvertUnits.ToSimUnits(dy*speed)), body.WorldCenter);
            tmp = new CircleShape(ConvertUnits.ToDisplayUnits(bulletRadius));
            tmp.FillColor = new Color(255, 255, 255, 255);
            body.IgnoreCollisionWith(parent);
            body.OnCollision += new OnCollisionEventHandler(Bullet_OnCollision);
        }
        public bool Bullet_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Col = true;
            return true;
        }

        public override void Update()
        {
            timeToLive++;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            tmp.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits((float)body.Position.X)-tmp.Radius, ConvertUnits.ToDisplayUnits((float)body.Position.Y) - tmp.Radius);
            tmp.Draw(target, states);
        }
    }
}
