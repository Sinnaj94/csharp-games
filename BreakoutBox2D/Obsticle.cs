using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using FarseerPhysics.Collision.Shapes;

namespace BreakoutBox2D
{
    class Obsticle : GameObject
    {

        Vector2 position;
        Vector2 size;
        float radius;
        CircleShape round;

        


        public Obsticle(float radius) : base(radius)
        {
            this.radius = radius;
            position = new Vector2(300, 300);
            round = new CircleShape(this.radius, 1000);
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
