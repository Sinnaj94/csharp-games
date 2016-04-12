using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;

namespace Pong
{
    class Background : Drawable
    {
        Sprite backGroundSprite;

        public Background(Vector2f windowSize)
        {
            backGroundSprite = new Sprite(new Texture(@"Resources/background.jpg"));
            backGroundSprite.Position = new Vector2f( -0.5f * (backGroundSprite.Texture.Size.X - windowSize.X), -0.5f * (backGroundSprite.Texture.Size.Y - windowSize.Y));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)backGroundSprite).Draw(target, states);
        }
    }
}
