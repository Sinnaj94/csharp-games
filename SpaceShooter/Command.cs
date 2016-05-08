using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    interface Command
    {
        void execute(Player p);
    }
    class ShootCommand : Command
    {
        public void execute(Player p)
        {
            throw new NotImplementedException();
        }
    }
    
}
