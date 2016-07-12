using JumpAndRun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    class Enemy : AbstractCaracter, SFML.Graphics.Drawable
    {
        float damage;
        List<Point> path;
        public Body debugpath;

        public Enemy(Body body)
        {
            this.body = body;
            Path = new List<Point>();
            this.maxSpeed = 3;
            initAnimations("enemy", new Texture(@"Resources/Sprites/enemy1.png"));
            InitPhysics(@"Resources\physicsattributes.json");
            body.BodyType = BodyType.Dynamic;
           
            foreach(Fixture f in body.FixtureList)
            {
                f.CollidesWith = Category.Cat2;
            }

            body.FixedRotation = true;
        }

        public List<Point> Path
        {
            get
            {
                return path;
            }

            set
            {
                path = value;
            }
        }

        public void moveToTarget(Point target)
        {
            body.LinearVelocity = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= (float)maxSpeed;
        }

        public void calculatePathToSimTargetUsingAStart(Vector2 position, Manhatten<Tile, Object> aStar)
        {
            Vector2f startVector = Vector2fExtensions.ToSf(this.body.Position);
            Vector2f endVector = Vector2fExtensions.ToSf(position);
            Point start = new Point((int)startVector.X / 32, (int)startVector.Y / 32);
            Point end = new Point((int)endVector.X / 32, (int)endVector.Y / 32);
            LinkedList<Tile> path = aStar.Search(start, end, null);
            Path = new List<Point>();
            if(path != null)
            {
                foreach (Tile t in path)
                {
                    Path.Add(new Point(t.X, t.Y));
                }
            }
        }

        public void MoveAndRotateTo(Point target)
        {
            Vector2 dif = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            dif.Normalize();
            body.LinearVelocity = dif;
            //body.LinearVelocity *= (float)maxSpeed;
             float angle = (float)(Math.Atan2(target.XSim - body.WorldCenter.X, target.YSim - body.WorldCenter.Y) * -1);
            angle = (float)Math.Atan2(dif.X, dif.Y);
            // body.Rotation = angle;
          //  float angle = Vector2fExtensions.GetAngle(body.Position, new Vector2(target.XSim, target.YSim));
            //angle = (angle * 180 / (float)Math.PI);


            body.Rotation = angle -  1/2 * (float)Math.PI;
            
            Console.WriteLine("angel: " + body.Rotation );

        }

        public override void updateExtension()
        {
            if (Path.Count != 0)
            {
              //  moveToTarget(Path[0]);
                MoveAndRotateTo(Path[0]);
                if (Math.Abs(Path[0].XSim - this.body.Position.X) < 0.1 && Math.Abs(Path[0].YSim - this.body.Position.Y) < 0.1)
                {
                    Path.Remove(Path.First());
                }
            }
        }

        public void DebugDraw(RenderTarget target, RenderStates states)
        {
            if(Path.Count > 0)
            {
                foreach (Point p in Path)
                {
                    RectangleShape rect = new RectangleShape(new Vector2f(2,2));
                    rect.Position = new Vector2f(p.X * 32, p.Y * 32 );
                    rect.FillColor = Color.Blue;
                    rect.Draw(target, states);
                }
            } else
            {
                body.LinearVelocity = new Vector2(0,0);
            }
        }

    }
}

/*
        public void calculatePathToSimTarget(Vector2 position, Map map)
        {
            Vector2f startVector = Vector2fExtensions.ToSf(this.body.Position);
            Vector2f endVector =    Vector2fExtensions.ToSf(position);
            Point start = new Point((int)startVector.X / 32, (int)startVector.Y / 32);
            Point end = new Point((int)endVector.X / 32, (int)endVector.Y / 32);
            SearchParameters sp = new SearchParameters(start, end, map);
            PathFinder pathFinder = new PathFinder(sp);
            Path = pathFinder.FindPath();

            /*
            FarseerPhysics.Common.Vertices verts = new FarseerPhysics.Common.Vertices();
            foreach (Point p in Path)
            {
                verts.Add(new Vector2(p.XSim, p.YSim));
            }
            if (verts.Count != 0)
            {
                debugpath = FarseerPhysics.Factories.BodyFactory.CreateChainShape(world, verts);
                debugpath.BodyType = BodyType.Static;
                debugpath.Enabled = false;
            }
            
        }

        public void calculatePathToTarget(Point end, Map map)
{
    Vector2f startVector = Vector2fExtensions.ToSf(this.body.Position);
    Point start = new Point((int)startVector.X / 32, (int)startVector.Y / 32);
    SearchParameters sp = new SearchParameters(start, end, map);
    PathFinder pathFinder = new PathFinder(sp);
    Path = pathFinder.FindPath();

    FarseerPhysics.Common.Vertices verts = new FarseerPhysics.Common.Vertices();
    foreach (Point p in Path)
    {
        verts.Add(new Vector2(p.XSim, p.YSim));
    }
    if (verts.Count != 0)
    {
        debugpath = FarseerPhysics.Factories.BodyFactory.CreateChainShape(world, verts);
        debugpath.BodyType = BodyType.Static;
        debugpath.Enabled = false;
    }
}

    */