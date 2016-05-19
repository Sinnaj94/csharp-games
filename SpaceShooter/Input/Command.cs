using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
namespace SpaceShooter
{
    interface Command
    {
        void execute(Ship p);
    }

    class ShootCommand : Command
    {
        public void execute(Ship p)
        {
            throw new NotImplementedException();
        }
    }

    class LeftCommand : Command
    {
        public void execute(Ship p)
        {
            p.move(-p.maxSpeed, 0);
        }
    }

    class UpCommand : Command
    {
        public void execute(Ship p)
        {
            p.move(0,-p.maxSpeed);
        }
    }

    class RightCommand : Command
    {
        public void execute(Ship p)
        {
            p.move(p.maxSpeed, 0);
        }
    }

    class DownCommand : Command
    {
        public void execute(Ship p)
        {
            p.move(0,p.maxSpeed);
        }
    }
}
