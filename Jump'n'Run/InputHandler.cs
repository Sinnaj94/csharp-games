using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
namespace JumpAndRun
{
    class InputHandler
    {
        List<Command> commandList;
        Command _goLeft;
        Command _goRight;
        Command _jump;
        Keyboard.Key leftKey;
        Keyboard.Key rightKey;
        Keyboard.Key jumpKey;
        bool joystickConnected;
        public InputHandler()
        {
            //Defining the Commands here
            _goLeft = new GoLeftCommand();
            _goRight = new GoRightCommand();
            _jump = new JumpCommand();
            //Building the Commandlist
            commandList = new List<Command>();

            //Setup the Joysticksettings
            setupJoystick(0);
            //Setup the Keys of the Keyboard
            setupKeyboard();
        }

        private void setupJoystick(uint nr)
        {
            Joystick.Update();
            if (Joystick.IsConnected(nr))
            {
                joystickConnected = true;
            }
        }

        private void setupKeyboard()
        {
            leftKey = Keyboard.Key.A;
            rightKey = Keyboard.Key.D;
            jumpKey = Keyboard.Key.Space;
        }

        public List<Command> HandleInput()
        {
            //KeyboardInput
            HandleKeyboardInput();
            HandleJoystickInput();
            return commandList;
        }

        private bool KeyDown(Keyboard.Key sKey)
        {
            return Keyboard.IsKeyPressed(sKey);
        }

        private void HandleKeyboardInput()
        {
            CommandAttributes ca = new CommandAttributes(1);
            if (KeyDown(leftKey))
            {
                AddCommandToList(_goLeft,ca);
            }
            if (KeyDown(rightKey))
            {
                AddCommandToList(_goRight,ca);
            }
            if (KeyDown(jumpKey))
            {
                AddCommandToList(_jump,ca);
            }
        }

        private void AddCommandToList(Command item,CommandAttributes ca)
        {
            if (!commandList.Contains(item))
            {
                item.Ca = ca;
                commandList.Add(item);
                Output.Instance.print("Command " + item.ToString() + " was added.");
            }
            else
            {
                Output.Instance.print("Command was not added, because it already exists.");
            }
        }

        public void ResetInput()
        {
            commandList = new List<Command>();
        }

        private void HandleJoystickInput()
        {
            if (joystickConnected)
            {

            }
        }

    }
}
