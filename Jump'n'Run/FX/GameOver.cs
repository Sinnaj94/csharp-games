using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace JumpAndRun
{
    class GameOver : SFML.Graphics.Drawable
    {
        Text gameoverText;
        public GameOver()
        {
            gameoverText = new Text("Game Over", ManageText.Instance.TextFont);
            gameoverText.CharacterSize *= 5;
            gameoverText.Origin += new SFML.System.Vector2f(gameoverText.GetGlobalBounds().Width / 2, gameoverText.GetGlobalBounds().Height);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            gameoverText.Position = target.GetView().Center;
            gameoverText.Draw(target, states);
        }
    }
}
