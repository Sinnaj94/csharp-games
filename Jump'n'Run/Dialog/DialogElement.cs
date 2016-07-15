using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace JumpAndRun
{
    class DialogElement : SFML.Graphics.Drawable
    {
        Clock c;
        Sprite speakerSprite;
        String text;
        RectangleShape background;

        Sprite backgroundBackground;
        Text shownText;
        RectangleShape edge;
        String savedText;
        int counter;
        int textSpeed;
        Clock timed;
        Vector2f floatingPosition;
        Sprite buttonASprite;
        //TODO: Auslagern
        public DialogElement(String imgSource,String text,String planeSource, String bgSource)
        {

            this.text = text;

            speakerSprite = new Sprite(new Texture(imgSource));
            speakerSprite.Scale = new Vector2f(1f, 1f);
            speakerSprite.Position = new Vector2f(1920 * .1f, 1080 * .7f);
            floatingPosition = speakerSprite.Position;
            backgroundBackground = new Sprite(new Texture(bgSource));
            backgroundBackground.Color = new Color(255, 255, 255, 50);
            background = new RectangleShape(new Vector2f(1920, 1080));
            background.FillColor = new Color(0, 0, 0, 100);
            savedText = text;
            shownText = new Text("", ManageText.Instance.TextFont);
            shownText.CharacterSize = 50;
            shownText.Position = speakerSprite.Position + new Vector2f(400,0);
            edge = new RectangleShape(new Vector2f(1920,200));
            c = new Clock();
            edge.Position = new Vector2f(0, speakerSprite.Position.Y);
            edge.FillColor = new Color(0, 0, 0, 100);
            textSpeed = 50;
            timed = new Clock();
            buttonASprite = new Sprite(new Texture(@"Resources/buttons/a.png"));
            buttonASprite.Origin = new Vector2f(buttonASprite.Texture.Size.X/2, buttonASprite.Texture.Size.Y/2);
            buttonASprite.Position = new Vector2f(1920 * .5f, 1080 * .85f);
            Console.WriteLine(buttonASprite.Origin + "");
        }

        

        public void Update()
        {
            speakerSprite.Position = new Vector2f(floatingPosition.X, floatingPosition.Y + (float)Math.Sin(timed.ElapsedTime.AsSeconds()) * 20);
            if (counter < savedText.Length)
            {
                if (c.ElapsedTime.AsMilliseconds() >= textSpeed)
                {
                    char a = savedText[counter];
                    counter++;
                    shownText.DisplayedString += a;
                    //ManageSound.Instance.textelement();
                    c = new Clock();
               }
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {


            //background.Draw(target, states);
            backgroundBackground.Draw(target, states);


            //edge.Draw(target, states);

            speakerSprite.Draw(target, states);
            shownText.Draw(target, states);
            if (textDone())
            {
                buttonASprite.Scale = new Vector2f((float)Math.Abs(Math.Sin(timed.ElapsedTime.AsSeconds()) * 1) + .2f, (float)Math.Abs(Math.Sin(timed.ElapsedTime.AsSeconds()) * 1) + .2f);
                buttonASprite.Draw(target, states);
            }
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
