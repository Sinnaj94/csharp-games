using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SpaceShooter.GameObjects
{
    class Player : GameObject, IRenderable, IMovable
    {
        private Sprite shipSprite;

        public void initSprite()
        {
            shipSprite = new Sprite(new Texture(@"Resources\Ships.png"), new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1], SpriteBounds[3]));
            shipSprite.Position = new SFML.System.Vector2f((float)x, (float)y);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
