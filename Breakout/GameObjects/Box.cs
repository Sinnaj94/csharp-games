using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout.GameObjects
{
    class Box : GameObject
    {

        RectangleShape boxShape;
        int stillToHit;
        bool isSolid;
        public Box(Vector2f position, Vector2f size, char type)
        {
            this.Position = position;
            this.Size = size;
            BoxShape = new RectangleShape(Size);
            BoxShape.Position = Position;
            BoxShape.FillColor = new Color(100, 200, 100, 255);
            BoxShape.OutlineColor = new Color(255, 255, 255, 255);
            BoxShape.OutlineThickness = 1;
            //if its a solid one
            if(type != 's')
            {
                stillToHit = type - '0';
                isSolid = false;
            }
            else
            {
                boxShape.FillColor = new Color(0, 0, 0, 255);
                isSolid = true;
            }
            
            Console.Out.WriteLine(stillToHit);
            buildBox();
        }

        private void buildBox()
        {
            if (!isSolid)
            {
                switch (stillToHit)
                {
                    case 1:
                        BoxShape.FillColor = new Color(100, 200, 100, 255);
                        break;
                    case 2:
                        BoxShape.FillColor = new Color(100, 100, 200, 255);
                        break;
                    case 3:
                        BoxShape.FillColor = new Color(200, 100, 100, 255);
                        break;

                }
            }
           
            
        }

        public bool destroyBox()
        {
            if (!isSolid)
            {
                stillToHit--;
                if (stillToHit <= 0)
                {
                    return true;
                }
                buildBox();
                return false;
            }
            return false;
            
        }

        public bool substractPoint()
        {
            stillToHit--;
            if(stillToHit <= 0)
            {
                return true;
            }
            return false;
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
