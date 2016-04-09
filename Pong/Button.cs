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
       // Color backColor = new Color(255, 255, 255, 32);


        public Button(Vector2f position, bool active, String text)
        {
            buttonPosition = position;
            IsActive = active;
            buttonText = new Text(text, ManageText.Instance.CrackmanFront, 100);
            buttonText.Color = ManageText.Instance.White;
            buttonText.Position = position;

            buttonTextBack = new Text(text, ManageText.Instance.CrackmanBack, 100);
            buttonTextBack.Color = ManageText.Instance.Grey;
            buttonTextBack.Position = position - new Vector2f(5,5);
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
                Console.Out.Write("asd");
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
