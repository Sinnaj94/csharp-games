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
using Microsoft.Xna.Framework;

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
            recoil = 10f;
            
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
            shipSprite.Origin = new SFML.System.Vector2f(shipSprite.GetGlobalBounds().Width / 2, shipSprite.GetGlobalBounds().Height / 2);
        }

        public void Shoot()
        {
            bullets.AddBullet(BulletFactory.CreateBullet(body.Position.X, body.Position.Y, 0, this.world, body));
            body.ApplyForce(new Microsoft.Xna.Framework.Vector2(0f, recoil), body.WorldCenter);
        }

        public void RotateTo(Body target)
        {
             double targetrotation = Math.Atan2(target.Position.Y - this.body.Position.Y, target.Position.X - this.body.Position.Y);
            //double targetrotation = Math.Atan2(target.Position.Y - this.body.Position.Y, target.Position.X - this.body.Position.X);
            Console.WriteLine("" + body.Rotation);
            //body.Rotation = (float)targetrotation;
           


            
            if (this.body.Rotation % 180 < targetrotation)
            {
                this.body.Rotation += ConvertUnits.ToSimUnits(1);
            }

            if (this.body.Rotation % 180 > targetrotation)
            {
                this.body.Rotation -= ConvertUnits.ToSimUnits(1);
            }
           
        }

        public override void Update()
        {
            Console.WriteLine("Sprite: " + shipSprite.Rotation);
            Console.WriteLine("body: " + ConvertUnits.ToDisplayUnits(body.Rotation));
            shipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
            shipSprite.Rotation = ConvertUnits.ToDisplayUnits(body.Rotation);
            bullets.Update();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            shipSprite.Draw(target, states);
            bullets.Draw(target, states);
        }
    }
}
