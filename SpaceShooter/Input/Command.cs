using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
namespace SpaceShooter
{
    //TODO: Controller einbinden!
    interface Command
    {
        void Execute(Ship p);
    }

    class ShootCommand : Command
    {
        public void Execute(Ship p)
        {
            throw new NotImplementedException();
        }
    }

    class LeftCommand : Command
    {
        public void Execute(Ship p)
        {
            p.Move(-1, 0);
        }
    }

    class UpCommand : Command
    {
        public void Execute(Ship p)
        {
            p.Move(0,-1);
        }
    }

    class RightCommand : Command
    {
        public void Execute(Ship p)
        {
            p.Move(1, 0);
        }
    }

    class DownCommand : Command
    {
        public void Execute(Ship p)
        {
            p.Move(0,1);
        }
    }
}
