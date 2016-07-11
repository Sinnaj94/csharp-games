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
    abstract class Enemy : GameObject, SFML.Graphics.Drawable
    {
        float damage;
        List<Point> path;
        public Body debugpath;
        public SFML.Graphics.Sprite enemysprite;

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

        public void Kill()
        {
            //TODO
        }

        public void GivePlayerDamage(Player p)
        {
            
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            enemysprite.Draw(target, states);
        }

        public void moveToTarget(Point target)
        {
            body.LinearVelocity = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= (float)maxSpeed;
        }

        public void jump(Point target)
        {
            if (body.LinearVelocity.Y < maxSpeed)
            {
                body.ApplyLinearImpulse(new Vector2(0, 2), new Vector2(target.XSim - body.Position.X, target.YSim - body.Position.Y));
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
        
        public void calculatePathToSimTargetUsingAStart(Vector2 position, SpatialAStar<Tile, Object> aStar)
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
            */
        }
    }




    class JumpingEnemy : Enemy
    {
        public JumpingEnemy(Body body, World world)
        {
            this.world = world;
            Path = new List<Point>();
            this.maxSpeed = 3;
            //TODO: Make him Jump
            this.body = body;
            body.FixedRotation = true;
            //   body.Restitution = 1f;
            body.Mass = 10;
            body.BodyType = BodyType.Dynamic;
            enemysprite = new SFML.Graphics.Sprite(new Texture(@"Resources\Sprites\enemy1.png"), new IntRect(0, 0, 32, 32));
        }
        public override void Update()
        {
            if(Path.Count != 0)
            {
                moveToTarget(Path[0]);
                if(Math.Abs(Path[0].XSim - this.body.Position.X) < 0.1 && Math.Abs(Path[0].YSim - this.body.Position.Y) < 0.1)
                {
                    Path.Remove(Path.First());
                }
            }
            enemysprite.Position = Vector2fExtensions.ToSf(body.Position);
        }
    }
}
