using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;

namespace Pong
{
    class KIHandler
    {
        public Vector2f moveToDirection(Vector2f KIPosition, Vector2f Ballposition, float KIMin, float KIMax)
        {
            Vector2f deltaPositionVector = new Vector2f(0, 0);

            float KIposition = 0.5f * (KIMax + KIMin);

            if(!(Math.Abs(KIposition - Ballposition.Y) < 7))
            {
                if (KIposition < Ballposition.Y)
                {
                    deltaPositionVector.Y = 10;
                }
                else
                {
                    deltaPositionVector.Y = -10;
                }
            }

            return deltaPositionVector;
        }

    }
}
