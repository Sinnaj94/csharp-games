using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Breakout
{
    class ScoreBoard : Drawable
    {
        private int score;
        private int lives;
        private Text scoreText;
        private Text livesText;
        Sprite status;

        public ScoreBoard()
        {
            score = 0;
            lives = 3;
            scoreText = new Text("0", ManageText.Instance.ArcadeClassic, 33);
            scoreText.Color = new Color(255,255,255,255);
            scoreText.Position = new Vector2f(410, 735);
            scoreText.DisplayedString = score.ToString();
            status = new Sprite(new Texture(@"Resources/status.png"), new IntRect(0, 0, 400, 33));
            status.Color = new Color(255, 255, 255, 200);
            status.Position = new Vector2f(100, 736);
            livesText = new Text("0", ManageText.Instance.ArcadeClassic, 100);
            livesText.Color = new Color(128, 0, 0, 255);
            livesText.Position = new Vector2f(610, 400);
            livesText.DisplayedString = lives.ToString();
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            scoreText.Draw(target, states);
            status.Draw(target, states);
        }

        public int Lives
        {
            get
            {
                return lives;
            }

            set
            {
                lives = value;
                IntRect tmpStatus = status.TextureRect;
                tmpStatus.Width = 200 + 38 * lives;
                status.TextureRect = tmpStatus;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                scoreText.DisplayedString = score.ToString();
            }
        }


    }
}
