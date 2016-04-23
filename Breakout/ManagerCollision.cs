﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Pong
{
    class ManagerCollision
    {
        private static ManagerCollision instance;
        int precision;
        public static ManagerCollision Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerCollision();
                }
                return instance;
            }
        }

        public ManagerCollision()
        {
            precision = 4;
        }

        public bool collide(RectangleShape a, RectangleShape b)
        {
            return !(a.Position.X > b.Position.X + b.Size.X || a.Position.X + a.Size.Y < b.Position.X || a.Position.Y > b.Position.Y + b.Size.Y || a.Position.Y + a.Size.Y < b.Position.Y);
        }

        public bool collide(RectangleShape a, CircleShape b)
        {
            List<Vector2f> points = getCollisionPoints(a);
            //Fixed the Middle to be in the inside of the circle

            CircleShape fM = new CircleShape(b);
            fM.Position += new Vector2f(b.Radius, b.Radius);
            //liegt eine ecke im kreis?
            foreach (Vector2f point in points)
            {
                if(Math.Pow((point.X - fM.Position.X),2) + Math.Pow((point.Y- fM.Position.Y),2) <= b.Radius)
                {
                    return true;
                }
            }

            //liegt der kreismittelpunkt im rechteck?

            if(
            //Liegt die Mitte drin?
            pointInRect(fM.Position, a) ||
            //Liegen die Seiten drin?
            pointInRect(new Vector2f(fM.Position.X- fM.Radius, fM.Position.Y), a) ||
            pointInRect(new Vector2f(fM.Position.X + b.Radius, fM.Position.Y), a) ||
            pointInRect(new Vector2f(fM.Position.X, fM.Position.Y- fM.Radius), a) ||
            pointInRect(new Vector2f(fM.Position.X - fM.Radius, fM.Position.Y+b.Radius), a))
            {
                return true;
            }


            return false;
        }

        private bool pointInRect(Vector2f point, RectangleShape a)
        {
            if (point.X >= getSide(a, 'l') && point.X <= getSide(a, 'r') && point.Y >= getSide(a, 'u') && point.Y <= getSide(a, 'd'))
            {
                return true;
            }
            return false;
        }

        public Vector2f collideWithDirection(RectangleShape a, CircleShape b)
        {
            if(collide(a, b))
            {
                if (b.Position.Y <= a.Position.Y - (a.Size.Y / 2))
                {
                    return new Vector2f(1, -1);
                }
                //Hit was from below the brick

                if (b.Position.Y >= a.Position.Y + (a.Size.Y / 2))
                {
                    return new Vector2f(1, -1);
                }
                //Hit was from above the brick

                if (b.Position.X < a.Position.X)
                {
                    return new Vector2f(-1, 1);
                }
                //Hit was on left

                if (b.Position.X > a.Position.X)
                {
                    return new Vector2f(-1, 1);
                }
                //Hit was on right
            }
            return new Vector2f(1, 1);
        }

        public float getSide(RectangleShape rect,Char side)
        {
            //left: l, right : r, up : u, down : d
            switch (side)
            {
                case 'l':
                    return rect.Position.X;
                case 'r':
                    return rect.Position.X + rect.Size.X;
                case 'u':
                    return rect.Position.Y;
                case 'd':
                    return rect.Position.Y + rect.Size.Y;
            }
                

            return 0;
        }

        private List<Vector2f> getCollisionPoints(RectangleShape a)
        {
            return getEdges(a);
         /*   List<Vector2f> edges = getEdges(a);
            List<Vector2f> re = new List<Vector2f>();

            re.AddRange(getIntermediatedPoints(edges[0],edges[1]));
            re.AddRange(getIntermediatedPoints(edges[1], edges[2]));
            re.AddRange(getIntermediatedPoints(edges[2], edges[3]));
            re.AddRange(getIntermediatedPoints(edges[3], edges[0]));

            re.AddRange(edges);

            return re;*/
        }

        private List<Vector2f> getIntermediatedPoints(Vector2f A, Vector2f B)
        {
            //direction vector
            Vector2f _d = B - A;
            List<Vector2f> re = new List<Vector2f>();
            for(int i = 1; i <= precision; i++)
            {
                Vector2f add = A + _d*(i / (precision + 1));
                re.Add(add);
            }
            return re;
        }

        private List<Vector2f> getEdges(RectangleShape a)
        {
            List<Vector2f> list = new List<Vector2f>();
            Vector2f A = new Vector2f(a.Position.X, a.Position.Y);
            Vector2f B = new Vector2f(a.Position.X + a.Size.X, a.Position.Y);
            Vector2f C = new Vector2f(a.Position.X + a.Size.X, a.Position.Y + a.Size.Y);
            Vector2f D = new Vector2f(a.Position.X, a.Position.Y + a.Size.Y);

            list.Add(A);
            list.Add(B);
            list.Add(C);
            list.Add(D);
            return list;
        }

        public void test() {
            //Just a few debugging tests...
            Vector2f size = new Vector2f(10, 10);
            RectangleShape a = new RectangleShape(size);
            RectangleShape b = new RectangleShape(size);
            CircleShape c = new CircleShape(1);

            a.Position = new Vector2f(0, 0);
            b.Position = new Vector2f(0, 0);
            c.Position = new Vector2f(2, 2);

            if (collide(a, c))
            {
                Console.Out.WriteLine("True");
            }
            else
            {
                Console.Out.WriteLine("False");
            }
        }
    }
}
