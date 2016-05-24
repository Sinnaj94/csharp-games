using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace SpaceShooter.GameObjects
{
    class Button : GameObject, IRenderable
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
            Console.Out.WriteLine("Button " + text + " at x=" + x + " and y=" + y + " created.");
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

        public override void Update()
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
    }
}
