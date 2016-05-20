using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceShooter
{
    abstract class GameObject
    {
        public abstract void Update();
        public String name { get; set; }
        public int HP { get; set; }
        public int maxHP { get; set; }
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
        public void Move(double dx, double dy)
        {
            x += dx*maxSpeed;
            y += dy*maxSpeed;
        }
    }
}
