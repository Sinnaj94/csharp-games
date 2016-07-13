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

        float threshold = .01f;

        Statemachine statemachine;

        public abstract void updateExtension();

        public void initAnimations(String jsonname, Texture texture)
        {
            PlayerTexture = texture;
            PlayerSprite = new Sprite(PlayerTexture);
            PlayerSprite.Origin += new Vector2f(16, 16);
            statemachine = new Statemachine(this, jsonname, texture);
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

        /* public void Move(float dx, float dy)
         {
             body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dx, ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dy), body.WorldCenter);
         }*/

        public virtual void move(Vector2f speed)
        {
            //Set speed to 1, if it is too big

            if (Math.Abs(speed.X) > 1)
            {
                speed.X /= speed.X;
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

            PlayerSprite.Rotation = MathHelper.ToDegrees(body.Rotation);
            PlayerSprite.Position = Vector2fExtensions.toVector2f(body.Position);

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

        public override void Update()
        {


            

            statemachine.Update();

            updateExtension();
        }

        public Vector2 GetSpeed()
        {
            return body.GetLinearVelocityFromLocalPoint(body.Position);
        }


        public float GetTotalSpeed()
        {

            return (float)Math.Sqrt(Math.Pow(GetSpeed().X, 2) + Math.Pow(GetSpeed().Y, 2));
        }
    }


}


