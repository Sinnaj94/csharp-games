using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Pong
{
    class Item : Drawable
    {
        Vector2f position;
        float size;
        float sizeChange;
        bool active;
        RectangleShape rectangle;
        Clock clock;
        float secondsToAppear;
        Random r;
        int featureNr;


        public Item(Vector2f window)
        {
            r = new Random();
            Active = false;
            Vector2u windowSize = new Vector2u((uint)window.X, (uint)window.Y);
            size = 40;
            position = getRandomPosition(windowSize);
            sizeChange = 10;
            rectangle = new RectangleShape(new Vector2f(size, size));
            rectangle.Position = position;
            rectangle.FillColor = Color.White;
            //At least every minute, fastest is 8 seconds
            secondsToAppear = getRandomSeconds(8, 30);
            //Starts the clock.
            clock = new Clock();
            FeatureNr = getRandomFeatureNr();
            rectangle.FillColor = getColor(featureNr);
        }

        public Color getColor(int nr)
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
                case 5:
                    return Color.Green;
                case 6:
                    return Color.Yellow;
                case 7:
                    return Color.Cyan;
                case 8:
                    return Color.Magenta;
            }
            return Color.Black;
        }

        private int getRandomFeatureNr()
        {
            //Racket: 1-4
            //Good Features:
            //Feature 1: Bigger Racket 
            //Feature 2: Faster Racket
            //Bad Features:
            //Feature 3: Smaller Racket
            //Feature 4: Slower Racket

            //Ball: 5-8:
            //Neither good nor bad:
            //Feature 5: Bigger Ball
            //Feature 6: Smaller Ball
            //Feature 7: Slower Ball
            //Feature 8: Faster Ball
            return r.Next(1, 4);
        }
        private float getRandomSeconds(int min, int max)
        {
            return r.Next(min, max);
        }

        //Start time to appear
        public bool timeOver()
        {
            Time elapsed = clock.ElapsedTime;
            
            if(elapsed.AsSeconds() >= secondsToAppear)
            {
                return true;
            }
            return false;
        }

        Vector2f getRandomPosition(Vector2u windowSize)
        {
            Vector2f value;
            
            value.X = windowSize.X/2- size/2;
            value.Y = r.Next(0, (int)(windowSize.Y/2 - size/2));
            return value;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (active)
            {
                ((Drawable)Rectangle).Draw(target, states);
            }
            

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
