using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SpaceShooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.Factories
{
    static class BulletFactory
    {
        public static Bullet CreateBullet(double x, double y, double angle, World world, Body parent)
        {
            Bullet b = new Bullet(parent);
            b.bulletRadius = ConvertUnits.ToSimUnits(5);
            b.angle = ConvertUnits.ToSimUnits(angle);
            b.body = BodyFactory.CreateCircle(world, b.bulletRadius, 1);
            b.body.Position = new Vector2((float)x, (float)y);
            b.body.BodyType = BodyType.Dynamic;
            b.body.IsBullet = true;
            b.BulletForce();
            return b;
        }

    }
}
