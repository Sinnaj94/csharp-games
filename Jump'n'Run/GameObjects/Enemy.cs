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
using FarseerPhysics;
using FarseerPhysics.Common;
using FarseerPhysics.Collision.Shapes;

namespace JumpAndRun
{
    class Enemy : AbstractCaracter, SFML.Graphics.Drawable
    {
        float damage;
        List<Point> path;
        public Body debugpath;
        Body radar;
        String rayhit;

        public Enemy(Body body, World world)
        {
            this.isWaiting = true;
            this.world = world;
            this.body = body;
            body.UserData = "Enemy";
            Path = new List<Point>();
            this.maxSpeed = 3;
            initAnimations("enemy", new Texture(@"Resources/Sprites/enemy1.png"));
            InitPhysics(@"Resources\json\physicsattributes.json");
            body.BodyType = BodyType.Dynamic;
            body.FixedRotation = true;
            body.LinearDamping = 10;
            initLineOfSight();
            body.CollidesWith = Category.Cat3;
        }
        public void initLineOfSight()
        {
            Vertices triangle = new Vertices();
            triangle.Add(new Vector2(0, -body.Position.Y));
            triangle.Add(new Vector2(5, 5));
            triangle.Add(new Vector2(-5, 5));
            body.FixedRotation = true;
            radar = FarseerPhysics.Factories.BodyFactory.CreatePolygon(world, triangle, 1);
            radar.Rotation = body.Rotation - (float)Math.PI / 2;
            PolygonShape p = new PolygonShape(triangle, 10);
            radar.Position = this.body.Position;
            radar.IsSensor = true;
            radar.OnCollision += Radar_OnCollision;
            radar.CollisionCategories = Category.Cat1;
        }
        private bool Radar_OnCollision(Fixture fixtureA, Fixture fixtureB, FarseerPhysics.Dynamics.Contacts.Contact contact)
        {
            createLineOfSight(fixtureB.Body.Position);
            Console.WriteLine("trig");
            return false;
        }
        public void createLineOfSight(Vector2 targetPosition)
        {
            world.RayCast(RayCallBack, this.body.Position, targetPosition);
        }
        private float RayCallBack(Fixture fixture, Vector2 point, Vector2 normal, float fraction)
        {
            if (fixture.Body.UserData != null)
            {
                rayhit = "" + fixture.Body.UserData;
            }
            if (fixture.Body.UserData == "player")
            {
                return fraction;
            }
            else
            {
                return 0f;
            }
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
        public void calculatePathToSimTargetUsingAStart(Vector2 position, Manhatten<Tile, Object> aStar)
        {
            List<Point> backupPath = Path;
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
            } else
            {
                Path = backupPath;
            }
        }
        public void MoveAndRotateTo(Point target)
        {
            Vector2 dif = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            dif.Normalize();
            body.LinearVelocity = dif;
            float angle = (float)(Math.Atan2(dif.X, dif.Y) * -1);
            body.Rotation = angle + (float)Math.PI / 2;
        }
        public void Follow()
        {
            if (Path.Count >= 2)
            {
                MoveAndRotateTo(Path[1]);
                if (Math.Abs(Path[1].XSim - this.body.Position.X) < 0.1 && Math.Abs(Path[1].YSim - this.body.Position.Y) < 0.1)
                {
                    Path.Remove(Path.First());
                }
            }
            else
            {
                body.LinearVelocity = new Vector2(0, 0);
                Statemachine.triggerAttack(0);
            }
        }
        public override void updateExtension()
        {
            if (rayhit == "player")
            {
                isWaiting = false;
            }
            radar.Position = body.Position;
            radar.Rotation = body.Rotation - (float)Math.PI / 2;

            if (!isWaiting) {
                Follow();
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
            } 
        }

    }
}
