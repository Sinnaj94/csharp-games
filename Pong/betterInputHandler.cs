using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SFML.Window;

namespace Pong
{
    class BetterInputHandler
    {
        private static BetterInputHandler instance;

        private BetterInputHandler() { }

        public static BetterInputHandler Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BetterInputHandler();
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
            return Keyboard.IsKeyPressed(Keyboard.Key.Return);
        }

        public bool Up()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Up);        
        }

        public bool Down()
        {
            return Keyboard.IsKeyPressed(Keyboard.Key.Down);
        }

    }
}
