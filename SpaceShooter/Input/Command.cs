using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using FarseerPhysics;

namespace SpaceShooter
{
    //TODO: Controller einbinden!
    interface Command
    {
        void Execute(Ship p);
        //For Joystick
        void ExecuteJoystick(Ship p);
        float Strength { get; set; }
    }
    

    class ShootCommand : Command
    {
        public float Strength
        {
            get
            {
                return 0;
            }

            set
            {
                
            }
        }

        public void Execute(Ship p)
        {
            p.Shoot();
        }

        public void ExecuteJoystick(Ship p)
        {
            p.Shoot();
        }
    }

    class LeftCommand : Command
    {
        float strength = 10;
        public float Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public void Execute(Ship p)
        {
            p.Move(ConvertUnits.ToSimUnits(-1), 0);
        }

        public void ExecuteJoystick(Ship p)
        {
            p.Move(ConvertUnits.ToSimUnits(-1*Strength), 0);
        }
    }

    class UpCommand : Command
    {
        float strength;
        public float Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public void Execute(Ship p)
        {
            p.Move(0, ConvertUnits.ToSimUnits(-1));
        }

        public void ExecuteJoystick(Ship p)
        {
            p.Move(0, ConvertUnits.ToSimUnits(-1* Strength));
        }
    }

    class RightCommand : Command
    {
        float strength;
        public float Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public void Execute(Ship p)
        {
            p.Move(ConvertUnits.ToSimUnits(1), 0);
        }

        public void ExecuteJoystick(Ship p)
        {
            p.Move(ConvertUnits.ToSimUnits(1), 0);
        }
    }

    class DownCommand : Command
    {
        float strength;
        public float Strength
        {
            get
            {
                return strength;
            }

            set
            {
                strength = value;
            }
        }

        public void Execute(Ship p)
        {
            p.Move(0, ConvertUnits.ToSimUnits(1));
        }

        public void ExecuteJoystick(Ship p)
        {
            p.Move(0, ConvertUnits.ToSimUnits(1* Strength));
        }
    }
}
