﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;


namespace Pong
{
    class Ball : Drawable
    {
        private CircleShape circle;
        private float ballSpeed;
        private Vector2f deltaXY;
        private Vector2f position;
        private Vector2f windowSize;
        private float radius;
        private float durchmesser;
        private int scoreState;
        private RectangleShape boundingBox;
        
        public Ball(Vector2f windowSize, Vector2f deltaXY, Vector2f position, float radius)
        {
            
            Circle = new CircleShape(radius);
            BoundingBox = new RectangleShape();
            BoundingBox.Size = new Vector2f(radius*2,radius*2);
            this.radius = radius;
            this.durchmesser = radius * 2;
            this.deltaXY = deltaXY;
            this.position = new Vector2f(position.X - (1 / 2 * Circle.Radius), position.Y - (1 / 2 * Circle.Radius));
            this.windowSize = windowSize;
            ballSpeed = 10;
            scoreState = 0;
        }

        public void setBoundingBox()
        {
            BoundingBox.Position = circle.Position;
        }


        public void updatePosition(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition, RectangleShape leftShape, RectangleShape rightShape)
        {

            if (position.Y < 0 || position.Y + durchmesser > windowSize.Y)
            {
                if(position.Y < 0)
                {
                    position.Y = 0;
                }
                else
                {
                    position.Y = windowSize.Y - durchmesser;
                }
                deltaXY.Y = -deltaXY.Y;
                ManageSound.Instance.side();
            }


        if(playerCollision(YBoundMin, YBoundMax, playerXPosition, KiYBoundMin, KiYBoundMax, KiXPosition))
            {
                ballSpeed += 1;
                ManageSound.Instance.hit();
            }




            position.X += deltaXY.X;
            position.Y += deltaXY.Y;
            Circle.Position = position;
           // setBoundingBox();
            CheckOutOfBounds();


        }

        public void moveByOne(Vector2f deltaXY)
        {
            Vector2f delta = deltaXY;
            if(delta.X >= delta.Y)
            {
                delta.Y = delta.Y / delta.X;
                delta.X = 1;
                
            } else
            {
                delta.X = delta.X / delta.Y;
                delta.Y = 1;
            }
        }

        

        private bool playerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition, RectangleShape leftplayer, RectangleShape rightplayer)
        {
            if (Collision.Instance.collide(leftplayer,boundingBox))
            {

                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                ManageSound.Instance.hit();
                return true;
            }

            if (Collision.Instance.collide(boundingBox,rightplayer))
            {
                double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((5 * Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);
                ballSpeed += 1;
                ManageSound.Instance.hit();
                return true;
            }
            return false;
        }

        private bool playerCollision(float YBoundMin, float YBoundMax, float playerXPosition, float KiYBoundMin, float KiYBoundMax, float KiXPosition)
        {
            if (position.X <= playerXPosition && position.X >= playerXPosition-1 && position.Y+radius > YBoundMin && position.Y < YBoundMax)
            {
                
                double relativeIntersectY = ((YBoundMax + YBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((YBoundMax - YBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((3* Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);

                return true;
            }

            if (position.X + radius >= KiXPosition && position.X <= KiXPosition + 1 && position.Y+radius > KiYBoundMin && position.Y < KiYBoundMax)
            {
                
                double relativeIntersectY = ((KiYBoundMax + KiYBoundMin) / 2) - position.Y;
                double normalizedRelativeIntersectionY = ((relativeIntersectY / ((KiYBoundMax - KiYBoundMin) / 2)));
                double bounceAngle = normalizedRelativeIntersectionY * ((3* Math.PI) / 12);
                deltaXY.X = ballSpeed * (float)-Math.Cos(bounceAngle);
                deltaXY.Y = ballSpeed * (float)-Math.Sin(bounceAngle);

                return true;
            }
            return false;
        }

        public int CheckOutOfBounds()
        {
            // Ball out of Bounds
            if (position.X <= 0 || position.X + radius >= windowSize.X)
            {
                ballSpeed = 10;

                if (position.X <= 0)
                {
                    ManageSound.Instance.lose();
                    deltaXY.X = 1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = windowSize.X / 2;
                    position.Y = windowSize.Y / 2;
                    scoreState = 2;
                    return 0;

                }
                else if (position.X + radius >= windowSize.X)
                {                  
                    deltaXY.X = -1 * ballSpeed;
                    deltaXY.Y = 0;
                    position.X = windowSize.X / 2;
                    position.Y = windowSize.Y / 2;
                    scoreState = 1;
                    return 1;

                }

            }
            scoreState = 0;
            return -1;

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

        public RectangleShape BoundingBox
        {
            get
            {
                return boundingBox;
            }

            set
            {
                boundingBox = value;
            }
        }

        public int ScoreState
        {
            get
            {
                return scoreState;
            }

            set
            {
                scoreState = value;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            ((Drawable)circle).Draw(target, states);
            //((Drawable)boundingBox).Draw(target, states);
        }

    }
}