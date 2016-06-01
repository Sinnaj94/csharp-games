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
        public static Bullet CreateBullet(double x, double y,double radius, World world, Body parent,double _dx, double _dy)
        {
            Bullet b = new Bullet(parent);
            b.bulletRadius = ConvertUnits.ToSimUnits(radius);
            b.body = BodyFactory.CreateCircle(world, b.bulletRadius, 1);
            b.body.Position = new Vector2((float)x, (float)y);
            b.body.BodyType = BodyType.Dynamic;
            b.body.IsBullet = true;
            b.body.CollisionCategories = Category.Cat3;
            b.dx = _dx;
            b.dy = _dy;
            b.speed = 400;
            b.BulletForce();
            return b;
        }

    }
}
