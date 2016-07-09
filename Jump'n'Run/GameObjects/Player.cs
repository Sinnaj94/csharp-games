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

namespace JumpAndRun
{
    class Player : GameObject,  SFML.Graphics.Drawable
    {

        Vector2 jumpForce;
        float movingSpeed;
        float maxSpeed;
        Animation spriteAnimation;
        Texture playerTexture;
        Sprite playerSprite;


        public Player(Body body)
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
            _temp.AnimationList.getAnimation("idle");
            //Texture stuff
            playerTexture = new Texture(@"Resources/Character.png");
            playerSprite = new Sprite(playerTexture);
            playerSprite.TextureRect = new IntRect(0, 0, 200, 100);
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
            if (GetSpeed().Y == 0)
            {
                Body.ApplyForce(jumpForce);
                //Output.Instance.print("Player jumps");
            }
        }

        private Vector2 GetSpeed()
        {
            return Body.GetLinearVelocityFromLocalPoint(Body.Position);
        }

        private bool canJump()
        {
            return false;
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
                if (GetSpeed().X > -maxSpeed)
                {
                    Body.ApplyForce(new Vector2(movingSpeed * speed, 0));
                }
            }
            else if (speed > 0)
            {
                if (GetSpeed().X < maxSpeed)
                {
                    Body.ApplyForce(new Vector2(movingSpeed * speed, 0));

                }
            }
        }



        public void Draw(RenderTarget target, RenderStates states)
        {
            playerSprite.Position = Vector2fExtensions.toVector2f(body.Position);
            playerSprite.Draw(target, states);
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
