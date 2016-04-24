using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    class newPlayer : Drawable
    {

        RectangleShape playerShape;

        public newPlayer(Vector2f size, Vector2f position)
        {
            PlayerShape = new RectangleShape(size);
            PlayerShape.Position = position;
            PlayerShape.FillColor = new Color(255, 255, 255, 255);
        }

        public RectangleShape PlayerShape
        {
            get
            {
                return playerShape;
            }

            set
            {
                playerShape = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)PlayerShape).Draw(target, states);
        }
    }
}
