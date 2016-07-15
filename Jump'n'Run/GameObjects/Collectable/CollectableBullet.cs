using FarseerPhysics;
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
    class CollectableBullet : AbstractCollectable
    {
        public CollectableBullet(Vector2 position, float rotation, World world, Texture texture)
        {
            this.body = FarseerPhysics.Factories.BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(10), 10);
            this.body.BodyType = BodyType.Static;
            this.body.IsSensor = true;
            this.body.Position = position;
            this.body.CollidesWith = Category.Cat2;
            Sprite = new Sprite(texture);
            Sprite.Origin += new SFML.System.Vector2f(texture.Size.X / 2, texture.Size.Y / 2);
            Sprite.Position = Vector2fExtensions.ToSf(position);
            Sprite.Rotation = rotation;
        }
    }
}
