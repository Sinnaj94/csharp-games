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
    class Player : GameObject, SFML.Graphics.Drawable
    {

        Vector2 jumpForce;
        float movingSpeed;
        float maxSpeed;
        Animation idleAnimation;
        Animation runAnimation;
        Animation walkAnimation;
        Animation jumpAnimation;
        Animation _currentAnimation;
        Texture playerTexture;
        Sprite playerSprite;
        Vector2 bodySize;
        bool canJump;
        bool jumpRequested;
        bool faceLeft;

        public Player(Body body, Vector2 bodySize)
        {
            this.Body = body;
            //TODO: Outsourcing of Forces 
            PhysicsBuilder p = new PhysicsBuilder(@"Resources\physicsattributes.json");
            PhysicsSettings _tempP = p.PhysicsReturn;
            this.jumpForce = new Vector2(0, -_tempP.jumpStrength);
            this.movingSpeed = _tempP.acceleration;
            this.maxSpeed = _tempP.maxSpeed;
            this.body.Friction = _tempP.friction;
            this.body.GravityScale = _tempP.mass;
            this.body.Restitution = .1f;
            Body.FixedRotation = true;
            SpriteBuilder _temp = new SpriteBuilder("player");

            //Texture stuff
            playerTexture = new Texture(@"Resources/Character.png");
            playerSprite = new Sprite(playerTexture);
            idleAnimation = _temp.AnimationList.GetAnimation("idle", playerTexture);
            runAnimation = _temp.AnimationList.GetAnimation("run", playerTexture);
            walkAnimation = _temp.AnimationList.GetAnimation("walk", playerTexture);
            jumpAnimation = _temp.AnimationList.GetAnimation("jump", playerTexture);
            playerSprite.TextureRect = idleAnimation.RectangleList[0];
            body.Position = new Vector2(ConvertUnits.ToSimUnits(100), ConvertUnits.ToSimUnits(0));
            body.BodyType = BodyType.Dynamic;
            body.LinearVelocity = new Vector2(0, 0);
            this.bodySize = new Vector2(ConvertUnits.ToSimUnits(32), ConvertUnits.ToSimUnits(32));
            //FixtureFactory.AttachRectangle(ConvertUnits.ToSimUnits(64), ConvertUnits.ToSimUnits(3), 1, new Vector2(0, ConvertUnits.ToSimUnits(35)), body);

            PolygonShape _tempPolygon = new PolygonShape(PolygonTools.CreateRectangle(ConvertUnits.ToSimUnits(32), ConvertUnits.ToSimUnits(3), new Vector2(0, ConvertUnits.ToSimUnits(35)), 0), 0);

            body.CreateFixture(_tempPolygon);
            body.FixtureList[1].IsSensor = true;
            canJump = false;
            jumpRequested = false;
            CreateCollisionListener();
            faceLeft = false;
        }



        private void CreateCollisionListener()
        {

            body.FixtureList[1].OnCollision += Body_OnCollision;
            body.FixtureList[1].OnSeparation += Body_OnSeperation;
        }

        private void Body_OnSeperation(Fixture fixtureA, Fixture fixtureB)
        {
            canJump = false;

        }

        private bool Body_OnCollision(Fixture fixtureA, Fixture fixtureB, Contact contact)
        {
            canJump = true;
            Console.WriteLine(canJump + "");

            return true;
        }

        public Body Body
        {
            get
            {
                return body;
            }

            set
            {
                body = value;
            }
        }

        public void Jump()
        {
            //TODO: If touches Ground
            if (canJump)
            {
                jumpAnimation.Restart();
                Body.ApplyForce(jumpForce);
                Output.Instance.print("Player jumps");
                jumpRequested = true;

            }
        }

        private Vector2 GetSpeed()
        {
            return Body.GetLinearVelocityFromLocalPoint(Body.Position);
        }




        public void move(float speed)
        {
            //Set speed to 1, if it is too big
            if (Math.Abs(speed) > 1)
            {
                speed /= speed;
            }
            if (speed < 0)
            {
                faceLeft = true;
                if (GetSpeed().X > -maxSpeed)
                {
                    
                    Body.ApplyForce(new Vector2(movingSpeed * speed, 0));
                }
            }
            else if (speed > 0)
            {
                faceLeft = false;
                if (GetSpeed().X < maxSpeed)
                {
                    Body.ApplyForce(new Vector2(movingSpeed * speed, 0));

                }
            }

        }



        public void Draw(RenderTarget target, RenderStates states)
        {
            float _tempDirection = 1;
            if (faceLeft)
            {
                _tempDirection *= -1;
            }
            bodySize = new Vector2(Math.Abs(bodySize.X) * _tempDirection, bodySize.Y);
            playerSprite.Scale = new Vector2f(Math.Abs(playerSprite.Scale.X )* _tempDirection, playerSprite.Scale.Y);
            
            playerSprite.Position = Vector2fExtensions.toVector2f(body.Position - bodySize);

            playerSprite.Draw(target, states);
        }

        public override void Update()
        {
            if (GetSpeed().X == 0)
            {
                _currentAnimation = idleAnimation;
            }
            else if (Math.Abs(GetSpeed().X) > 0 && Math.Abs(GetSpeed().X) < 1)
            {
                _currentAnimation = walkAnimation;
            }
            else if (Math.Abs(GetSpeed().X) > 1)
            {
                _currentAnimation = runAnimation;

            }
            if (!canJump)
            {
                _currentAnimation = jumpAnimation;

            }

            playerSprite.TextureRect = _currentAnimation.Animate();

        }
    }


}
