using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    class Menu : SFML.Graphics.Drawable
    {
        
        List<Button> ButtonList;
        int menuState;
        int screenMid = 683;

        public Menu()
        {
            menuState = 0;
            ButtonList = new List<Button>
            {
                new Button(new Vector2f(screenMid, 100), true, "1 Player"),
                new Button(new Vector2f(screenMid, 250), false, "2 Player"),
                new Button(new Vector2f(screenMid, 400), false, "setting"),
                new Button(new Vector2f(screenMid, 550), false, "exit")
            };
        }

        public int returnNewGamestate()
        {
            return 1;
        }

   
        public void updateMenuState()
        {
            if (ManageInput.Instance.SlowUp())
            {
                ManageSound.Instance.click();
                ButtonList[menuState].IsActive = false;
                menuState -= 1;
                if (menuState < 0)
                {
                    menuState = ButtonList.Count - 1;
                }
                ButtonList[menuState].IsActive = true;
            }

            else if (ManageInput.Instance.SlowDown())
            {
                ManageSound.Instance.click();
                ButtonList[menuState].IsActive = false;
                menuState += 1;
                if (menuState > ButtonList.Count - 1)
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

            if (ManageInput.Instance.Return())
            {
                switch (menuState)
                {
                    case 0:
                        return 1;
                    case 1:
                        return 6;
                    case 2:
                        return 3;
                    case 3:
                        return 4;
                }
            }
            return 0;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Button b in ButtonList)
            {
                b.updateText();
                b.Draw(target, states);
            }
        }
    }
}
