using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;

namespace Breakout
{
    class Collision
    {
        private static Collision instance;
        int precision;
        public static Collision Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Collision();
                }
                return instance;
            }
        }

        public Collision()
        {
            precision = 4;
        }

        public bool collide(RectangleShape a, RectangleShape b)
        {
            if (a.Position.X < b.Position.X + b.Size.X && a.Position.X + a.Size.X > b.Position.X && a.Position.X < b.Position.Y + b.Size.Y && a.Size.Y + a.Position.Y > b.Position.Y)
            {
                return true;
            }
            return false;
        }

        public bool collide(RectangleShape a, CircleShape b)
        {
            List<Vector2f> points = getCollisionPoints(a);

            //liegt eine ecke im kreis?
            foreach(Vector2f point in points)
            {
                if(Math.Pow((point.X - b.Position.X),2) + Math.Pow((point.Y-b.Position.Y),2) <= b.Radius)
                {
                    return true;
                }
            }

            //liegt der kreismittelpunkt im rechteck?
            Vector2f middle = b.Position;
            if(middle.X >= getSide(a,'l') && middle.X <= getSide(a,'r') && middle.Y >= getSide(a,'u') && middle.Y <= getSide(a,'d'))
            {
                return true;
            }

            return false;
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
            List<Vector2f> edges = getEdges(a);
            List<Vector2f> re = new List<Vector2f>();

            re.AddRange(getIntermediatedPoints(edges[0],edges[1]));
            re.AddRange(getIntermediatedPoints(edges[1], edges[2]));
            re.AddRange(getIntermediatedPoints(edges[2], edges[3]));
            re.AddRange(getIntermediatedPoints(edges[3], edges[0]));

            re.AddRange(edges);

            return re;
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
