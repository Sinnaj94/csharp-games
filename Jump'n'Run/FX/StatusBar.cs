using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class StatusBar : Drawable
    {
        Texture bulletTexture;
        Sprite bulletSprite;
        Texture EnemyTexture;
        Sprite EnemySprite;
        Text BulletCounter;
        Text EnemyCounter;
      
        public StatusBar()
        {
            bulletTexture = new Texture(@"Resources/sprites/bullet.png");
            bulletSprite = new Sprite(bulletTexture);
            EnemyTexture = new Texture(@"Resources/sprites/EnemyStatusBar.png");
            EnemySprite = new Sprite(EnemyTexture);
            BulletCounter = new Text("10", ManageText.Instance.TextFont);
            EnemyCounter = new Text("5", ManageText.Instance.TextFont);
            BulletCounter.CharacterSize *= 3;
            BulletCounter.Origin = new SFML.System.Vector2f(0, 0);
            EnemyCounter.CharacterSize *= 3;
            EnemyCounter.Origin = new SFML.System.Vector2f(0, 0);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            bulletSprite.Position = target.GetView().Center - target.GetView().Size * .5f;
            BulletCounter.Position = bulletSprite.Position + new SFML.System.Vector2f(bulletSprite.Texture.Size.X, 0);
            EnemySprite.Position = bulletSprite.Position + new SFML.System.Vector2f(0, bulletSprite.Texture.Size.Y);
            EnemyCounter.Position = EnemySprite.Position + new SFML.System.Vector2f(EnemySprite.Texture.Size.X, 0);
            
            bulletSprite.Draw(target, states);
            BulletCounter.Draw(target, states);
            EnemySprite.Draw(target, states);
            EnemyCounter.Draw(target, states);
            
        }
    }
}
