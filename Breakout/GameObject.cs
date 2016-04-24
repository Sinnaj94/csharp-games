using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;
using FarseerPhysics.Controllers;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SFML.System;

namespace Breakout
{
    abstract class GameObject : Drawable
    {

        Vector2f position;
        Vector2f size;

        public abstract void Draw(RenderTarget target, RenderStates states);
        public abstract void update();
        public Vector2f Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;
            }
        }

        public Vector2f Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
            }
        }

    }
}
