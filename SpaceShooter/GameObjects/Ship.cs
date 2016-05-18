using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter.GameObjects
{
    class Ship : GameObject, IRenderable, IMovable
    {

        public Ship()
        {

        }

        public Ship(String name, int maxHP, double maxSpeed)
        {
            this.name = name;
            this.maxHP = maxHP;
            this.maxSpeed = maxSpeed;
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }

        public void Draw()
        {
            throw new NotImplementedException();
        }
    }
}
