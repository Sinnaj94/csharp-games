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


        public CollectableBullet(Vector2 position, float rotation, Texture texture)
        {
            this.body = new FarseerPhysics.Dynamics.Body(this.world, position, rotation);
            Sprite = new Sprite(texture);
            Sprite.Position = Vector2fExtensions.ToSf(position);
            Sprite.Rotation = rotation;
        }
    }
}
