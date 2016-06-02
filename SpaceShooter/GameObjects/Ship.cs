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
        private List<GameObject> explosions;
        Shield s;
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

        internal Shield S
        {
            get
            {
                return s;
            }

            set
            {
                s = value;
            }
        }

        public void init()
        {
            r = new Random();
            recoil = 10;
            bulletSpeed = 3;
            bulletSpeedBig = 3;
            Life = maxHP;
            maxSpeed = ConvertUnits.ToSimUnits((float)maxSpeed);
            c = new Clock();
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
            bullets = new BulletContainer();
            hud = new DrawShipAttributes(this);
            explosions = new List<GameObject>();
            Console.WriteLine("bullets: " + fireRateBigMS);
            S = new Shield(this);
        }
        
        public void initShootRandomClock()
        {
            currentRandomMilliseconds = r.Next(500, 2000);
            shootRandomClock = new Clock();
        }
        public void shootRandomly()
        {
            if (shootRandomClock.ElapsedTime.AsMilliseconds() >= currentRandomMilliseconds)
            {
                ShootBig();
                initShootRandomClock();
            }
        }
        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {

            Vector2 contactNormal;
            FarseerPhysics.Common.FixedArray2<Vector2> contactPoints;
            contact.GetWorldManifold(out contactNormal, out contactPoints);
            explosions.Add(new Explosion(contactPoints[0]));

            col = true;
            if (fixtureB.CollisionCategories == Category.Cat3)
            {
                DeltaLife(-.5f);
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
            if (c.ElapsedTime.AsMilliseconds() >= fireRateMS)
            {
                double _dx = -Math.Sin((this.body.Rotation));
                double _dy = Math.Cos((this.body.Rotation));
                bullets.AddBullet(BulletFactory.CreateBullet(body.Position.X, body.Position.Y, 5, this.world, body, _dx * bulletSpeed, _dy * bulletSpeed));
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
        public void MoveAndRotateTo(Ship target)
        {
            body.LinearVelocity = new Vector2((float)target.x - body.Position.X, (float)target.y - body.Position.Y);
            body.LinearVelocity.Normalize();
            body.LinearVelocity *= (float)maxSpeed;
            float angle = (float)(Math.Atan2(target.x - body.WorldCenter.X, target.y - body.WorldCenter.Y) * -1);
            body.Rotation = angle;
        }
        public override void Update()
        {
            x = body.Position.X;
            y = body.Position.Y;
            ShipSprite.Position = new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits((float)x), ConvertUnits.ToDisplayUnits((float)y));
            ShipSprite.Rotation = MathHelper.ToDegrees(body.Rotation + (float)Math.PI);
            bullets.Update();
            hud.Update();
            S.Update();
        }
        public void DeltaLife(float delta)
        {
            if(!S.Active)
            {
                life += delta;

            }
            S.changeShield(delta);
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            S.Draw(target, states);
            ShipSprite.Draw(target, states);
            bullets.Draw(target, states);
            //Lifebar zeichnen
            hud.Draw(target, states);
            if (explosions != null)
            {
                foreach (Explosion exp in explosions)
                {
                    exp.Draw(target, states);
                }
            }
            
        }
    }


}
