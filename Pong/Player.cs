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

        public Player(RenderWindow window)
        {
              _playerOneSize = new Vector2f(window.Size.X * 0.02f, window.Size.Y * 0.25f);
            _PlayerOnePosition = new Vector2f(window.Size.X * 0.05f, window.Size.Y * 0.5f);
            _shape = new RectangleShape(_playerOneSize);
           // _shape.Origin = _PlayerOnePosition + 1 / 2 * _playerOneSize;      
        }

        Vector2f _PlayerOnePosition;
        Vector2f _playerOneSize;
        RectangleShape _shape;
        float yBoundMin;
        float yBoundMax;


        public void update()
        {

            YBoundMin = _PlayerOnePosition.Y;     // upper bounds
            YBoundMax = _PlayerOnePosition.Y + _playerOneSize.Y;     // lower bounds
            _shape.Position = _PlayerOnePosition;
        }
        



        public Vector2f PlayerOnePosition
        {
            get { return _PlayerOnePosition; }
            set { _PlayerOnePosition = value; }
        }


        public RectangleShape shape
        {
            get { return _shape; }
            set { _shape = value; }
        }

        public Vector2f PlayerOneSize
        {
            get
            {
                return _playerOneSize;
            }

            set
            {
                _playerOneSize = value;
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
    }
}
