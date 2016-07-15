using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics;
using FarseerPhysics.Factories;

namespace JumpAndRun
{
    class CollectableContainer : SFML.Graphics.Drawable
    {
        private List<CollectableBullet> container;
        private World world;
        Texture texture;

        public CollectableContainer(World world)
        {
            this.world = world;
            container = new List<CollectableBullet>();
            texture = new Texture(@"Resources\sprites\bulletSmall.png");
        }

        public void Add(Vector2 position, float rotation)
        {
            CollectableBullet b = new CollectableBullet(position, rotation, world, texture);
            b.body.Position = position;
            b.body.Rotation = rotation;
            container.Add(b);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (CollectableBullet b in container)
            {
                if (!b.WasColected)
                {
                    b.Draw(target, states);
                }
            }
        }
    }
}
