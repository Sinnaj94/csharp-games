using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Controllers;
using Microsoft.Xna.Framework;

namespace BreakoutBox2D
{
    class Player : GameObject
    {
        //  Interface
        Vector2 position;
        Vector2 size;
        float radius;

        

        // Own
        EdgeShape edgy;

        // Player is poly
        public Player(Vector2 size) : base(size)
        {
            this.size = size;
            position = new Vector2(100, 100);
            edgy = new EdgeShape(position, position + size);
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
