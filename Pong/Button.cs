using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
using Pong.Properties;


// implementing text button without mouse interaction for now

namespace Pong
{
    class Button : Drawable
    {

        Vector2f        buttonPosition;
        bool            isActive;
        Text buttonText;
        Text buttonTextBack;

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
            try{
                buttonTextBack.Draw(target, states);
                buttonText.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("Button not implemented");
            }
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
    }
}
