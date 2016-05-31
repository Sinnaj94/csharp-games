using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using FarseerPhysics;
using SFML.System;

namespace SpaceShooter
{
    //TODO: Controller einbinden!
    interface Command
    {
        void Execute(Ship p);
        //For Joystick

        Vector2f Strength { get; set; }
    }

    interface CommandStrength
    {
        float X { get; set; }
        float Y { get; set; }
    }

    interface MenuCommand
    {
        void Execute(Menu m);
    }



    class ShootCommand : Command
    {
        private Vector2f strength;
        public Vector2f Strength
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
            if(strength.X <= 1)
            {
                p.Shoot();

            }
            else
            {
                p.ShootBig();
            }
        }


    }

    class MoveCommand : Command
    {
        Vector2f strength;
        public Vector2f Strength
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
            p.Move(ConvertUnits.ToSimUnits(Strength.X), ConvertUnits.ToSimUnits(Strength.Y));
        }


    }

    class TurnCommand : Command
    {
        private Vector2f strength;
        public Vector2f Strength
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
            Vector2f dif = new Vector2f(ConvertUnits.ToDisplayUnits(p.body.Position.X) , ConvertUnits.ToDisplayUnits(p.body.Position.Y));
            strength -= dif;
            float angle = (float)(Math.Atan2(strength.X - p.body.WorldCenter.X, strength.Y - p.body.WorldCenter.Y) * -1);
            p.Rotate(angle);
            p.CursorPosition = strength+ new Vector2f(ConvertUnits.ToDisplayUnits(p.body.Position.X), ConvertUnits.ToDisplayUnits(p.body.Position.Y));
        }

    }

    class TurnCommandJoystick : Command
    {
        private Vector2f strength;
        public Vector2f Strength
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
            
            Console.Out.WriteLine(strength);
            float angle = (float)(Math.Atan2(strength.X + p.body.WorldCenter.X, strength.Y + p.body.WorldCenter.Y) * (-180 / Math.PI));
            p.Rotate(angle - 180);
            p.CursorPosition = strength + new Vector2f(ConvertUnits.ToDisplayUnits(p.body.Position.X), ConvertUnits.ToDisplayUnits(p.body.Position.Y));
        }

    }


    class MenuDownCommand : MenuCommand
    {
        public void Execute(Menu m)
        {
            m.navigateDown();
        }
    }

    class MenuUpCommand : MenuCommand
    {
        public void Execute(Menu m)
        {
            m.navigateUp();
        }
    }

    class MenuSelectCommand : MenuCommand
    {
        public void Execute(Menu m)
        {
            m.selectCurrent();
        }
    }




}
