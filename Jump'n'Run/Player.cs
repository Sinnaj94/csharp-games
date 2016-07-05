using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Player
    {
        public void Jump()
        {
            //TODO: Implement
            Output.Instance.print("Player jumps");
        }

        //Moves the player. Speed must be between 0 and 1 (for controller input)
        public void move(float speed)
        {
            //Set speed to 1, if it is too big
            if (Math.Abs(speed) > 1)
            {
                speed /= speed;
            }
            //TODO: Implement
            Output.Instance.print("Player walks (" + speed + ")");
        }
    }
}
