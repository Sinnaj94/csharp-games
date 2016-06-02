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
        private Ship _player;
        Battlefield b;

        public EnemyShipContainer(Battlefield b)
        {
            Container = new List<Ship>();
            this.b = b;
            this._player = b.Player;
            c = new Clock();
        }
        public void updatePlayerBody(Ship _player)
        {
            this._player = _player;
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
                container[i].MoveAndRotateTo(_player);
                container[i].shootRandomly();

                if (container[i].col)
                {
                    b.Score += 50;
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

