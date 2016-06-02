using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics;
namespace SpaceShooter.GameObjects
{
    class Explosion : GameObject, SFML.Graphics.Drawable
    {

        private int spriteSize = 96;
        private int SpriteCountHorizontal = 5;
        private int SpriteCountVertical = 4;
        private int counterHorizontal = 0;
        private int counterVertical = 0;
        SFML.Graphics.Sprite explosionSprite;
        SFML.Graphics.Texture explosionTexture;

        public Explosion(Vector2 position)
        {
            explosionTexture = new Texture(@"Resources/explosion.png");
            explosionSprite = new Sprite(explosionTexture, new IntRect(0, 0, spriteSize, spriteSize));
            explosionSprite.Origin = new SFML.System.Vector2f(spriteSize / 2, spriteSize / 2);
            explosionSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(position.X), ConvertUnits.ToDisplayUnits(position.Y));
        }

        public void SelfDestruct(Explosion e)
        {
            e = null;
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            if (counterVertical < SpriteCountVertical)
            {
                if (counterHorizontal < SpriteCountHorizontal)
                {
                    explosionSprite.TextureRect = new IntRect(spriteSize * counterHorizontal, spriteSize * counterVertical, spriteSize, spriteSize);
                    explosionSprite.Draw(target, states);
                    counterHorizontal++;
                }
                counterVertical++;
            }

            else
            {
                SelfDestruct(this);
            }
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
