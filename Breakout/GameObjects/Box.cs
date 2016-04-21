using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong.GameObjects
{
    class Box : GameObject
    {

        RectangleShape boxShape;

        public Box(Vector2f position, Vector2f size)
        {
            this.Position = position;
            this.Size = size;
            BoxShape = new RectangleShape(Size);
            BoxShape.Position = Position;
            BoxShape.FillColor = new Color(100, 200, 100, 200);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            BoxShape.Draw(target, states);
        }

        public override void update()
        {
            
        }

        public RectangleShape BoxShape
        {
            get
            {
                return boxShape;
            }

            set
            {
                boxShape = value;
            }
        }
    }
}
