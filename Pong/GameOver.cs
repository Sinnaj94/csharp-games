using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Pong
{
    class GameOver : SFML.Graphics.Drawable
    {

        Text gameOver;
        Text menu;
        Text startAgain;
        Font arial = new Font(@"Resources\arial.ttf");

        public GameOver()
        {
            gameOver = new Text("Game Over", arial, 100);
            gameOver.Color = new Color(0, 255, 0, 255);
        }

        public int updateGameState()
        {
            if (ManageInput.Instance.Return())
            {
                return 0;
            }
            return 2;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                gameOver.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("gameOver not Implemented");
            }
        }
    }
}
