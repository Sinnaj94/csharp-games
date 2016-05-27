using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using FarseerPhysics.Dynamics;

namespace SpaceShooter
{ 
    class DebugDraw : SFML.Graphics.Drawable
    {
        private SFML.Graphics.RenderWindow window;
        private List<Drawable> drawList;

        public DebugDraw(SFML.Graphics.RenderWindow window)
        {
            this.window = window;
        }

        public void DrawPoly(ref FarseerPhysics.Dynamics.Body testBody)
        {
           foreach (Fixture f in testBody.FixtureList)
            {
              
            }
        }

        void Drawable.Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
}
