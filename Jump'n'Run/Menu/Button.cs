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

        

        //TODO : Fenstergroesse automatisch erkennen
        public Button(String text, double x, double y)
        {
            Selected = false;
            characterSize = 100;
            this.text = text;
            x = x*1920;
            y = y*1080;
            textObject = new Text(text, ManageText.Instance.NormalFont,characterSize);
            
            textObject.Position = new Vector2f((float)x, (float)y);

            textSelectedObject = new Text(textObject);
            textSelectedObject.Font = ManageText.Instance.SelectedFont;
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            
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
