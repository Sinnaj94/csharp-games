using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;

namespace SpaceShooter.GameObjects
{
    class Ship : GameObject, IRenderable, IMovable
    {

        private Sprite shipSprite;

        public void initSprite()
        {
            shipSprite = new Sprite(new Texture(@"Resources\Ships.png"), new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1], SpriteBounds[3]));
        }

        public override void Update()
        {
            // Pos in engine coords
            // shipSprite.Position = new SFML.System.Vector2f((float)x, (float)y);
            // Pos in Display coords
            shipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            shipSprite.Draw(target, states); 
        }
    }
}
