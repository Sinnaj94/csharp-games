﻿using SFML.Window;
using SpaceShooter.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using FarseerPhysics;
namespace SpaceShooter
{
    class InputHandler
    {
        Vector2i mousePosition;

        //Commands for the game
        //Shooting command
        uint nrShoot;
        uint nrSelect;
        uint nrBigShoot;
        Keyboard.Key _buttonSelect;
        Keyboard.Key _buttonShoot;
        //Left command
        Keyboard.Key _buttonLeft;
        //Right command
        Keyboard.Key _buttonRight;
        //Up Command
        Keyboard.Key _buttonUp;
        //Down Command
        Keyboard.Key _buttonDown;
        Keyboard.Key _buttonBigShoot;
        Keyboard.Key _P;
        Keyboard.Key _ESC;
        Keyboard.Key _R;
        Mouse.Button _mouseShoot;
        Mouse.Button _mouseBigShoot;
        Keyboard.Key _ArrowUp;

        Command buttonShoot;
        Command[] moveCommandArray;
        //Other commands
        Command turnCommand;
        Command turnCommandJoystick;

        //Commands for the Menu
        //Up Command
        MenuCommand menuUpCommand;
        MenuCommand menuDownCommand;
        MenuCommand menuSelectCommand;

        // Commands for pause screen
        PauseCommand PausePressedCommand;
        PauseCommand ResumePressedCommand;
        GameCommand ESCPressedCommand;

        GameCommand UpgradePressedCommand;

        DialogCommand dialogEnterCommand;

        Ship p;
        //Finally, a list to store the commands
        List<Command> requestedCommands;
        List<MenuCommand> requestedMenuCommands;
        List<DialogCommand> requestedDialogCommands;
        List<PauseCommand> requestedPauseCommands;
        List<GameCommand> requestedGameCommands;
        bool joystickConnected;
        public InputHandler()
        {
            _buttonShoot = Keyboard.Key.Space;
            buttonShoot = new ShootCommand();
            _buttonLeft = Keyboard.Key.A;
            _buttonRight = Keyboard.Key.D;
            _buttonUp = Keyboard.Key.W;
            _buttonDown = Keyboard.Key.S;
            _buttonBigShoot = Keyboard.Key.LAlt;
            _buttonSelect = Keyboard.Key.Return;
            _mouseShoot = Mouse.Button.Left;
            _mouseBigShoot = Mouse.Button.Right;
            _P = Keyboard.Key.P;
            _R = Keyboard.Key.R;
            _ArrowUp = Keyboard.Key.Up;
            _ESC = Keyboard.Key.Escape;

            nrShoot = 11;
            nrSelect = 14;
            nrBigShoot = 10;

            turnCommand = new TurnCommand();
            moveCommandArray = new Command[4];
            for(int i = 0; i < moveCommandArray.Length; i++)
            {
                moveCommandArray[i] = new MoveCommand();
            }

            menuUpCommand = new MenuUpCommand();
            menuDownCommand = new MenuDownCommand();
            menuSelectCommand = new MenuSelectCommand();
            turnCommandJoystick = new TurnCommandJoystick();

            PausePressedCommand = new PausePressedCommand();
            ResumePressedCommand = new ResumePressedCommand();

            UpgradePressedCommand = new UpgradePressedCommand();
            ESCPressedCommand = new ESCPressedCommand();
            dialogEnterCommand = new DialogEnterCommand();

            RequestedCommands = new List<Command>();
            RequestedMenuCommands = new List<MenuCommand>();
            RequestedDialogCommands = new List<DialogCommand>();
            requestedPauseCommands = new List<PauseCommand>();
            RequestedGameCommands = new List<GameCommand>();
            ConfigureJoystick(0);

            mousePosition = Mouse.GetPosition();
        }

        

        private void ConfigureJoystick(uint nr)
        {
            Joystick.Update();
            if (Joystick.IsConnected(nr))
            {
                Console.Out.Write("Joystick " + nr + " is inserted and being used.");
                JoystickConnected = true;
            }
            else
            {
                Console.Out.Write("Joystick is not inserted. Using Keyboard instead.");
                JoystickConnected = true;
            }

        }


