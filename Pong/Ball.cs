using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;


namespace Pong
{
    class Ball
    {



        private CircleShape circle;
        private float ballSpeed;
        private Vector2f deltaXY;
        private Vector2f position;
        private RenderWindow window;
        private float radius;
        private float durchmesser;
        private Logic rulesystem;
        private SoundManager soundManage;


        public Ball(RenderWindow window, Vector2f deltaXY, Vector2f position, float radius, Logic rulesystem,SoundManager soundManage)
        {
            Circle = new CircleShape(radius);
            this.radius = radius;
            this.durchmesser = radius * 2;
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * Circle.Radius), position.Y - (1 / 2 * Circle.Radius));
            this.window = window;
            ballSpeed = 10;
            this.rulesystem = rulesystem;
            this.soundManage = soundManage;
        }




        public CircleShape Circle
        {
            get
            {
                return circle;
            }

            set
            {
                circle = value;
            }
        }

        public void updatePosition(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {

            if (position.Y < 0 || position.Y + durchmesser > window.Size.Y)
            {
                if(position.Y < 0)
                {
                    position.Y = 0;
                }
                else
                {
                    position.Y = window.Size.Y - durchmesser;
                }
                deltaXY.Y = -deltaXY.Y;
                soundManage.playSound(2);
            }

            playerCollision(YBoundMin, YBoundMax, playerXPosition, KiYBoundMin, KiYBoundMax, KiXPosition);

            position.X += deltaXY.X;
            position.Y += deltaXY.Y;
            Circle.Position = position;
            CheckOutOfBounds();

        }

        private void playerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {
            if (position.X <= playerXPosition && position.Y+radius > YBoundMin && position.Y < YBoundMax)
            {
                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                soundManage.playSound(0);

            }

            if (position.X + radius >= KiXPosition && position.Y+radius > KiYBoundMin && position.Y < KiYBoundMax)
            {
                
                double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                soundManage.playSound(0);
            }
        }

        public int CheckOutOfBounds()
        {
            //Ball out of Bounds
            if (position.X <= 0 || position.X + radius >= window.Size.X)
            {
                ballSpeed = 10;

                if (position.X <= 0)
                {
                    
                    rulesystem.addPointToPlayer(1);
                    Console.WriteLine("Right player (KI) got a point.");
                    soundManage.playSound(1);
                    deltaXY.X = 1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = window.Size.X / 2;
                    position.Y = window.Size.Y / 2;
                    
                    return 0;

                }
                else if (position.X + radius >= window.Size.X)
                {
                    
                    rulesystem.addPointToPlayer(0);
                    Console.WriteLine("Left player got a point.");
                    deltaXY.X = -1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = window.Size.X / 2;
                    position.Y = window.Size.Y / 2;
                    return 1;

                }

            }
            return -1;

        }

        

        
    }



}