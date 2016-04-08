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
        Button exit;

        public Menu()
        {
            start =      new Button(new Vector2f(100, 100), true, "start game");
            settings =   new Button(new Vector2f(100, 300), false, "setting");
            exit =       new Button(new Vector2f(100, 500), false, "exit");
        }

        public int returnNewGamestate()
        {
            return 1;
        }

        

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                start.updateText();
                settings.updateText();
                exit.updateText();
                start.ButtonText.Draw(target, states);
                settings.ButtonText.Draw(target, states);
                exit.ButtonText.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("menu not implemented");
            }
        }
    }
}
