using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace SpaceShooter
{
    class ManageMenu : IRenderable
    {
        Clock c;
        //The background Images
        Texture backgroundImage;
        Sprite backgroundSprite;
        Texture backgroundImageG;
        Sprite backgroundSpriteG;
        Clock glow;
        RectangleShape background;
        bool active;
        int milliseconds;
        InputHandler input;
        Menu menu;
        List<MenuCommand> currentCommands;

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        internal Menu Menu
        {
            get
            {
                return menu;
            }

            set
            {
                menu = value;
            }
        }

        public ManageMenu()
        {
            Menu = new MainMenu(this);
            glow = new Clock();
            Active = true;
            milliseconds = 200;
            c = new Clock();
            input = new InputHandler();
            currentCommands = new List<MenuCommand>(10);

            background = new RectangleShape(new Vector2f(1920, 1080));
            background.FillColor = new Color(0, 0, 0, 255);
            backgroundImage = new Texture(@"Resources/mainscreen.png");
            backgroundImageG = new Texture(@"Resources/mainscreenglow.png");

            backgroundSprite = new Sprite(backgroundImage);
            backgroundSpriteG = new Sprite(backgroundImageG);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            backgroundSprite.Draw(target, states);
            backgroundSpriteG.Draw(target, states);
            Menu.Draw(target, states);
        }

        public void Update()
        {

            currentCommands = input.HandleInputMenu();
            foreach (MenuCommand com in currentCommands)
            {
                if (c.ElapsedTime.AsMilliseconds() >= milliseconds)
                {
                    com.Execute(Menu);
                    c.Restart();
                }
            }
            currentCommands.Clear();
            backgroundSpriteG.Color = new Color(255, 255, 255, (byte)(Math.Abs(Math.Sin(glow.ElapsedTime.AsSeconds()) * 255)));
        }
    }
}
