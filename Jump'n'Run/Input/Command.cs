using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using FarseerPhysics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    class CommandAttributes
    {
        Vector2f strength;
        int attackNr;

        public CommandAttributes(int nr,bool egal) {
            this.attackNr = nr;
        }
        public CommandAttributes(float x)
        {

            this.strength = new Vector2f(x, 0);
        }

        public CommandAttributes(float x, float y)
        {
            this.strength = new Vector2f(x, y);
        }

        public bool Normalize()
        {
            double v = Math.Pow(Math.Abs(strength.X), 2) + Math.Pow(Math.Abs(strength.Y), 2);
            if (Math.Sqrt(v) > 1)
            {
                DoNormalization((float)Math.Pow(v,2));

                return true;
            }
            return false;
        }

        private void DoNormalization(float v)
        {
            //TODO

        }

        public bool IsValid()
        {
            return (strength.X != 0 || strength.Y != 0);
        }

        public void Add(Vector2f _add)
        {
            strength += _add;
        }

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

        public int AttackNr
        {
            get
            {
                return attackNr;
            }

            set
            {
                attackNr = value;
            }
        }
    }


    interface Command
    {
        //Commandattributes
        CommandAttributes Ca { get; set; }
        void Execute(Player p);
    }

    //Command patterns:
    class JumpCommand : Command
    {
        CommandAttributes ca;
        public CommandAttributes Ca
        {
            get
            {
                return ca;
            }

            set
            {
                ca = value;
            }
        }

        public void Execute(Player p)
        {
            // p.Jump();
        }
    }

    class TurnCommand : Command
    {
        CommandAttributes ca;

        public CommandAttributes Ca
        {
            get
            {
                return ca;
            }

            set
            {
                ca = value;
            }
        }


        public void _Execute(Player p)
        {
            Vector2f dif = Vector2fExtensions.ToSf(p.body.Position);
            ca.Strength -= dif;
            Vector2 caSim = Vector2fExtensions.ToSimVector(ca.Strength);
            caSim -= p.body.Position;
            float angle = (float)(Math.Atan2(caSim.X - p.body.WorldCenter.X, caSim.Y - p.body.WorldCenter.Y) * -1);
            p.Rotate(angle);
            p.CursorPosition = ca.Strength + dif;
        }

        public void Execute(Player p)
        {
            Vector2 caSim = Vector2fExtensions.ToSimVector(ca.Strength);
            Vector2 dif = p.body.Position - caSim;
            dif.Normalize();
            float angle = (float)(Math.Atan2(dif.X, dif.Y) * -1);
            p.body.Rotation = angle - (float)Math.PI / 2;
        }
    }

    class AttackCommand : Command
    {
        CommandAttributes ca;
        public CommandAttributes Ca
        {
            get
            {
                return ca;
            }

            set
            {
                ca = value;
            }
        }

        public void Execute(Player p)
        {
            p.Statemachine.triggerAttack(ca.AttackNr);
        }
    }


    class GoCommand : Command
    {
        CommandAttributes ca;
        public CommandAttributes Ca
        {
            get
            {
                return ca;
            }

            set
            {
                ca = value;
            }
        }

        public void Execute(Player p)
        {
            p.move(ca.Strength);
        }
    }
}
