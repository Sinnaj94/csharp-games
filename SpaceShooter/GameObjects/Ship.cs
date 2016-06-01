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
using SFML.System;
namespace SpaceShooter.GameObjects
{
    
    class Ship : GameObject, IRenderable, IMovable
    {

        private Sprite shipSprite;
        public bool col;
        private BulletContainer bullets;
        Clock c;
        float recoil;
        double bulletSpeed;
        double bulletSpeedBig;
        float life;
        DrawShipAttributes hud;
        Clock shootRandomClock;
        int currentRandomMilliseconds;
        Vector2f cursorPosition;
        Random r;
        public double fireRateMS { get; set; }
        public double fireRateBigMS { get; set; }

        public float Life
        {
            get
            {
                return life;
            }

            set
            {
                life = value;
            }
        }

        public Vector2f CursorPosition
        {
            get
            {
                return cursorPosition;
            }

            set
            {
                cursorPosition = value;
            }
        }

        public Sprite ShipSprite
        {
            get
            {
                return shipSprite;
            }

            set
            {
                shipSprite = value;
            }
        }

        public void init()
        {
            r = new Random();
            recoil = 10;
            bulletSpeed = 3;
            bulletSpeedBig = 3;
            Life = maxHP;
            c = new Clock();
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
            bullets = new BulletContainer();
            hud = new DrawShipAttributes(this);
            Console.WriteLine("bullets: " + fireRateBigMS);
        }

        public void initShootRandomClock()
        {
            currentRandomMilliseconds = r.Next(500, 2000);
            shootRandomClock = new Clock();
        }

        public void shootRandomly()
        {
            if(shootRandomClock.ElapsedTime.AsMilliseconds() >= currentRandomMilliseconds)
            {
                ShootBig();
                initShootRandomClock();
            }
        }
        
        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            col = true;
            if(fixtureB.CollisionCategories == Category.Cat3)
            {
                life--;
            }
            return true;
        }
        
        public void initSprite()
        {
            ShipSprite = new Sprite(new Texture(@"Resources\Ships.png", new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1] - SpriteBounds[0], SpriteBounds[3] - SpriteBounds[2])));
            ShipSprite.Origin = new SFML.System.Vector2f((ShipSprite.GetGlobalBounds().Left + ShipSprite.GetGlobalBounds().Width) / 2,
                                                              (ShipSprite.GetGlobalBounds().Top + ShipSprite.GetGlobalBounds().Height) / 2);
            Console.WriteLine("width: " + ShipSprite.GetGlobalBounds().Width);
        }

        public void Shoot()
        {
            if(c.ElapsedTime.AsMilliseconds() >= fireRateMS)
            {
                double _dx = -Math.Sin((this.body.Rotation));
                double _dy = Math.Cos((this.body.Rotation));
                bullets.AddBullet(BulletFactory.CreateBullet(body.Position.X, body.Position.Y, 5, this.world, body, _dx*bulletSpeed, _dy* bulletSpeed));
                body.ApplyForce(new Vector2(0f, recoil), body.WorldCenter);
                ManageSound.Instance.shoot();
                c = new Clock();

            }
        }

        public void ShootBig()
        {
            if (c.ElapsedTime.AsMilliseconds() >= fireRateBigMS)
            {
                double _dx = -Math.Sin((this.body.Rotation));
                double _dy = Math.Cos((this.body.Rotation));
                bullets.AddBullet(BulletFactory.CreateBullet(body.Position.X, body.Position.Y, 10, this.world, body, _dx * bulletSpeedBig, _dy * bulletSpeedBig));
                body.ApplyForce(new Vector2(0f, recoil), body.WorldCenter);
                ManageSound.Instance.shoot1();

                c = new Clock();
            }
        }

        public void RotateTo(Body target)
        { 
            body.LinearVelocity = new Vector2(target.Position.X - body.Position.X, target.Position.Y - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= .1f;
            float angle = (float)(Math.Atan2(target.WorldCenter.X - body.WorldCenter.X, target.WorldCenter.Y - body.WorldCenter.Y) * -1);
            body.Rotation = angle;
        }

        public override void Update()
        {
            ShipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
            ShipSprite.Rotation = MathHelper.ToDegrees(body.Rotation + (float)Math.PI);
            bullets.Update();
            hud.Update();
        }

        public void DeltaLife(float delta)
        {
            life += delta;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ShipSprite.Draw(target, states);
            bullets.Draw(target, states);
            //Lifebar zeichnen
            hud.Draw(target,states);
        }
    }
}
