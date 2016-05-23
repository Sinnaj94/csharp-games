using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using SpaceShooter.Factories;

namespace SpaceShooter.GameObjects
{
    
    class Ship : GameObject, IRenderable, IMovable
    {

        private Sprite shipSprite;
        public bool col;
        private BulletContainer bullets;
        private float recoil;
        public void init()
        {
            recoil = 1f;
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
            bullets = new BulletContainer();
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

        public void Shoot()
        {
            bullets.AddBullet(BulletFactory.CreateBullet(body.Position.X, body.Position.Y, 0, this.world, body));
            body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(0f, recoil), body.WorldCenter);
        }

        public override void Update()
        {
            // Sprite to Pos in Display coords
            shipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
            bullets.Update();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            shipSprite.Draw(target, states);
            bullets.Draw(target, states);
        }
    }
}
