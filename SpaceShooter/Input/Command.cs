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
            //For mouse:
            /*Vector2f temp = new Vector2f(ConvertUnits.ToDisplayUnits(p.body.Position.X), ConvertUnits.ToDisplayUnits(p.body.Position.Y));
            strength = strength - temp;
            Console.Out.WriteLine("X:" + strength.X + " Y: " + strength.Y);*/
            Vector2f nullWinkel = new Vector2f(1, 0);
            double sqrtS = Math.Sqrt(Math.Pow(strength.X,2) + Math.Pow(strength.Y,2));
            double sqrtN = Math.Sqrt(Math.Pow(nullWinkel.X,2) + Math.Pow(nullWinkel.Y,2));
            double skalar = strength.X * nullWinkel.X + strength.Y * nullWinkel.Y;
            double angle = skalar / (sqrtS * sqrtN);

            angle *= 90;
            if(strength.Y > 0)
            {
                angle -= 180;
                angle *= -1;
            }
            p.Rotate(angle);
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
