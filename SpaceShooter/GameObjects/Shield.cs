using FarseerPhysics;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SpaceShooter.GameObjects
{
    class Shield : GameObject, IRenderable, IMovable
    {
        CircleShape circle;
        Ship ownedShip;
        int radius;
        float maxEnergy;
        float energy;
        bool active;
        Clock recoverClock;
        int timeToRecover;
        float recoverSpeed;
        public float Energy
        {
            get
            {
                return energy;
            }

            set
            {
                energy = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }



        public Shield(Ship ownedShip)
        {
            this.ownedShip = ownedShip;
            radius = 150;
            maxEnergy = 1f;
            recoverSpeed = .01f;
            Energy = maxEnergy;
            Active = true;
            circle = new CircleShape(radius);
            circle.FillColor = new Color(255, 228, 0, 128);
            timeToRecover = 3000;
            Update();
        }

        private void Recover()
        {


            if (recoverClock.ElapsedTime.AsMilliseconds() >= timeToRecover)
            {
                Active= true;
            }
        }


        public void IncreaseEnergy()
        {
            if(Energy+recoverSpeed <= maxEnergy)
            {
                Energy +=recoverSpeed;
            }
            else
            {
                //Energy = maxEnergy;
            }
            
            circle.FillColor = new Color(255, 228, 0, (byte)(128 * Energy / maxEnergy));
        }

        public void changeShield(float delta)
        {
            if(Energy + delta > 0)
            {
                Energy += delta;

            }
            else
            {
                Energy = 0;
                Active = false;
                recoverClock = new Clock();
            }
            circle.FillColor = new Color(255, 228, 0, (byte)(50 * Energy));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            
            circle.Draw(target, states);
        }

        public override void Update()
        {
            circle.Position = new Vector2f(ConvertUnits.ToDisplayUnits(ownedShip.body.Position.X)-radius, ConvertUnits.ToDisplayUnits(ownedShip.body.Position.Y)-radius);
            if (!Active)
            {

                Recover();
            }
            else
            {
                IncreaseEnergy();
            }
        }
    }
}
