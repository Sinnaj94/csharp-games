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

namespace SpaceShooter

    // Class to handle enemy Ships (Spawn, Despawn, Damage, Collision, etc.)
{
    class EnemyShipContainer : SFML.Graphics.Drawable
    {
        private List<Ship> container;

        public EnemyShipContainer()
        {
            Container = new List<Ship>();
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
                if (container[i].col)
                {
                    container.RemoveAt(i);
                }
            }
        }

    }
}

