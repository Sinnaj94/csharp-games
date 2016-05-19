using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
namespace SpaceShooter
{
    class InputHandler
    {
        Keyboard.Key _buttonShoot;
        Command buttonShoot;
        Player p;
        public InputHandler()
        {
            _buttonShoot = Keyboard.Key.Space;
        }

        public Command HandleInput()
        {
            if (Keyboard.IsKeyPressed(_buttonShoot))
            {
                return buttonShoot;
            }
            return null;
        }
    }
}
