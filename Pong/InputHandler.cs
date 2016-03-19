using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;
using SFML.System;

namespace Pong
{
    class InputHandler
    {
        public InputHandler()
        {
            direction = 0;
            playerIsMoving = false;
        }

        int direction;
        bool playerIsMoving;

        public int deltaY
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        public bool PlayerIsMoving
        {
            get
            {
                return playerIsMoving;
            }

            set
            {
                playerIsMoving = value;
            }
        }

        public void listenToEvents()
        { 
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    //System.Diagnostics.Debug.Write("triggerd");
                    deltaY = -10;
                    playerIsMoving = true;
                } else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                   // System.Diagnostics.Debug.Write("triggerd");
                    deltaY = 10;
                    playerIsMoving = true;
                } else
                {
                    deltaY = 0;
                    playerIsMoving = false;
            }

        }

    }
}
