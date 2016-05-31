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
        double fireRateMS;
        double fireRateBigMS;
        float recoil;
        double bulletSpeed;
        double bulletSpeedBig;
        float life;
        DrawShipAttributes hud;
        Clock shootRandomClock;
        int currentRandomMilliseconds;
        Vector2f cursorPosition;
        Random r;
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

        public void init()
        {
            r = new Random();
            recoil = 10;
            fireRateMS = 100;
            fireRateBigMS = 500;
            bulletSpeed = 3;
            bulletSpeedBig = 3;
            Life = maxHP;
            c = new Clock();
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
            bullets = new BulletContainer();
            hud = new DrawShipAttributes(this);
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
            return true;
        }
        
        public void initSprite()
        {
            shipSprite = new Sprite(new Texture(@"Resources\Ships.png"), new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1], SpriteBounds[3]));
            shipSprite.Origin = new SFML.System.Vector2f(shipSprite.GetGlobalBounds().Width / 2, shipSprite.GetGlobalBounds().Height / 2);
        }

        /*
        SFML.Graphics.IntRect DebugRect;
        SFML.Graphics.RectangleShape DebugShape;
        SFML.Graphics.CircleShape DebugFrontShape;

        public void debugDraw(RenderTarget target, RenderStates states)
        {

            DebugRect = new IntRect(SpriteBounds[0], SpriteBounds[2], SpriteBounds[1], SpriteBounds[3]);
            DebugShape = new SFML.Graphics.RectangleShape(new SFML.System.Vector2f(DebugRect.Width, DebugRect.Height));
            DebugShape.OutlineColor = new Color(255, 255, 255);
            DebugShape.FillColor = new Color(0, 0, 0, 0);
            DebugShape.OutlineThickness = 1;
            DebugShape.Origin = shipSprite.Origin;

            DebugFrontShape = new SFML.Graphics.CircleShape(10, 3);
            DebugFrontShape.FillColor = new Color(255, 0, 0);

            DebugShape.Position = shipSprite.Position;
            DebugShape.Rotation = shipSprite.Rotation;
            DebugShape.Draw(target, states);

            DebugFrontShape.Position = shipSprite.Position;
            DebugFrontShape.Rotation = shipSprite.Rotation;
            DebugFrontShape.Draw(target, states);
        }
        */
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
            shipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(body.Position.X), ConvertUnits.ToDisplayUnits(body.Position.Y));
            shipSprite.Rotation = MathHelper.ToDegrees(body.Rotation + (float)Math.PI);
            bullets.Update();
            hud.Update();
        }

        public void DeltaLife(float delta)
        {
            life += delta;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {

            shipSprite.Draw(target, states);
            bullets.Draw(target, states);
            //Lifebar zeichnen
            hud.Draw(target,states);
        }
    }
}
