using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    class GameOver : SFML.Graphics.Drawable
    {

        Text gameOver;
        Text continueToMenu;
        int screenMid = 300;

        public GameOver()
        {
            gameOver = new Text("Game Over", ManageText.Instance.CrackmanFront, 100);
            gameOver.Color = ManageText.Instance.getFlashingYellow();
            gameOver.Position = new Vector2f(screenMid - gameOver.GetLocalBounds().Width / 2, 100);
            continueToMenu = new Text("press return to continue", ManageText.Instance.ArcadeClassic, 50);
            continueToMenu.Color = ManageText.Instance.Grey;
            continueToMenu.Position = new Vector2f(screenMid - continueToMenu.GetLocalBounds().Width / 2, 300);
        }

        public int updateGameState()
        {
            gameOver.Color = ManageText.Instance.getFlashingYellow();

            if (ManageInput.Instance.Return())
            {
                return 0;
            }
            return 2;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            gameOver.Draw(target, states);
            continueToMenu.Draw(target, states);
        }
    }
}
