using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics;
using FarseerPhysics.Factories;

namespace JumpAndRun
{
    class EnemyContainer : SFML.Graphics.Drawable
    {
        private List<Enemy> container;
        private World world;
        bool allEnemyDead;
        Player player;

        public EnemyContainer(World world)
        {
            this.world = world;
            container = new List<Enemy>();
        }

        public void Add(Vector2 position, float rotation)
        {
            Enemy e = new Enemy(BodyFactory.CreateCircle(world, ConvertUnits.ToSimUnits(10), 1), world);
            e.body.Position = position;
            e.body.Rotation = rotation;
            container.Add(e);
        }

        public void updatePaths(Vector2 playerPos, Manhatten<Tile, Object> aStar)
        {
            foreach (Enemy e in container)
            {
                e.calculatePathToSimTargetUsingAStart(playerPos, aStar);
            }
        }

        public void Update()
        {
            AllEnemyDead = true;

            foreach (Enemy e in container)
            {
                if (!e.isDead && Player.isDead)
                {
                    
                } else
                {
                    e.Update();
                }

                if (!e.isDead)
                {
                    AllEnemyDead = false;
                }
            }         
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Enemy e in container)
            {
                e.Draw(target, states);
            }
        }

        public bool AllEnemyDead
        {
            get
            {
                return allEnemyDead;
            }

            set
            {
                allEnemyDead = value;
            }
        }

        internal Player Player
        {
            get
            {
                return player;
            }

            set
            {
                player = value;
            }
        }
    }
}
