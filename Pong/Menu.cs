using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Menu : SFML.Graphics.Drawable
    {
        
        List<Button> ButtonList;
        int menuState;

        public Menu()
        {
            menuState = 0;
            ButtonList = new List<Button>
            {
                new Button(new Vector2f(100, 100), true, "start game"),
                new Button(new Vector2f(100, 300), false, "setting"),
                new Button(new Vector2f(100, 500), false, "exit")
            };
        }

        public int returnNewGamestate()
        {
            return 1;
        }

   
        public void updateMenuState()
        {
            if (BetterInputHandler.Instance.SlowUp())
            {
                ButtonList[menuState].IsActive = false;
                menuState -= 1;
                if (menuState < 0)
                {
                    menuState = 2;
                }
                ButtonList[menuState].IsActive = true;
            }

            else if (BetterInputHandler.Instance.SlowDown())
            {
                ButtonList[menuState].IsActive = false;
                menuState += 1;
                if (menuState > 2)
                {
                    menuState = 0;
                }
                ButtonList[menuState].IsActive = true;
            }
        }

        // gamestates 0: menu, 1: game, 2: gameover, 3: settings, 4: exit
        public int updateGameState()
        {
            updateMenuState();

            if (BetterInputHandler.Instance.Return())
            {
                switch (menuState)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 3;
                    case 2:
                        return 4;
                }
            }
            return 0;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                foreach(Button b in ButtonList)
                {
                    b.updateText();
                    b.Draw(target, states);
                }
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("menu not implemented");
            }
        }
    }
}
