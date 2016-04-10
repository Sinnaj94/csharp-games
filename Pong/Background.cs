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
        // Texture image = new Texture(@"Resources/background.jpg");
        //Sprite drawImage = new Sprite(image, new IntRect(0, 0, (int)(1366 * 1.25), (int)(768 * 1.25)));

        Sprite backGroundSprite;
        Sprite foreGroundSprite;



        public Background(Vector2f windowSize)
        {
            backGroundSprite = new Sprite(new Texture(@"Resources/background.jpg"));
            backGroundSprite.Position = new Vector2f( -0.5f * (backGroundSprite.Texture.Size.X - windowSize.X), -0.5f * (backGroundSprite.Texture.Size.Y - windowSize.Y));
            foreGroundSprite = new Sprite(new Texture(@"Resources/field.jpg"), new IntRect(0, 0, (int)(1366), (int)(768)));
           // foreGroundSprite.Position = new Vector2f(-1366f * 0.125f, -768f * 0.125f);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)backGroundSprite).Draw(target, states);
           // ((Drawable)foreGroundSprite).Draw(target, states);
        }
    }
}
