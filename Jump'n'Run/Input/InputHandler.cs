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
        Command _go;
        Command _jump;
        Command turnCommand;
        SFML.System.Vector2i mousePosition;

        //Keyboard
        Keyboard.Key leftKey;
        Keyboard.Key rightKey;
        Keyboard.Key jumpKey;

        //Joystick
        uint jumpJoy;
        bool joystickConnected;
        uint joystickNr;

        //Standard Attributes
        CommandAttributes ca;
        //Negative
        CommandAttributes caN;

        //Axis definition
        Joystick.Axis runAxis;
        float threshold;
        public InputHandler()
        {
            //Defining the Commands here
            _go = new GoCommand();
            _jump = new JumpCommand();
            turnCommand = new TurnCommand();

            //Building the Commandlist
            commandList = new List<Command>();

            //Setup the Joysticksettings
            joystickNr = 0;
            setupJoystick(joystickNr);
            //Setup the Keys of the Keyboard
            setupKeyboard();

            ca = new CommandAttributes(1);
            caN = new CommandAttributes(-1);
            mousePosition = Mouse.GetPosition();
        }

        private void setupJoystick(uint nr)
        {
            Joystick.Update();
            if (Joystick.IsConnected(nr))
            {
                Output.Instance.print("Joystick is connected and will be used.");
                joystickConnected = true;
                jumpJoy = 0;
                runAxis = Joystick.Axis.X;
                threshold = .01f;
            }
            else
            {
                joystickConnected = false;
                Output.Instance.print("There was no Joystick at Port " + nr + " detected. You will have to play with Keyboard.");
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

        private bool MousePositionChanged()
        {
            SFML.System.Vector2i currentPos = Mouse.GetPosition();
            if (currentPos.X != mousePosition.X || currentPos.Y != mousePosition.Y)
            {
                mousePosition = currentPos;
                return true;
            }
            return false;
        }

        private void HandleKeyboardInput()
        {
            if (KeyDown(leftKey))
            {
                AddCommandToList(_go, caN);
            }
            if (KeyDown(rightKey))
            {
                AddCommandToList(_go, ca);
            }
            if (KeyDown(jumpKey))
            {
                AddCommandToList(_jump, ca);
            }
            if (MousePositionChanged())
            {
                CommandAttributes _ca = new CommandAttributes(mousePosition.X, mousePosition.Y);
                AddCommandToList(turnCommand, _ca);
            }
        }

        private void AddCommandToList(Command item, CommandAttributes ca)
        {
            if (!commandList.Contains(item))
            {
                item.Ca = ca;
                commandList.Add(item);
                //Output.Instance.print("Command " + item.ToString() + " was added.");
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
                //Always update the Joystick (otherwise it doesn't work.)
                Joystick.Update();
                //Check for the Buttons to be pressed
                if (Joystick.IsButtonPressed(joystickNr, jumpJoy)){
                    AddCommandToList(_jump, ca);
                }
                float _axisDirection = axisDirection(runAxis);
                if (Math.Abs(_axisDirection) > threshold)
                {
                    Output.Instance.print("Axis D " + _axisDirection);
                    CommandAttributes _ca = new CommandAttributes(_axisDirection/100);
                    AddCommandToList(_go, _ca);
                }
            }
        }

        private float axisDirection(Joystick.Axis axis)
        {
            return Joystick.GetAxisPosition(joystickNr,axis);
        }

        private void JoystickDebug()
        {
            for (uint i = 0; i < 32; i++)
            {
                if (Joystick.IsButtonPressed(joystickNr, i))
                {
                    Output.Instance.print("Nr " + i + " was pressed.");

                }

            }
        }

    }
}
