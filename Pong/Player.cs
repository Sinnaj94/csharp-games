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

        public Player()
        {
            _PlayerOnePosition = new Vector2f(10, 100);
            _playerOneSize = new Vector2f(10, 100);
            _shape = new RectangleShape(_playerOneSize);
        }

        Vector2f _PlayerOnePosition;
        Vector2f _playerOneSize;
        RectangleShape _shape;

        public void update()
        {
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
    }
}
