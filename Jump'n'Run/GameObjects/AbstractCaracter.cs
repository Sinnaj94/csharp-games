using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
using FarseerPhysics;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Factories;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics.Contacts;

namespace JumpAndRun
{

    public abstract class AbstractCaracter : GameObject, SFML.Graphics.Drawable
    {
        float movingSpeed;
        Texture playerTexture;
        Sprite playerSprite;
        int bulletCount;
        float threshold = .01f;

        Statemachine statemachine;

        public abstract void updateExtension();

        public void initAnimations(String jsonname, Texture texture)
        {
            PlayerTexture = texture;
            PlayerSprite = new Sprite(PlayerTexture);
            PlayerSprite.Origin += new Vector2f(16, 16);
            Statemachine = new Statemachine(this, jsonname, texture);
        }

        public void InitPhysics(String jsonname)
        {
            PhysicsBuilder p = new PhysicsBuilder(jsonname);
            PhysicsSettings _tempP = p.PhysicsReturn;
            this.MovingSpeed = _tempP.acceleration;
            this.maxSpeed = _tempP.maxSpeed;
            this.body.Friction = _tempP.friction;
            this.body.GravityScale = _tempP.mass;
            this.body.Restitution = .1f;
            body.SleepingAllowed = false;
            body.OnCollision += new OnCollisionEventHandler(Body_OnCollision);
        }

        public void Move(float dx, float dy)
        {
            body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dx, ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dy), body.WorldCenter);
        }

        Clock _footStepClock = new Clock();
        Time footStepTime = Time.FromMilliseconds(200);
        public virtual void move(Vector2f speed)
        {

            //Set speed to 1, if it is too big
            if (Math.Abs(speed.X) > 1)
            {
                speed.X /= speed.X;
            }
            
            if (_footStepClock.ElapsedTime > footStepTime)
            {
                ManageSound.Instance.footStep();
                _footStepClock = new Clock();

                int _tempTime = 120 - (int)(((Math.Abs(speed.X) + Math.Abs(speed.Y)) * 100) / 2);
                footStepTime = Time.FromMilliseconds(_tempTime * 500 / 100);
                Console.WriteLine(_tempTime);

            }

            if (Math.Abs(speed.Y) > 1)
            {
                speed.Y /= speed.Y;
            }

            body.ApplyForce(new Vector2(MovingSpeed * speed.X, MovingSpeed * speed.Y));

            // body.LinearVelocity = new Vector2(MovingSpeed);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            float _temp = statemachine.CurrentState.Animation.PixelSize;
            PlayerSprite.Origin = new Vector2f(_temp / 2, _temp / 2);
            PlayerSprite.Position = Vector2fExtensions.toVector2f(body.Position);
            PlayerSprite.Rotation = MathHelper.ToDegrees(body.Rotation);
            PlayerSprite.Draw(target, states);
        }

        public bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            Vector2 contactNormal;
            FarseerPhysics.Common.FixedArray2<Vector2> contactPoints;
            contact.GetWorldManifold(out contactNormal, out contactPoints);

            if (fixtureB.CollisionCategories == Category.Cat3)
            {
                ManageSound.Instance.splatter();
                this.isDead = true;
            }
            return true;
        }
        public float MovingSpeed
        {
            get
            {
                return movingSpeed;
            }

            set
            {
                movingSpeed = value;
            }
        }
        public Texture PlayerTexture
        {
            get
            {
                return playerTexture;
            }

            set
            {
                playerTexture = value;
            }
        }
        public Sprite PlayerSprite
        {
            get
            {
                return playerSprite;
            }

            set
            {
                playerSprite = value;
            }
        }
        public Statemachine Statemachine
        {
            get
            {
                return statemachine;
            }

            set
            {
                statemachine = value;
            }
        }

        public int BulletCount
        {
            get
            {
                return bulletCount;
            }

            set
            {
                bulletCount = value;
            }
        }

        public override void Update()
        {
            Statemachine.Update();
            if (!isDead) { updateExtension(); }

        }
        public Vector2 GetSpeed()
        {
            return body.GetLinearVelocityFromLocalPoint(body.Position);
        }
        public Vector2 getBodyDirection()
        {
            Vector2 direction = new Vector2((float)Math.Cos(body.Rotation), (float)Math.Sin(body.Rotation));
            direction.Normalize();
            return direction;
        }
        public float GetTotalSpeed()
        {

            return (float)Math.Sqrt(Math.Pow(GetSpeed().X, 2) + Math.Pow(GetSpeed().Y, 2));
        }
    }


}


