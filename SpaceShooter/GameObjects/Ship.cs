using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;

namespace SpaceShooter.GameObjects
{
    
    class Ship : GameObject, IRenderable, IMovable
    {

        private Sprite shipSprite;
        public bool col;

        public void init()
        {
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
        }
        
        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            col = true;
            return true;
        }
        
        public void initSprite()
        {
            shipSprite = new Sprite(new Texture(@"Resources\Ships.png"), new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1], SpriteBounds[3]));
        }

        public override void Update()
        {
            // Sprite to Pos in Display coords
            shipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            shipSprite.Draw(target, states); 
        }
    }
}
