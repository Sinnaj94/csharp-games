using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong
{
    class Settings : Drawable
    {

        Text settings;
        Font arial = new Font(@"Resources\arial.ttf");

        public Settings()
        {
            settings = new Text("Todo, return to go back to menu", arial, 50);
            settings.Color = new Color(255, 255, 255, 128);
        }

        public int update()
        {
            if (BetterInputHandler.Instance.Return())
            {
                return 0;
            }
            else
            {
                return 3;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                settings.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("gameOver not Implemented");
            }
        }
    }
}
