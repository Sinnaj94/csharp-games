using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Menu : SFML.Graphics.Drawable
    {

    //    const int menu = 0, start = 1, gameOver = 2, settings = 3;

        Button start;
        Button settings;

        public Menu()
        {
            
            start =      new Button(new Vector2f(100, 100), true, "start game");
            settings =   new Button(new Vector2f(100, 300), false, "setting");
            
        }

        void updateMenu(bool direction)
        {
            // ture = up
            // false = down
            if (direction)
            {
                start.IsActive = true;
            } else
            {
                start.IsActive = false;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                //   start.Draw(target, states);
                start.updateText();
                settings.updateText();
                start.ButtonText.Draw(target, states);
                settings.ButtonText.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("asd");
            }
            //throw new NotImplementedException();
        }
    }
}
