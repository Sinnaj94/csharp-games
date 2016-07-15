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

        List<MenuCommand> commandList;
        public InputHandlerMenu()
        {
            commandList = new List<MenuCommand>();
            up = new MenuUpCommand();
            down = new MenuDownCommand();
            enter = new MenuEnterCommand();
        }

        public List<MenuCommand> HandleInput()
        {
            HandleKeyboardInput();
            return commandList;
        }

        public void Flush()
        {
            commandList = new List<MenuCommand>();

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
            if (KeyDown(Keyboard.Key.C))
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
                commandList.Add(item);
            }
            else
            {
                Output.Instance.print("Command was not added, because it already exists.");
            }
        }
    }
}
