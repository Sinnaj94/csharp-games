using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Pong
{
    class Item 
    {
        Vector2f position;
        float size;
        float sizeChange;
        public Item()
        {
            position = getRandomPosition();
            size = 20;
            float sizeChange = 10;
        }

        Vector2f getRandomPosition()
        {
            Vector2f value;
            Random r = new Random();
            value.X = 400-size/2;
            value.Y = r.Next(0, (int)(400-size/2));
            return value;
        }
    }
}
