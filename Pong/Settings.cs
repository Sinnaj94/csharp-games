﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    class Settings : Drawable
    {
        private List<Button> ButtonList;
        private int menuState;
        private int screenMid = 683;
        private List<String> difficulty = new List<string> { "Easy ->", "<- Medium ->", "<- Hard"};
        private List<String> volume = new List<string> { "volume: 0%", "volume: 25%", "volume: 50%,", "volume: 75%", "volume: 100%" };

        public Settings(int setVolume, int setDifficulty)
        {
            menuState = 0;
            ButtonList = new List<Button>
            {
                new Button(new Vector2f(screenMid, 100), true, difficulty, setDifficulty),
                new Button(new Vector2f(screenMid, 250), false, volume, setVolume),
                new Button(new Vector2f(screenMid, 400), false, "back")
            };
        }

        public void updateMenuState()
        {
            if (ManageInput.Instance.SlowUp())
            {
                ButtonList[menuState].IsActive = false;
                menuState -= 1;
                if (menuState < 0)
                {
                    menuState = 2;
                }
                ButtonList[menuState].IsActive = true;
                ManageSound.Instance.click();
            }

            else if (ManageInput.Instance.SlowDown())
            {
                ButtonList[menuState].IsActive = false;
                menuState += 1;
                if (menuState > 2)
                {
                    menuState = 0;
                }
                ButtonList[menuState].IsActive = true;
                ManageSound.Instance.click();
            }
        }

        private void updateVolume()
        {
            ManageSound.Instance.setVolume(ButtonList[1].ButtonState * 25);
        }

        public int updateDifficulty()
        {
            return ButtonList[0].ButtonState * 2 + 9;
        }

        public int update()
        {
            updateMenuState();

            // Volume
            updateVolume();
            // Difficulty

            if (ManageInput.Instance.SlowRight() && menuState != 2)
                {
                    ButtonList[menuState].updateText(1);
                }

                if (ManageInput.Instance.SlowLeft() && menuState != 2)
                {
                    ButtonList[menuState].updateText(-1);
            }


            if (ManageInput.Instance.Return() && menuState == 2)
            {
                return 0;
            }
            else
            {
                return 3;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach (Button b in ButtonList)
            {
                b.updateText();
                b.Draw(target, states);
            }
        }
    }
}
