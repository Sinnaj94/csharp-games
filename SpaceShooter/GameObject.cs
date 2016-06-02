using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace SpaceShooter
{
    abstract class GameObject
    {
        // FARSSER physics body for col. detection
        public World world;
        public Body body;
        public abstract void Update();
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }
        public int price { get; set; }
        public double speed { get; set; }
        public double maxSpeed { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double dx { get; set; }
        public double dy { get; set; }
        public double drot { get; set; }
        public double rotation { get; set; }
        public int[] SpriteBounds { get; set; }

        /// <summary>
        /// Moves the Object at given speed DX. Multiplies with maxSpeed
        /// </summary>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        public void Move(float dx, float dy)
        {
            body.ApplyLinearImpulse(new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dx, ConvertUnits.ToDisplayUnits((float)maxSpeed) * (float)dy), body.WorldCenter);
        }


        public void Rotate(double newRotation)
        {
            //this.body.Rotation = ConvertUnits.ToSimUnits(newRotation);
            this.body.Rotation = (float)newRotation;
            this.body.AngularVelocity = 0;
        }
    }
}
