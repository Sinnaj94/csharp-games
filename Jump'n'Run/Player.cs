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
        public Player(Body body)
        {
            this.Body = body;
            //TODO: Outsourcing of Forces 
            this.jumpForce = new Vector2(0, -100);
            this.movingSpeed = .02f;
            Body.FixedRotation = true;
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
                Output.Instance.print("Player started a Jump");
            }


        }

        private Vector2 GetSpeed()
        {
            return Body.GetLinearVelocityFromLocalPoint(Body.Position);
        }

        //Moves the player. Speed (Abs) must be between 0 and 1 (for controller input)
        public void move(float speed)
        {
            //Set speed to 1, if it is too big
            if (Math.Abs(speed) > 1)
            {
                speed /= speed;
            }

            Body.ApplyLinearImpulse(new Vector2(movingSpeed * speed, 0));
            Output.Instance.print("Player walks (" + speed + ")");


            //TODO: Implement
        }
    }
}
