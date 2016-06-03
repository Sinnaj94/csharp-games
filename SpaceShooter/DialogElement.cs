using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace SpaceShooter
{
    class DialogElement : IRenderable
    {
        Clock c;
        Sprite speakerSprite;
        String text;
        RectangleShape background;

        Sprite backgroundSprite;
        Sprite backgroundBackground;
        Text shownText;
        RectangleShape edge;
        String savedText;
        int counter;
        int textSpeed;
        Clock timed;
        //TODO: Auslagern
        public DialogElement(String imgSource,String text,String planeSource, String bgSource)
        {
            this.text = text;

            speakerSprite = new Sprite(new Texture(imgSource));
            speakerSprite.Scale = new Vector2f(0.4f, .4f);
            speakerSprite.Position = new Vector2f(1920 * .1f, 1080 * .7f);

            backgroundSprite = new Sprite(new Texture(planeSource));
            backgroundBackground = new Sprite(new Texture(bgSource));
            background = new RectangleShape(new Vector2f(1920, 1080));
            background.FillColor = new Color(0, 0, 0, 100);
            savedText = text;
            shownText = new Text("", ManageText.Instance.TextFont);
            shownText.CharacterSize = 40;
            shownText.Position = speakerSprite.Position + new Vector2f(250,0);
            edge = new RectangleShape(new Vector2f(1920,200));
            c = new Clock();
            edge.Position = new Vector2f(0, speakerSprite.Position.Y);
            edge.FillColor = new Color(0, 0, 0, 100);
            textSpeed = 100;
            timed = new Clock();
        }

        

        public void Update()
        {
            backgroundSprite.Position = new Vector2f(backgroundSprite.Position.X, backgroundSprite.Position.X + (float)Math.Sin(timed.ElapsedTime.AsSeconds()) * 50);
            if (counter < savedText.Length)
            {
                if (c.ElapsedTime.AsMilliseconds() >= textSpeed)
                {
                    char a = savedText[counter];
                    counter++;
                    shownText.DisplayedString += a;
                    ManageSound.Instance.textelement();
                    c = new Clock();
               }

            }
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            background.Draw(target, states);
            backgroundBackground.Draw(target, states);

            backgroundSprite.Draw(target, states);
            edge.Draw(target, states);

            speakerSprite.Draw(target, states);
            shownText.Draw(target, states);
        }

        public void showAllText()
        {
            textSpeed = 10;
        }

        public bool textDone()
        {
            return (counter >= savedText.Length);
        }
    }
}
