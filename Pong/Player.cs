using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Player
    {

        public Player(RenderWindow window, Vector2f startPosition, bool playerSide)
        {
            playerSize = new Vector2f(10, 200);
            playerPosition = startPosition;
            shape = new RectangleShape(playerSize);
            shape.Position = playerPosition;
            leftPlayer = playerSide;
        }

        Vector2f playerPosition;
        Vector2f playerSize;
        RectangleShape shape;
        float yBoundMin;
        float yBoundMax;
        bool leftPlayer;


        public void update()
        {
            YBoundMin = playerPosition.Y;                       // upper bounds
            YBoundMax = playerPosition.Y + playerSize.Y;     // lower bounds
            shape.Position = playerPosition;
        }

        public Vector2f PlayerPosition
        {
            get { return playerPosition; }
            set { playerPosition = value; }
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
    }
}
