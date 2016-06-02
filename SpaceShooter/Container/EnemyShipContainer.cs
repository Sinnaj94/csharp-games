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

    // Class to handle enemy Ships (Spawn, Despawn, Damage, Collision, Movement, Rotation, etc.)
{
    class EnemyShipContainer : SFML.Graphics.Drawable
    {
        private List<Ship> container;
        private Body player;
        private Clock c;
        private int score;

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
        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Ship s in Container)
            {
                s.Draw(target, states);
            }
        }
        public void Update()
        {
            for(int i = container.Count - 1; i >= 0; i--)
            {
                container[i].Update();
                container[i].MoveAndRotateTo(player);
                container[i].shootRandomly();

                if (container[i].col)
                {
                    score += 50;
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

