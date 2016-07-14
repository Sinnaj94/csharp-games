using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public class Lightcone : Drawable
    {
        RenderTexture darkTex;
        Sprite darkSprite;
        Texture lightTex;
        SFML.Graphics.RenderWindow window;
        Sprite lightSprite;

        public Lightcone(RenderWindow window)
        {
            this.window = window;
            SetupDarkSprite();
            SetupLightSprite();
            darkTex.Draw(lightSprite, new RenderStates(BlendMode.Multiply));
            darkTex.Display();
        }

        public void SetupDarkSprite()
        {
            darkTex = new RenderTexture(1920, 1080);
            darkSprite = new Sprite(darkTex.Texture);
            darkSprite.Origin = new SFML.System.Vector2f(darkSprite.Texture.Size.X / 2, darkSprite.Texture.Size.Y / 2);
            darkTex.Clear(new Color(0, 0, 0, 200));
            darkTex.Display();
        }
        public void SetupLightSprite()
        {
            lightTex = new Texture(@"Resources/sprites/sprite.png");
            lightSprite = new Sprite(lightTex);
            lightSprite.Scale = new SFML.System.Vector2f(2, 2);
            lightSprite.Origin = new SFML.System.Vector2f(lightSprite.Texture.Size.X / 2, lightSprite.Texture.Size.Y / 2);
            lightSprite.Position = window.GetView().Center;
        }

        public void updateLightCone(RenderWindow window)
        {
            darkSprite.Position = window.GetView().Center;

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            updateLightCone(window);
            darkSprite.Draw(target, states);
        }
    }
}
