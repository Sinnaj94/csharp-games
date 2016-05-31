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
        Texture speaker;
        Sprite speakerSprite;
        String text;
        RectangleShape background;
        Text shownText;
        RectangleShape edge;
        String savedText;
        int counter;
        int textSpeed;
        //TODO: Auslagern
        public DialogElement(String imgSource,String text)
        {
            this.text = text;
            speaker = new Texture(imgSource);
            speakerSprite = new Sprite(speaker);
            speakerSprite.Scale = new Vector2f(0.4f, .4f);
            speakerSprite.Position = new Vector2f(1920 * .1f, 1080 * .7f);
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
        }

        public void Update()
        {
            if (counter < savedText.Length)
            {
                if (c.ElapsedTime.AsMilliseconds() >= textSpeed)
                {
                    char a = savedText[counter];
                    counter++;
                    shownText.DisplayedString += a;
                    c = new Clock();
               }

            }
            
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            Update();
            background.Draw(target, states);
            edge.Draw(target, states);
            speakerSprite.Draw(target, states);
            shownText.Draw(target, states);
        }
    }
}
