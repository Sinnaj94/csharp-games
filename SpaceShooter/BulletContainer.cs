using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SpaceShooter.GameObjects;

namespace SpaceShooter
{
    class BulletContainer : IRenderable
    {
        private List<Bullet> container;

        public BulletContainer()
        {
            Container = new List<Bullet>();
        }

        internal List<Bullet> Container
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

        public void AddBullet(Bullet b)
        {
            Container.Add(b);
        }

        public void DeleteBullet(Bullet b)
        {
            b = null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Bullet b in Container)
            {
                b.Draw(target, states);
            }
        }

        public void Update()
        {
            for (int i = container.Count - 1; i >= 0; i--)
            {
                container[i].Update();
                
                if (container[i].timeToLive >= container[i].timeToLiveMax || container[i].Col)
                {
                    container[i].body.Dispose();
                    container.RemoveAt(i);
                }
            }
        }

    }
}
