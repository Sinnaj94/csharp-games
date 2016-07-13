using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;

namespace JumpAndRun
{
    abstract class AbstractPhysicsObject : GameObject, SFML.Graphics.Drawable
    {


        public void Init(Body b)
        {
            body = b;
            body.Position = new Vector2(ConvertUnits.ToSimUnits(200), ConvertUnits.ToSimUnits(128));

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
}
