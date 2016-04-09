using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Pong.Properties;

namespace Pong
{
    class Player
    {
        static int id = 0;
        private Vector2f playerPosition;
        private Vector2f playerSize;
        private RectangleShape shape;
        private float yBoundMin;
        private float yBoundMax;
        private bool leftPlayer;
        private int score;
        private int playerid;
        private RenderWindow window;
        private Text scoreText;
     //   Font arial = new Font(@"Resources\coure.fon");
        Font arial = new Font(@"Resources\comic.ttf");


        public Player(RenderWindow window, Vector2f startPosition, bool playerSide)
        {
            playerSize = new Vector2f(10, 200);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            leftPlayer = playerSide;
            score = 0;
            playerid = id;
            id++;
            this.window = window;
            scoreText = new Text("" + score, arial, 200);
            scoreText.Color = new Color(255, 255, 255, 128);
            

            if (leftPlayer)
            {
                scoreText.Position = new Vector2f(window.Size.X*.2f, window.Size.Y*.2f);
            }
            else
            {
                scoreText.Position = new Vector2f(window.Size.X * .8f, window.Size.Y * .2f);
            }

            

        }




        public void update()
        {
            YBoundMin = playerPosition.Y;                       // upper bounds
            YBoundMax = playerPosition.Y + playerSize.Y;     // lower bounds
            shape.Position = playerPosition;
        }

        public void AddPoint()
        {
            
            score += 1;
            Console.WriteLine("New Score: " + score);
            ScoreText.DisplayedString = score.ToString();
        }


        public Vector2f PlayerPosition
        {
            get { return playerPosition; }
            set {
                if(value.Y < 0)
                {
                    playerPosition.Y = 0;
                }else if(value.Y > window.Size.Y - playerSize.Y)
                {
                    playerPosition.Y = window.Size.Y - playerSize.Y;
                }
                else
                {
                    playerPosition = value;
                }
                
            }
        }

        public RectangleShape Shape
        {
            get { return shape; }
            set { shape = value; }
        }

        public Vector2f PlayerSize
        {
            get
            {
                return playerSize;
            }

            set
            {
                playerSize = value;
            }
        }

        public float YBoundMin
        {
            get
            {
                return yBoundMin;
            }

            set
            {
                yBoundMin = value;
            }
        }

        public float YBoundMax
        {
            get
            {
                return yBoundMax;
            }

            set
            {
                yBoundMax = value;
            }
        }

        public bool LeftPlayer
        {
            get
            {
                return leftPlayer;
            }

            set
            {
                leftPlayer = value;
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
            }
        }

        public int Playerid
        {
            get
            {
                return playerid;
            }

            set
            {
                playerid = value;
            }
        }

        public Text ScoreText
        {
             get
            {
                return scoreText;
            }

            set
            {
                scoreText = value;
            }
        }
    }
}
