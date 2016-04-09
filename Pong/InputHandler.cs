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

        int direction;
        bool playerIsMoving;
        bool returnIsPressed;

        public InputHandler()
        {
            direction = 0;
            playerIsMoving = false;
        }

        public void listenToEvents()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape)){
                System.Environment.Exit(1);
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Return))
            {
                returnIsPressed = true;
            } else
            {
                returnIsPressed = false;
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                deltaY = -10;
                playerIsMoving = true;
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                deltaY = 10;
                playerIsMoving = true;
            }
            else
            {
                deltaY = 0;
                playerIsMoving = false;
            }

        }

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

        public bool ReturnIsPressed
        {
            get
            {
                return returnIsPressed;
            }

            set
            {
                returnIsPressed = value;
            }
        }
    }
}
