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
        double x;
        double y;
        uint characterSize;
        Text textObject;
        //TODO : Fenstergroesse automatisch erkennen
        public Button(String text, double x, double y)
        {
            characterSize = 100;
            this.text = text;
            this.x = x*1920;
            this.y = y*1080;
            textObject = new Text(text, ManageText.Instance.NormalFont,characterSize);
            textObject.Position = new Vector2f((float)x, (float)y);
            Console.Out.WriteLine("Button " + text + " at x=" + x + " and y=" + y + " created.");
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            textObject.Draw(target, states);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
