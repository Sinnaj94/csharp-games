using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Breakout
{
    class Item : GameObject
    {
        float size;

        bool active;
        RectangleShape rectangle;
        float speed;
        Random r;
        int featureNr;


        public Item(Vector2f window, Vector2f boxPosition, Vector2f boxSize)
        {
            //Initiate the RandomClass
            r = new Random();
            Active = true;
            size = 40;
            speed = getRandomSpeed();
            rectangle = new RectangleShape(new Vector2f(size, size));
            rectangle.Position = boxPosition + boxSize/2 - new Vector2f(size/2,size/2);
            FeatureNr = getRandomFeatureNr();
            rectangle.FillColor = getColor(featureNr);
        }

        private float getRandomSpeed()
        {
            float ret = r.Next(2, 5);
            ret += (float)r.NextDouble();
            return ret;
        }



        private Color getColor(int nr)
        {
            switch (nr)
            {
                case 1:
                    return Color.Green;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.Red;
            }
            return Color.Black;
        }

        private int getRandomFeatureNr()
        {
            //4 Items im Moment
            return r.Next(1, 4);
        }
        

       
        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (active)
            {

                ((Drawable)Rectangle).Draw(target, states);
            }
        }

        public override void update()
        {
            Rectangle.Position += new Vector2f(0,speed);
        }

        public RectangleShape Rectangle
        {
            get
            {
                return rectangle;
            }

            set
            {
                rectangle = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        public int FeatureNr
        {
            get
            {
                return featureNr;
            }

            set
            {
                featureNr = value;
            }
        }
    }
}
