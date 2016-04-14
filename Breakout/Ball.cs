using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Ball : Drawable
    {

        private CircleShape circle;

        public Ball(float radius)
        {
            circle = new CircleShape(radius);
        }

        public CircleShape Circle
        {
            get
            {
                return circle;
            }

            set
            {
                circle = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)Circle).Draw(target, states);
        }
    }
}
