using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;



// implementing text button without mouse interaction for now

namespace Breakout
{
    class Button : Drawable
    {

        private Vector2f buttonPosition;
        private  bool isActive;
        private  Text buttonText;
        private Text buttonTextBack;
        private int buttonState;
        private  List<String> optionButtonText;

        // Regular Button
        public Button(Vector2f position, bool active, String text)
        {
            buttonPosition = position;
            IsActive = active;
            buttonText = new Text(text, ManageText.Instance.CrackmanFront, 100);
            buttonText.Color = ManageText.Instance.White;
            buttonText.Position = new Vector2f(position.X - buttonText.GetLocalBounds().Width / 2, position.Y);
            buttonTextBack = new Text(text, ManageText.Instance.CrackmanBack, 100);
            buttonTextBack.Color = ManageText.Instance.Grey;
            buttonTextBack.Position = buttonText.Position - new Vector2f(5,5);
        }

        // Option Button
        public Button(Vector2f position, bool active, List<String> text, int setId)
        {
            optionButtonText = text;
            buttonState = setId;
            buttonPosition = position;
            IsActive = active;
            buttonText = new Text(optionButtonText[buttonState], ManageText.Instance.CrackmanFront, 100);
            buttonText.Color = ManageText.Instance.White;   
            buttonTextBack = new Text(optionButtonText[buttonState], ManageText.Instance.CrackmanBack, 100);
            buttonTextBack.Color = ManageText.Instance.Grey;
            buttonText.Position = new Vector2f(buttonPosition.X - buttonText.GetLocalBounds().Width / 2, position.Y);
            buttonTextBack.Position = buttonText.Position - new Vector2f(5, 5);
        }

        public void updateOptionButton(int buttonPressed)
        {
            if (buttonPressed == 1)
            {
                if (buttonState < optionButtonText.Count - 1)
                {
                    buttonState += 1;
                    ManageSound.Instance.click();
                }
            }

            if (buttonPressed == -1)
            {
                if (buttonState > 0)
                {
                    buttonState -= 1;
                    ManageSound.Instance.click();
                }

            }

            buttonText.DisplayedString = optionButtonText[buttonState];
            buttonTextBack.DisplayedString = optionButtonText[buttonState];
            buttonText.Position = new Vector2f(buttonPosition.X - buttonText.GetLocalBounds().Width / 2, buttonPosition.Y);
            buttonTextBack.Position = buttonText.Position - new Vector2f(5, 5);

        }

        public void updateText(int buttonPressed)
        {

            // only triggers if Button is an option button
            // TODO: find nicer way to fix 

            if (optionButtonText != null)
            {
                updateOptionButton(buttonPressed);
            }


            if (IsActive)
            {
                buttonText.Color = ManageText.Instance.Yellow;
            }
            else
            {
                buttonText.Color = ManageText.Instance.White;
            }
        }

        public void updateText()
        {
            if (IsActive)
            {
                buttonText.Color = ManageText.Instance.Yellow;
            } else
            {
                buttonText.Color = ManageText.Instance.White;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            buttonTextBack.Draw(target, states);
            buttonText.Draw(target, states);
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
            }
        }

        public Text ButtonText
        {
            get
            {
                return buttonText;
            }

            set
            {
                buttonText = value;
            }
        }

        public int ButtonState
        {
            get
            {
                return buttonState;
            }

            set
            {
                buttonState = value;
            }
        }
    }
}
