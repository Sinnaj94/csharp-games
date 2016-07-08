using System;
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
    class Player : SFML.Graphics.Drawable
    {
        Body body;
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
            spriteAnimation = _temp.ReturnedAnimation;
            //Texture stuff
            playerTexture = new Texture(@"Resources/Character.png");
            playerSprite = new Sprite(playerTexture);
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

        /// <summary>
        /// Makes the Player Jump
        /// </summary>
        public void Jump()
        {
            //TODO: If touches Ground
            if (GetSpeed().Y == 0)
            {
                Body.ApplyForce(jumpForce);
                //Output.Instance.print("Player jumps");
            }


        }

        /// <summary>
        /// Returns the Current speed of the Player
        /// </summary>
        /// <returns>Current Speed of the Player</returns>
        private Vector2 GetSpeed()
        {
            return Body.GetLinearVelocityFromLocalPoint(Body.Position);
        }


        /// <summary>
        /// Returns if a player can jump.
        /// </summary>
        /// <returns></returns>
        private bool canJump()
        {
            return false;

        }

        /// <summary>
        /// Moves the player in a direction.
        /// </summary>
        /// <param name="speed">Direction with additional speed vector</param>
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

        private Vector2f toVector2f(Vector2 _in)
        {

            
            return new Vector2f(_in.X*100, _in.Y*100);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            playerSprite.Position = toVector2f(body.Position);
            playerSprite.Draw(target, states);
        }
    }
}
