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
    class Button
    {

        Vector2f        buttonPosition;
        bool            isActive;
        Font arial = new Font(Resources.arial);
        Text buttonText;



        public Button(Vector2f position, bool active, String text)
        {
            buttonPosition = position;
            IsActive = active;
            buttonText = new Text(text, arial, 100);
            buttonText.Position = position;
        }

        void updateText()
        {
            if (IsActive)
            {
                buttonText.Color = new Color(0, 255, 0, 255);
            } else
            {
                buttonText.Color = new Color(255, 255, 255, 128);
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
