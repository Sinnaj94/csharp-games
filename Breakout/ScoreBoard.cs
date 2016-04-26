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


        public ScoreBoard()
        {
            score = 0;
            lives = 3;
            scoreText = new Text("0", ManageText.Instance.ArcadeClassic, 100);
            scoreText.Color = new Color(0,0,0,255);
            scoreText.Position = new Vector2f(610, 200);
            scoreText.DisplayedString = score.ToString();

            livesText = new Text("0", ManageText.Instance.ArcadeClassic, 100);
            livesText.Color = new Color(128, 0, 0, 255);
            livesText.Position = new Vector2f(610, 400);
            livesText.DisplayedString = lives.ToString();

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            scoreText.Draw(target, states);
            livesText.Draw(target, states);
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
                livesText.DisplayedString = lives.ToString();
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
