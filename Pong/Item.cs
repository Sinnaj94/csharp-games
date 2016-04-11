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



        public Item(Vector2f window)
        {
            r = new Random();
            Active = true;
            Vector2u windowSize = new Vector2u((uint)window.X, (uint)window.Y);
            size = 40;
            position = getRandomPosition(windowSize);
            sizeChange = 10;
            rectangle = new RectangleShape(new Vector2f(size, size));
            rectangle.Position = position;
            rectangle.FillColor = Color.White;
            secondsToAppear = getRandomSeconds(1, 30);
        }

        private float getRandomSeconds(int min, int max)
        {
            return r.Next(min, max);
        }

        public void appearAfterSeconds(Time t)
        {
            clock = new Clock();
            Time elapsed = clock.ElapsedTime;
            Console.Out.WriteLine(elapsed.AsMilliseconds());
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
    }
}
