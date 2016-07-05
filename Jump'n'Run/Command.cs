using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
namespace JumpAndRun
{
    class CommandAttributes
    {
        float strength;
        public CommandAttributes(float strength)
        {
            this.strength = strength;
        }
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
            p.Jump();
        }
    }

    class GoRightCommand : Command
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

    class GoLeftCommand : Command
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
            p.move(-ca.Strength);
        }
    }

}
