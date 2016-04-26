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
        Sprite healthbar;


        public ScoreBoard()
        {
            score = 0;
            lives = 2;
            scoreText = new Text("0", ManageText.Instance.ArcadeClassic, 100);
            scoreText.Color = new Color(0,0,0,255);
            scoreText.Position = new Vector2f(610, 200);
            scoreText.DisplayedString = score.ToString();

            healthbar = new Sprite(new Texture(@"Resources/health.png"), new IntRect(0,400,30,200));
            healthbar.Position = new Vector2f(610, 400);
            livesText = new Text("0", ManageText.Instance.ArcadeClassic, 100);
            livesText.Color = new Color(128, 0, 0, 255);
            livesText.Position = new Vector2f(610, 400);
            livesText.DisplayedString = lives.ToString();
        }


        public void Draw(RenderTarget target, RenderStates states)
        {
            scoreText.Draw(target, states);
            //livesText.Draw(target, states);
            healthbar.Draw(target, states);
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
                IntRect tmp = healthbar.TextureRect;
                tmp.Top = lives * 200;
                healthbar.TextureRect = tmp;
                // TODO call gameover
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
