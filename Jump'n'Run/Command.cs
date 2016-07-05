using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    interface Command
    {
        void execute(Player p);
    }

    //Command patterns:
    class JumpCommand : Command
    {
        public void execute(Player p)
        {
            throw new NotImplementedException();
        }
    }

    class GoRightCommand : Command
    {
        public void execute(Player p)
        {
            throw new NotImplementedException();
        }
    }

    class GoLeftCommand : Command
    {
        public void execute(Player p)
        {
            throw new NotImplementedException();
        }
    }

}
