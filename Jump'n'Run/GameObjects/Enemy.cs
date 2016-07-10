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
  
        }

        public void moveToTarget(Point target)
        {
            /*
            if(body.LinearVelocity.X < maxSpeed)
            {
                body.ApplyLinearImpulse(new Vector2(1, 0));
            }
            */

            body.LinearVelocity = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= (float)maxSpeed;
            
            /*

            Vector2 force = new Vector2(target.XSim - body.Position.X, target.YSim - body.Position.Y);
            force.Normalize();
            body.ApplyForce(new Vector2(force.X, 0));
            
            if (body.Position.Y > target.YSim)
            {
                Console.WriteLine(body.Position.Y + " < " + target.YSim);
                body.ApplyForce(new Vector2(0, force.Y * 4));
            }
            */
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
        }
    }
}
