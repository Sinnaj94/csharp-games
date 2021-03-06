﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.System;

namespace JumpAndRun
{
    class InputHandler
    {
        List<Command> commandList;
        List<GameCommand> gameCommandList;
        Command _go;
        Command _jump;
        Command turnCommand;
        Command attack;
        Command escapeCommand;
        GameCommand restartCommand;

        SFML.System.Vector2i mousePosition;
        SFML.Graphics.RenderWindow window;
        //Keyboard
        Keyboard.Key leftKey;
        Keyboard.Key rightKey;
        Keyboard.Key jumpKey;
        Keyboard.Key downKey;
        Keyboard.Key upKey;
        Keyboard.Key rKey;
        Keyboard.Key escKey;
        // Mouse
        Mouse.Button leftClick;
        Mouse.Button rightClick;
        //Joystick
        uint jumpJoy;
        bool joystickConnected;
        uint joystickNr;

        //Standard Attributes
        CommandAttributes caX;
        CommandAttributes caY;

        //Negative
        CommandAttributes caXN;
        CommandAttributes caYN;

        //Clock
        CommandAttributes timed;


        //Axis definition
        Joystick.Axis runAxis;
        Joystick.Axis upAxis;

        Joystick.Axis rightAxisL;
        Joystick.Axis rightAxisU;

        float threshold;
        private uint attackJoy;
        private uint altAttackJoy;
        


        public InputHandler(SFML.Graphics.RenderWindow window)
        {
            //Defining the Commands here
            _go = new GoCommand();
            _jump = new JumpCommand();
            turnCommand = new TurnCommand();
            attack = new AttackCommand();
            //Building the Commandlist
            commandList = new List<Command>();
            GameCommandList = new List<GameCommand>();
            restartCommand = new restartCommand();

            //Setup the Joysticksettings
            joystickNr = 0;
            setupJoystick(joystickNr);
            //Setup the Keys of the Keyboard
            setupKeyboard();

            caX = new CommandAttributes(1);
            caY = new CommandAttributes(0, 1);
            caXN = new CommandAttributes(-1);
            caYN = new CommandAttributes(0, -1);
            timed = new CommandAttributes(Time.FromSeconds(.5f));

            mousePosition = Mouse.GetPosition();
            this.window = window;
        }


        private void setupJoystick(uint nr)
        {
            Joystick.Update();
            if (Joystick.IsConnected(nr))
            {
                Output.Instance.print("Joystick is connected and will be used.");
                joystickConnected = true;
                jumpJoy = 0;
                attackJoy = 11;
                altAttackJoy = 10;
                runAxis = Joystick.Axis.X;
                upAxis = Joystick.Axis.Y;
                rightAxisL = Joystick.Axis.Z;
                rightAxisU = Joystick.Axis.R;
                threshold = 1f;
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
            upKey = Keyboard.Key.W;
            downKey = Keyboard.Key.S;
            escKey = Keyboard.Key.Escape;
            rKey = Keyboard.Key.R;
            leftClick = Mouse.Button.Left;
            rightClick = Mouse.Button.Right;
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


        private bool mouseButtonDown(Mouse.Button mButton)
        {
             return Mouse.IsButtonPressed(mButton);
        }

        private bool MousePositionChanged()
        {
            SFML.System.Vector2i currentPos = Mouse.GetPosition();
            Vector2f worldPos = window.MapPixelToCoords(currentPos);

            if (currentPos.X != mousePosition.X || currentPos.Y != mousePosition.Y)
            {
                mousePosition = currentPos;
                return true;
            }
            return false;
        }



        internal List<GameCommand> GameCommandList
        {
            get
            {
                return gameCommandList;
            }

            set
            {
                gameCommandList = value;
            }
        }

        private void HandleKeyboardInput()
        {
            CommandAttributes _temp = new CommandAttributes(0, 0);
            if (KeyDown(leftKey))
            {
                _temp.Add(new Vector2f(-1, 0));
            }
            if (KeyDown(rightKey))
            {
                _temp.Add(new Vector2f(1, 0));
            }
            if (KeyDown(jumpKey))
            {
                AddCommandToList(_jump, caX);
            }
            if (KeyDown(upKey))
            {
                _temp.Add(new Vector2f(0, -1));
            }
            if (KeyDown(downKey))
            {
                _temp.Add(new Vector2f(0, 1));
            }
            if (_temp.IsValid())
            {
                _temp.Normalize();
                AddCommandToList(_go, _temp);
            }
            if (MousePositionChanged())
            {
                Vector2f worldPos = window.MapPixelToCoords(new Vector2i(mousePosition.X, mousePosition.Y));
                CommandAttributes _ca = new CommandAttributes(worldPos.X, worldPos.Y);
                AddCommandToList(turnCommand, _ca);
            }
            if (mouseButtonDown(leftClick))
            {
                AddCommandToList(attack, timed);
            }
            if (KeyDown(escKey))
            {
                System.Environment.Exit(0);
            }
            if (KeyDown(rKey))
            {
                AddCommandToGameCommandList(restartCommand);
            }
        }

        private void AddCommandToGameCommandList(GameCommand item)
        {
            GameCommandList.Add(item);
        }

        private void AddCommandToList(Command item, CommandAttributes ca)
        {
            if (!commandList.Contains(item))
            {
                item.Ca = ca;
                commandList.Add(item);
            }
            else
            {
                Output.Instance.print("Command was not added, because it already exists.");
            }
        }

        public void ResetInput()
        {
            commandList = new List<Command>();
            gameCommandList = new List<GameCommand>();
        }

        private void HandleJoystickInput()
        {
            
            if (joystickConnected)
            {
                //Always update the Joystick (otherwise it doesn't work.)
                Joystick.Update();
                //Check for the Buttons to be pressed
                if (Joystick.IsButtonPressed(joystickNr, jumpJoy))
                {
                    AddCommandToList(_jump, caX);
                }
                if (Joystick.IsButtonPressed(joystickNr, attackJoy))
                {
                    AddCommandToList(attack, timed);
                }
                if (Joystick.IsButtonPressed(joystickNr, altAttackJoy))
                {
                    //CommandAttributes _t = new CommandAttributes(1, false);

                    //AddCommandToList(attack, _t);
                }
                float _axisDirection = axisDirection(runAxis);
                float _axisDirectionY = axisDirection(upAxis);

                if (Math.Abs(_axisDirection) > threshold || Math.Abs(_axisDirectionY) > threshold)
                {
                    CommandAttributes _ca = new CommandAttributes(_axisDirection / 100, _axisDirectionY / 100);
                    AddCommandToList(_go, _ca);
                }

                float _axisRightL = axisDirection(rightAxisL);
                float _axisRightU = axisDirection(rightAxisU);
                if (Math.Abs(_axisRightU) > 50 || Math.Abs(_axisRightL) > 50)
                {
                    Vector2f worldPos = window.MapPixelToCoords(new Vector2i((int)(_axisRightL + window.DefaultView.Size.X / 2), (int)(_axisRightU + window.DefaultView.Size.Y / 2)));
                    CommandAttributes _ca = new CommandAttributes(worldPos.X, worldPos.Y);

                    AddCommandToList(turnCommand, _ca);
                }
                if (Joystick.IsButtonPressed(0, 14))
                {
                    AddCommandToGameCommandList(restartCommand);
                }
                JoystickDebug();
            }
        }

        private float axisDirection(Joystick.Axis axis)
        {
            return Joystick.GetAxisPosition(joystickNr, axis);

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
