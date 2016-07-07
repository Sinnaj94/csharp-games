using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    class Player
    {
        Body body;
        Vector2 jumpForce;
        float movingSpeed;
        float maxSpeed;
        Animation spriteAnimation;
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
            Body.FixedRotation = true;
            SpriteBuilder _temp = new SpriteBuilder("player");
            spriteAnimation = _temp.ReturnedAnimation;
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
    }
}
