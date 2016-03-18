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
        public InputHandler(Window window)
        {
            this.window = window;
        }

        Window window;
        int direction;

        public int Direction
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

        public void listenToEvents()
        {

      
                if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
                {
                    System.Diagnostics.Debug.Write("triggerd");
                    Direction = -10;
                } else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
                {
                    System.Diagnostics.Debug.Write("triggerd");
                    Direction = 10;
                } else
                {
                    Direction = 0;
                }


            
        }

    }
}
