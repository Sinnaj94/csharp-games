using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;

namespace BreakoutBox2D
{
    abstract class GameObject : Drawable
    {
        // Drawable object
        private Drawable drawable;

        // Constuctors for Circle and Rectangle shapes
        protected GameObject(Vector2 size)
        {
            drawable = new RectangleShape(new Vector2f(size.X, size.Y));
        }

        protected GameObject(float radius)
        {
            drawable = new CircleShape(radius);
        }

        // Seperate Draw methods
        private void drawRect()
        {
            ((RectangleShape)(drawable)).Position = new Vector2f(Position().X, Position().Y);

            //TODO replace with something clever
            ((RectangleShape)(drawable)).FillColor = new Color(255, 255, 255, 255);
        }

        private void drawCircle()
        {
            ((CircleShape)(drawable)).Position = new Vector2f(Position().X, Position().Y);

            //TODO replace with something clever
            ((CircleShape)(drawable)).FillColor = new Color(255, 255, 255, 255);
        }


        // basic update method
        private void update()
        {
            if(drawable is RectangleShape)
            {
                drawRect();
            }

            if(drawable is CircleShape)
            {
                drawCircle();
            }
        }


        // Abstract methods
        abstract public Vector2 Position();
        abstract public Vector2 Size();

        // Interface Drawable
        public void Draw(RenderTarget target, RenderStates states)
        {
            update();
            drawable.Draw(target, states);
        }
    }
}
