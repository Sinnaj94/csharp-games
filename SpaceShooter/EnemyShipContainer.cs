using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using SFML.Graphics;

namespace SpaceShooter

    // Class to handle enemy Ships (Spawn, Despawn, Damage, Collision, etc.)
{
    class EnemyShipContainer : SFML.Graphics.Drawable
    {
        private List<Ship> container;

        public EnemyShipContainer()
        {
            container = new List<Ship>();
        }
        public void AddShip(Ship enemyShip)
        {
            container.Add(enemyShip);
        }

        public void DeleteShip(Ship enemyShip)
        {
            // To be implemented
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Ship s in container)
            {
                s.Draw(target, states);
            }
        }

        public void Update()
        {
            foreach(Ship s in container){
                s.Update();
            }
        }


    }
}
