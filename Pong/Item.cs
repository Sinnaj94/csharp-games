﻿using System;
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
        RectangleShape rectangle;

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

        

        public Item(RenderWindow window)
        {
            Vector2u windowSize = window.Size;
            Console.Write(window.Size.X + " " + window.Size.Y);
            position = getRandomPosition(windowSize);
            size = 40;
            sizeChange = 10;
            rectangle = new RectangleShape(new Vector2f(size, size));
            rectangle.Position = position;
            rectangle.FillColor = Color.Cyan;
        }

        Vector2f getRandomPosition(Vector2u windowSize)
        {
            Vector2f value;
            Random r = new Random();
            value.X = windowSize.X/2- size/2;
            value.Y = r.Next(0, (int)(windowSize.Y/2 - size/2));
            return value;
        }
    }
}
