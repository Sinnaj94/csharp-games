using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;

namespace BreakoutBox2D
{
    abstract class GameObject : Drawable
    {
        private RectangleShape rect;

        // Abstract methods
        abstract public Vector2 Position();
        abstract public Vector2 Size();

        // Interface Drawable
        public void Draw(RenderTarget target, RenderStates states)
        {
            rect = new RectangleShape(new Vector2f(Size().X, Size().Y));
            rect.Position = new Vector2f(Position().X, Position().Y);
            rect.FillColor = new Color(255, 255, 255, 255);
            rect.Draw(target, states);
        }
    }
}
