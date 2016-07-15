using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class InputHandlerMenu
    {
        MenuCommand up;
        MenuCommand down;
        MenuCommand enter;
        Clock menuDelayClock;
        Time delayTime;
        uint joystickEnter;
        List<MenuCommand> commandList;
        public InputHandlerMenu()
        {
            menuDelayClock = new Clock();
            delayTime = Time.FromSeconds(.1f);
            commandList = new List<MenuCommand>();
            up = new MenuUpCommand();
            down = new MenuDownCommand();
            enter = new MenuEnterCommand();
        }

        public List<MenuCommand> HandleInput()
        {
            if(menuDelayClock.ElapsedTime > delayTime)
            {
                HandleKeyboardInput();
                HandleControllerInput();

            }
            return commandList;
        }

        private void HandleControllerInput()
        {
            Joystick.Update();
            if (Joystick.IsButtonPressed(0, 14))
            {
                AddCommandToList(enter);
            }
            if (axisDirection(Joystick.Axis.Y) > .2)
            {
                AddCommandToList(down);

            }
            if (axisDirection(Joystick.Axis.Y) < -.2)
            {
                AddCommandToList(up);

            }
        }

        public void Flush()
        {
            commandList = new List<MenuCommand>();

        }

        private float axisDirection(Joystick.Axis axis)
        {
            return Joystick.GetAxisPosition(0, axis);

        }

        private void HandleKeyboardInput()
        {

            if (KeyDown(Keyboard.Key.W))
            {
                AddCommandToList(up);
            }
            if (KeyDown(Keyboard.Key.S))
            {
                AddCommandToList(down);

            }
            if (KeyDown(Keyboard.Key.Return))
            {
                AddCommandToList(enter);
            }
        }

        private bool KeyDown(Keyboard.Key sKey)
        {
            return Keyboard.IsKeyPressed(sKey);
        }

        private void AddCommandToList(MenuCommand item)
        {
            if (!commandList.Contains(item))
            {
                menuDelayClock = new Clock();
                commandList.Add(item);
            }
            else
            {
                Output.Instance.print("Command was not added, because it already exists.");
            }
        }


    }
}
