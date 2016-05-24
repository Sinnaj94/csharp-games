using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace SpaceShooter.GameObjects
{
    class Bullet : GameObject, IRenderable, IMovable
    {
        public double angle;
        public float bulletRadius;
        public int timeToLive = 0;
        public int timeToLiveMax = 100;
        CircleShape tmp;
        Body parent;

        public Bullet(Body parent)
        {
            this.parent = parent;
        }

        public void BulletForce()
        {
            body.ApplyForce(new Vector2(ConvertUnits.ToSimUnits(0), ConvertUnits.ToSimUnits(-400)), body.WorldCenter);
            tmp = new CircleShape(ConvertUnits.ToDisplayUnits(bulletRadius));
            tmp.FillColor = new Color(255, 255, 0, 255);
            body.IgnoreCollisionWith(parent);
        }

        public override void Update()
        {
            timeToLive++;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            tmp.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits((float)body.Position.X), ConvertUnits.ToDisplayUnits((float)body.Position.Y));
            tmp.Draw(target, states);
        }
    }
}
