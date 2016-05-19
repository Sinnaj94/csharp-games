using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SpaceShooter.GameObjects;
namespace SpaceShooter
{
    class InputHandler
    {
        //Shooting command
        Keyboard.Key _buttonShoot;
        Command buttonShoot;
        //Left command
        Keyboard.Key _buttonLeft;
        Command buttonLeft;
        //Right command
        Keyboard.Key _buttonRight;
        Command buttonRight;
        //Up Command
        Keyboard.Key _buttonUp;
        Command buttonUp;
        //Down Command
        Keyboard.Key _buttonDown;
        Command buttonDown;

        Ship p;
        public InputHandler()
        {
            _buttonShoot = Keyboard.Key.Space;
            buttonShoot = new ShootCommand();
            _buttonLeft = Keyboard.Key.Left;
            buttonLeft = new LeftCommand();
            _buttonRight = Keyboard.Key.Right;
            buttonRight = new RightCommand();
            _buttonUp = Keyboard.Key.Up;
            buttonUp = new UpCommand();
            _buttonDown = Keyboard.Key.Down;
            buttonDown = new DownCommand();
        }

        public Command HandleInput()
        {
            if (Keyboard.IsKeyPressed(_buttonShoot))
            {
                return buttonShoot;
            }else if (Keyboard.IsKeyPressed(_buttonLeft))
            {
                return buttonLeft;
            }else if (Keyboard.IsKeyPressed(_buttonRight))
            {
                return buttonRight;
            }
            else if (Keyboard.IsKeyPressed(_buttonUp))
            {
                return buttonUp;
            }else if (Keyboard.IsKeyPressed(_buttonDown))
            {
                return buttonDown;
            }
                return null;
        }
    }
}
