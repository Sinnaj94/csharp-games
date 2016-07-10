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
           
            body.LinearVelocity = new Vector2((float)target.XSim - body.Position.X, (float)target.YSim - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= (float)maxSpeed;
            
           
            /*
            float angle = (float)(Math.Atan2(target.XSim - body.WorldCenter.X, target.YSim - body.WorldCenter.Y) * -1);
            body.Rotation = angle;
            */
        }

        public void calculatePathToTarget(Point end, Map map)
        {
            Vector2f startVector = Vector2fExtensions.ToSf(this.body.Position);
            Point start = new Point((int)startVector.X / 32, (int)startVector.Y / 32);
            SearchParameters sp = new SearchParameters(start, end, map);
            PathFinder pathFinder = new PathFinder(sp);
            Path = pathFinder.FindPath();
        }
    }

    class JumpingEnemy : Enemy
    {
        public JumpingEnemy(Body body)
        {
            Path = new List<Point>();
            this.maxSpeed = 1;
            //TODO: Make him Jump
            this.body = body;
            body.FixedRotation = true;
            body.Restitution = 1f;
            body.BodyType = BodyType.Dynamic;
        }
        public override void Update()
        {
            if(Path.Count != 0)
            {
                moveToTarget(Path[0]);
                if(Path[0].XSim - this.body.Position.X < 1)
                {
                    Path.Remove(Path.First());
                }
            }
        }
    }
}
