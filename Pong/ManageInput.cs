using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;

namespace Pong
{
    class ManageInput
    {
        private static ManageInput instance;

        private ManageInput() { }

        private bool returnPressed;
        private bool upPressed;
        private bool downPressed;
        private bool leftPressed;
        private bool rightPressed;

        public static ManageInput Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManageInput();
                }
                return instance;
            }
        }

        public bool Escape()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Escape);
        }

        public bool Return()
        {
            if(Keyboard.IsKeyPressed(Keyboard.Key.Return) && !returnPressed)
            {
                returnPressed = true;
                return true;
            } else
            {
                returnPressed = Keyboard.IsKeyPressed(Keyboard.Key.Return);
                return false;
            }
            
        }

        public bool Up()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Up);        
        }

        public bool Down()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Down);
        }
        public bool Left()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Left);
        }
        public bool Right()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Right);
        }

        public bool SlowUp()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up) && !upPressed)
            {
                upPressed = true;
                return true;
            }
            else
            {
                upPressed = Keyboard.IsKeyPressed(Keyboard.Key.Up);
                return false;
            }
        }

        public bool SlowDown()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Down) && !downPressed)
            {
                downPressed = true;
                return true;
            }
            else
            {
                downPressed = Keyboard.IsKeyPressed(Keyboard.Key.Down);
                return false;
            }
        }

        public bool SlowLeft()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left) && !leftPressed)
            {
                leftPressed = true;
                return true;
            }
            else
            {
                leftPressed = Keyboard.IsKeyPressed(Keyboard.Key.Left);
                return false;
            }
        }

        public bool SlowRight()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Right) && !rightPressed)
            {
                rightPressed = true;
                return true;
            }
            else
            {
                rightPressed = Keyboard.IsKeyPressed(Keyboard.Key.Right);
                return false;
            }
        }

    }
}
