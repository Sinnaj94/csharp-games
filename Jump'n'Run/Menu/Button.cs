using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace JumpAndRun
{
    class Button : SFML.Graphics.Drawable
    {
        String text;
        bool selected;
       
        uint characterSize;
        Text textObject;
        Text textSelectedObject;
        float x;
        float y;
        

        //TODO : Fenstergroesse automatisch erkennen
        public Button(String text, double x, double y)
        {
            Selected = false;
            characterSize = 100;
            this.text = text;

            textObject = new Text(text, ManageText.Instance.NormalFont,characterSize);
            this.x = (float)x;
            this.y = (float)y;
            textObject.Position = new Vector2f(0,0);

            textSelectedObject = new Text(textObject);
            textSelectedObject.Font = ManageText.Instance.SelectedFont;
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            Vector2f screenSize = target.GetView().Size;
            textSelectedObject.Position = new Vector2f(screenSize.X*x, screenSize.Y*y);
            textObject.Position = new Vector2f(screenSize.X * x, screenSize.Y * y);

            if (Selected)
            {
                textSelectedObject.Draw(target, states);
            }
            else
            {
                textObject.Draw(target, states);

            }
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public bool Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
            }
        }

        public void changeText(String newText)
        {
            text = newText;
            textObject.DisplayedString = newText;
            textSelectedObject.DisplayedString = newText;

        }
    }
}
