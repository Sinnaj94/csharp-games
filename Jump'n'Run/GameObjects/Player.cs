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

    abstract class AbstractCaracter : GameObject, SFML.Graphics.Drawable
    {
        Animation idleAnimation;
        Animation runAnimation;
        Animation walkAnimation;
        Animation jumpAnimation;
        Animation _currentAnimation;
        float movingSpeed;
        Texture playerTexture;
        Sprite playerSprite;

        public void initAnimations(String jsonname, Texture texture)
        {
            SpriteBuilder _temp = new SpriteBuilder(jsonname);
            PlayerTexture = texture;
            PlayerSprite = new Sprite(PlayerTexture);
            PlayerSprite.Origin += new Vector2f(16, 16);
            IdleAnimation = _temp.AnimationList.GetAnimation("idle", PlayerTexture);
            RunAnimation = _temp.AnimationList.GetAnimation("run", PlayerTexture);
            WalkAnimation = _temp.AnimationList.GetAnimation("walk", PlayerTexture);
            PlayerSprite.TextureRect = IdleAnimation.RectangleList[0];
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
        }

        public void Move(float dx, float dy)
        {
            body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dx, ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dy), body.WorldCenter);
        }


        internal Animation IdleAnimation
        {
            get
            {
                return idleAnimation;
            }

            set
            {
                idleAnimation = value;
            }
        }

        internal Animation RunAnimation
        {
            get
            {
                return runAnimation;
            }

            set
            {
                runAnimation = value;
            }
        }

        internal Animation WalkAnimation
        {
            get
            {
                return walkAnimation;
            }

            set
            {
                walkAnimation = value;
            }
        }

        internal Animation CurrentAnimation
        {
            get
            {
                return _currentAnimation;
            }

            set
            {
                _currentAnimation = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            PlayerSprite.Position = Vector2fExtensions.toVector2f(body.Position);
            PlayerSprite.Rotation = MathHelper.ToDegrees(body.Rotation );
            PlayerSprite.Draw(target, states);
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

        public Vector2 GetSpeed()
        {
            return body.GetLinearVelocityFromLocalPoint(body.Position);
        }

    }

    class Player : AbstractCaracter
    {
        Vector2 positionChangedVector;

        public Player(Body body, Vector2 bodySize)
        {
            this.body = body;
            initAnimations("player", new Texture(@"Resources/Sprites/enemy1.png"));
            InitPhysics(@"Resources\physicsattributes.json");
            body.BodyType = BodyType.Dynamic;
            body.LinearVelocity = new Vector2(0, 0);
        }



        public event EventHandler onPositionChanged;

        protected virtual void PositionChanged(EventArgs e)
        {
            if (onPositionChanged != null) onPositionChanged(this, e);
        }

        public void CheckPositionChange()
        {
            if(Math.Abs(positionChangedVector.X - this.body.Position.X) > .5f || Math.Abs(positionChangedVector.Y - this.body.Position.Y) > .5f)
            {
                positionChangedVector = this.body.Position;
                PositionChanged(EventArgs.Empty);
            }
        }

        public void move(float speed)
        {
            //Set speed to 1, if it is too big
            if (Math.Abs(speed) > 1)
            {
                speed /= speed;
            }

            body.ApplyForce(new Vector2(MovingSpeed * speed, 0));

           // body.LinearVelocity = new Vector2(MovingSpeed);
        }

        public override void Update()
        {
            if (GetSpeed().X == 0)
            {
                CurrentAnimation = IdleAnimation;
            }
            else if (Math.Abs(GetSpeed().X) > 0 && Math.Abs(GetSpeed().X) < 1)
            {
                CurrentAnimation = WalkAnimation;
            }
            else if (Math.Abs(GetSpeed().X) > 1)
            {
                CurrentAnimation = RunAnimation;
            }

            CheckPositionChange();
            PlayerSprite.TextureRect = CurrentAnimation.Animate();      
        }
    }
}
