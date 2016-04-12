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
        Sprite backGroundGlow;
        Clock c;
        public Background(Vector2f windowSize)
        {
            backGroundSprite = new Sprite(new Texture(@"Resources/background.jpg"));
            backGroundGlow = new Sprite(new Texture(@"Resources/backgroundred.jpg"));
            backGroundSprite.Position = new Vector2f( -0.5f * (backGroundSprite.Texture.Size.X - windowSize.X), -0.5f * (backGroundSprite.Texture.Size.Y - windowSize.Y));
            backGroundGlow.Position = backGroundSprite.Position;
            c = new Clock();
        }

        public void changeOpacity()
        {
            Byte opacity = (Byte)Math.Sin(c.ElapsedTime.AsSeconds());
            backGroundGlow.Color = new Color(255, 255, 255, opacity);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)backGroundSprite).Draw(target, states);
            changeOpacity();
            ((Drawable)backGroundGlow).Draw(target, states);
        }
    }
}
