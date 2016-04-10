using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Score : Drawable
    {
        private int winningScore;
        private int scoreLeft;
        private int scoreRight;
        private Text scoreTextLeft;
        private Text scoreTextRight;


        public Score(int winningScore)
        {
            scoreLeft = 0;
            scoreRight = 0;
            this.winningScore = winningScore;
            scoreTextLeft = new Text("0", ManageText.Instance.ArcadeClassic, 200);
            scoreTextRight = new Text("0", ManageText.Instance.ArcadeClassic, 200);
            scoreTextLeft.Color = ManageText.Instance.Grey;
            scoreTextRight.Color = ManageText.Instance.Grey;
            scoreTextLeft.Position = new Vector2f(341, 100);
            scoreTextRight.Position = new Vector2f(1024, 100);

    }

        public bool GameOver(int playId)
        {
            
            if(playId == 1)
            {
                scoreLeft += 1;
                scoreTextLeft.DisplayedString = scoreLeft.ToString();
            }

            if (playId == 2)
            {
                scoreRight += 1;
                scoreTextRight.DisplayedString = scoreRight.ToString();
            }
            

            if(scoreLeft >= winningScore || scoreRight >= winningScore)
            {
                return true;
            } else
            {
                return false;
            }
            

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            try
            {
                scoreTextLeft.Draw(target, states);
                scoreTextRight.Draw(target, states);
            }
            catch (NotImplementedException)
            {
                Console.Out.Write("Score not implemented");
            }
        }
    }
}
