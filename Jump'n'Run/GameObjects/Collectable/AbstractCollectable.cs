using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class AbstractCollectable : GameObject, Drawable
    {

        Sprite sprite;
        bool wasColected;

        public void Draw(RenderTarget target, RenderStates states)
        {
            Sprite.Draw(target, states);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public bool WasColected
        {
            get
            {
                return wasColected;
            }

            set
            {
                wasColected = value;
            }
        }
    }
}
