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

        //Finally, a list to store the commands
        List<Command> requestedCommands;
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
            RequestedCommands = new List<Command>();
        }

        /// <summary>
        /// Checks if any of the implemented keys are pressed.
        /// </summary>
        /// <returns>Returns a CommandList of the Requested Commands</returns>
        public List<Command> HandleInput()
        {
            if (Keyboard.IsKeyPressed(_buttonShoot))
            {
                AddCommandToList(buttonShoot);
            }
            if (Keyboard.IsKeyPressed(_buttonLeft))
            {
                AddCommandToList(buttonLeft);

            } if (Keyboard.IsKeyPressed(_buttonRight))
            {
                AddCommandToList(buttonRight);
            }
            if (Keyboard.IsKeyPressed(_buttonUp))
            {
                AddCommandToList(buttonUp);
            }
            if (Keyboard.IsKeyPressed(_buttonDown))
            {
                AddCommandToList(buttonDown);
            }
            return RequestedCommands;
        }

        /// <summary>
        /// Adds a specific Command to the List
        /// </summary>
        /// <param name="c">The Command that should be added to the Command List</param>
        private void AddCommandToList(Command c)
        {
            if (!RequestedCommands.Contains(c))
            {
                RequestedCommands.Add(c);
            }
        }


        internal List<Command> RequestedCommands
        {
            get
            {
                return requestedCommands;
            }

            set
            {
                requestedCommands = value;
            }
        }
    }
}
