using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using SFML.Graphics;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using SFML.System;
namespace SpaceShooter

    // Class to handle enemy Ships (Spawn, Despawn, Damage, Collision, etc.)
{
    class EnemyShipContainer : SFML.Graphics.Drawable
    {
        private List<Ship> container;
        private Body player;
        private Clock c;
        public EnemyShipContainer(Body player)
        {
            Container = new List<Ship>();
            this.player = player;
            c = new Clock();
        }

        internal List<Ship> Container
        {
            get
            {
                return container;
            }

            set
            {
                container = value;
            }
        }

        public void AddShip(Ship enemyShip)
        {
            enemyShip.initShootRandomClock();
            Container.Add(enemyShip);

        }

        public void DeleteShip(Ship enemyShip)
        {
            enemyShip = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Ship s in Container)
            {
                s.Draw(target, states);
            }
        }

        public void _Update()
        {
            foreach(Ship s in Container){
                s.Update();
                if (s.col)
                {
                    container.Remove(s);
                }
            }
        }

        public void Update()
        {
            for(int i = container.Count - 1; i >= 0; i--)
            {
                container[i].Update();
                container[i].RotateTo(player);
                container[i].shootRandomly();
                if (container[i].col)
                {
                    container[i].DeltaLife(-.1f);
                    container[i].col = false;
                }

                if (container[i].Life <= 0)
                {
                    container[i].body.Dispose();
                    container.RemoveAt(i);
                }

            }
        }

    }
}

