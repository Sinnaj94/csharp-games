using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Breakout.GameObjects
{
    class Item : GameObject
    {

        Feature f;
        bool active;
        RectangleShape rectangle;
        float speed;
        Random r;
        int featureNr;
        Sprite itemSprite;
        Texture itemTexture;
        Vector2f windowSize;
        Vector2f size;

        public Item(Vector2f window, Vector2f boxPosition, Vector2f boxSize)
        {
            //Initiate the RandomClass
            r = new Random();
            Active = true;
            size = new Vector2f(32, 75.5f);
            speed = getRandomSpeed();
            rectangle = new RectangleShape(size);
            rectangle.Position = boxPosition + boxSize/2 - size/2;
            FeatureNr = getRandomFeatureNr();
            rectangle.FillColor = getColor(featureNr);
            
            itemTexture = new Texture(@"Resources/item1.png");
            IntRect tmp = new IntRect(0, 0, (int)itemTexture.Size.X, (int)itemTexture.Size.Y);
            itemSprite = new Sprite(itemTexture, tmp);
            itemSprite.Scale = new Vector2f(size.X / itemTexture.Size.X, size.Y / itemTexture.Size.Y);
            this.windowSize = window;
        }

        private float getRandomSpeed()
        {
            float ret = r.Next(2, 5);
            ret += (float)r.NextDouble();
            return ret;
        }



        private Color getColor(int nr)
        {
            /*
            switch (nr)
            {
                case 1:
                    return Color.Blue;
                case 2:
                    return Color.Green;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.Red;
            }*/
            return Color.Blue;
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
                itemSprite.Draw(target, states);
            }
        }

        public override void update()
        {
            Rectangle.Position += new Vector2f(0,speed);
            itemSprite.Position = rectangle.Position;

        }

        public bool outOfRange()
        {
            if (Rectangle.Position.Y + size.Y >= windowSize.Y)
            {
                return true;
            }
            return false;
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
