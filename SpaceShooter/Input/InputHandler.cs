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
        uint nrShoot;
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


        bool joystickConnected;
        public InputHandler()
        {
            _buttonShoot = Keyboard.Key.Space;
            buttonShoot = new ShootCommand();
            nrShoot = 11;
            _buttonLeft = Keyboard.Key.Left;
            buttonLeft = new LeftCommand();
            _buttonRight = Keyboard.Key.Right;
            buttonRight = new RightCommand();
            _buttonUp = Keyboard.Key.Up;
            buttonUp = new UpCommand();
            _buttonDown = Keyboard.Key.Down;
            buttonDown = new DownCommand();
            RequestedCommands = new List<Command>();
            
            ConfigureJoystick(0);

        }

        private void ConfigureJoystick(uint nr)
        {
            Joystick.Update();
            if (Joystick.IsConnected(nr))
            {
                Console.Out.Write("Joystick " + nr + " is inserted and being used.");
                joystickConnected = true;
            }
            else
            {
                Console.Out.Write("Joystick is not inserted. Using Keyboard instead.");
                joystickConnected = true;
            }
            
        }


        private bool JoystickMovedInDirection(Joystick.Axis axis,int factor)
        {
            Joystick.Update();
            if(factor < 1)
            {
                if (Joystick.GetAxisPosition(0, axis) < 0.01*factor)
                {
                    return true;
                }
            }
            else
            {
                if (Joystick.GetAxisPosition(0, axis) > 0.01*factor)
                {
                    return true;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Checks if any of the implemented keys are pressed.
        /// </summary>
        /// <returns>Returns a CommandList of the Requested Commands</returns>
        public List<Command> HandleInputKeyboard()
        {
            //JoystickTesting();
            
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

        float getStrength(Joystick.Axis axis)
        {
            return Joystick.GetAxisPosition(0, axis);
        }

        //TODO: Im Moment noch sehr unsauber!!
        public List<Command> HandleInputJoystick()
        {
            if (joystickConnected)
            {
                
                
                if (Joystick.IsButtonPressed(0, nrShoot))
                {
                    setStrengthAndAdd(buttonShoot, 0);
                }
                if (JoystickMovedInDirection(Joystick.Axis.X, -1))
                {
                    setStrengthAndAdd(buttonLeft, getStrength(Joystick.Axis.X));

                }
                if (JoystickMovedInDirection(Joystick.Axis.X, 1))
                {
                    setStrengthAndAdd(buttonRight, getStrength(Joystick.Axis.X));
                }
                if (JoystickMovedInDirection(Joystick.Axis.Y, -1))
                {
                    setStrengthAndAdd(buttonUp, getStrength(Joystick.Axis.Y));
                }
                if (JoystickMovedInDirection(Joystick.Axis.Y, 1))
                {
                    setStrengthAndAdd(buttonDown, getStrength(Joystick.Axis.Y));
                }   
            }
            return RequestedCommands;
        }

        private void setStrengthAndAdd(Command c, float strength)
        {
            c.Strength = Math.Abs(strength/100);
            AddCommandToList(c);
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