        private bool JoystickMovedInDirection(Joystick.Axis axis, int factor)
        {
            Joystick.Update();
            if (factor < 1)
            {
                if (Joystick.GetAxisPosition(0, axis) < 0.01 * factor)
                {
                    return true;
                }
            }
            else
            {
                if (Joystick.GetAxisPosition(0, axis) > 0.01 * factor)
                {
                    return true;
                }
            }

            return false;
        }

        private bool JoystickMoved(Joystick.Axis axis1)
        {
            Joystick.Update();
            if (Math.Abs(Joystick.GetAxisPosition(0, axis1)) > 0.01 )
            {
                return true;
            }
            return false;
        }

        private void JoystickTesting()
        {
            Joystick.Update();
            foreach(Joystick.Axis a in Enum.GetValues(typeof(Joystick.Axis))){
                //Console.Out.WriteLine(a.ToString() + " " + Joystick.GetAxisPosition(0, a));
            }

            
        }

        private bool MousePositionChanged()
        {
            Vector2i currentPos = Mouse.GetPosition();
            if(currentPos.X!=mousePosition.X ||currentPos.Y != mousePosition.Y)
            {
                mousePosition = currentPos;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Checks if any of the implemented keys are pressed.
        /// </summary>
        /// <returns>Returns a CommandList of the Requested Commands</returns>
        public List<Command> HandleInputKeyboard()
        {

            if (Keyboard.IsKeyPressed(_buttonShoot) || Mouse.IsButtonPressed(_mouseShoot))
            {
                buttonShoot.Strength = new Vector2f(1, 1);
                AddCommandToList(buttonShoot);
            }
            if (Keyboard.IsKeyPressed(_buttonBigShoot) || Mouse.IsButtonPressed(_mouseBigShoot))
            {
                buttonShoot.Strength = new Vector2f(2, 2);
                AddCommandToList(buttonShoot);
            }
            if (Keyboard.IsKeyPressed(_buttonLeft))
            {
                moveCommandArray[0].Strength = new Vector2f(-1,0);
                AddCommandToList(moveCommandArray[0]);

            }
            if (Keyboard.IsKeyPressed(_buttonRight))
            {
                moveCommandArray[1].Strength = new Vector2f(1, moveCommandArray[1].Strength.Y);
                AddCommandToList(moveCommandArray[1]);
            }
            if (Keyboard.IsKeyPressed(_buttonUp))
            {
                moveCommandArray[2].Strength = new Vector2f(moveCommandArray[2].Strength.X, -1);
                AddCommandToList(moveCommandArray[2]);
            }
            if (Keyboard.IsKeyPressed(_buttonDown))
            {
                moveCommandArray[3].Strength = new Vector2f(moveCommandArray[3].Strength.X, 1);
                AddCommandToList(moveCommandArray[3]);
            }
            if (MousePositionChanged())
            {

                turnCommand.Strength = new Vector2f(mousePosition.X,mousePosition.Y);
                //JoystickTesting();
                AddCommandToList(turnCommand);
            }
            return RequestedCommands;
        }

        public List<MenuCommand> HandleInputMenu()
        {
            
            if (Keyboard.IsKeyPressed(_buttonUp) || JoystickMovedInDirection(Joystick.Axis.Y, -1))
            {
                AddCommandToList(menuUpCommand);
            }
            if (Keyboard.IsKeyPressed(_buttonDown) || JoystickMovedInDirection(Joystick.Axis.Y, 1))
            {
                AddCommandToList(menuDownCommand);
            }
            if(Joystick.IsButtonPressed(0, nrSelect) || Keyboard.IsKeyPressed(_buttonSelect))
            {
                AddCommandToList(menuSelectCommand);
            }
            return RequestedMenuCommands;
        }

        public List<DialogCommand> HandleInputDialog()
        {
            if (Joystick.IsButtonPressed(0, nrSelect) || Keyboard.IsKeyPressed(_buttonSelect))
            {
                AddCommandToList(dialogEnterCommand);
            }

            return RequestedDialogCommands;
        }

        public List<PauseCommand> HandleInputPause()
        {
            if (Keyboard.IsKeyPressed(_P))
            {
                AddCommandToList(PausePressedCommand);
            }

            if (Keyboard.IsKeyPressed(_R))
            {
                AddCommandToList(ResumePressedCommand);
            }

            return RequestedPauseCommands;
        }

        public List<GameCommand> HandleInputGame()
        {
            if (Keyboard.IsKeyPressed(_ArrowUp))
            {
                AddCommandToList(UpgradePressedCommand);
            }

            if (Keyboard.IsKeyPressed(_ESC))
            {
                AddCommandToList(ESCPressedCommand);
            }

            return RequestedGameCommands;
        }

        float getStrength(Joystick.Axis axis)
        {
            Joystick.Update();
            return Joystick.GetAxisPosition(0, axis)/100;
        }

        //TODO: Im Moment noch sehr unsauber!!
        public List<Command> HandleInputJoystick()
        {

            if (Joystick.IsButtonPressed(0, nrShoot))
            {
                buttonShoot.Strength = new Vector2f(1, 1);
                AddCommandToList(buttonShoot);
            }
            if (Joystick.IsButtonPressed(0, nrBigShoot))
            {
                buttonShoot.Strength = new Vector2f(2, 2);
                AddCommandToList(buttonShoot);
            }
                if (JoystickMoved(Joystick.Axis.X))
            {
                moveCommandArray[0].Strength = new Vector2f(getStrength(Joystick.Axis.X), 0);
                AddCommandToList(moveCommandArray[0]);
            }

            if (JoystickMoved(Joystick.Axis.Y))
            {
                moveCommandArray[1].Strength = new Vector2f(0, getStrength(Joystick.Axis.Y));
                AddCommandToList(moveCommandArray[1]);
            }
            if (JoystickMoved(Joystick.Axis.R) || JoystickMoved(Joystick.Axis.Z))
            {
                turnCommandJoystick.Strength = new Vector2f(getStrength(Joystick.Axis.Z)*300, getStrength(Joystick.Axis.R)*300);
                //JoystickTesting();
                AddCommandToList(turnCommandJoystick);
            }

            return RequestedCommands;
        }

        public List<Command> HandlePlayerInput()
        {
            HandleInputKeyboard();
            HandleInputJoystick();
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

        private void AddCommandToList(PauseCommand c)
        {
            if (!RequestedPauseCommands.Contains(c))
            {
                RequestedPauseCommands.Add(c);
            }
        }

        /// <summary>
        /// Adds a specific Command to the List (MenuCommand)
        /// </summary>
        /// <param name="c">The Command that should be added to the MenuCommand List</param>
        private void AddCommandToList(MenuCommand c)
        {
            if (!RequestedMenuCommands.Contains(c))
            {
                RequestedMenuCommands.Add(c);
            }
        }

        private void AddCommandToList(DialogCommand c)
        {
            if (!RequestedDialogCommands.Contains(c))
            {
                RequestedDialogCommands.Add(c);
            }
        }

        private void AddCommandToList(GameCommand c)
        {
            if (!RequestedGameCommands.Contains(c))
            {
                RequestedGameCommands.Add(c);
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

        public bool JoystickConnected
        {
            get
            {
                return joystickConnected;
            }

            set
            {
                joystickConnected = value;
            }
        }

        internal List<MenuCommand> RequestedMenuCommands
        {
            get
            {
                return requestedMenuCommands;
            }

            set
            {
                requestedMenuCommands = value;
            }
        }

        internal Ship P
        {
            get
            {
                return p;
            }

            set
            {
                p = value;
            }
        }

        internal List<DialogCommand> RequestedDialogCommands
        {
            get
            {
                return requestedDialogCommands;
            }

            set
            {
                requestedDialogCommands = value;
            }
        }

        internal List<PauseCommand> RequestedPauseCommands
        {
            get
            {
                return requestedPauseCommands;
            }

            set
            {
                requestedPauseCommands = value;
            }
        }

        internal List<GameCommand> RequestedGameCommands
        {
            get
            {
                return requestedGameCommands;
            }

            set
            {
                requestedGameCommands = value;
            }
        }
    }
}
