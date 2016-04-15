using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework;

namespace BreakoutBox2D
{
    class Player : GameObject
    {
        Vector2 position;
        Vector2 size;

        public Player()
        {
            position = new Vector2(100, 100);
            size = new Vector2(100, 100);
            AABB testBox = new AABB(position, position + size);
        }

        public override Vector2 Position()
        {
            return position;
        }

        public override Vector2 Size()
        {
            return size;
        }
    }
}
