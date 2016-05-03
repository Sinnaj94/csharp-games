using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Breakout.GameObjects
{
    class Feature
    {
        Paddle paddle;
        RectangleShape rectangle;
        Clock featureTime;
        int featureNumber;
        Random r;
        public Feature(int featureNumber, Paddle p)
        {
            featureTime = new Clock();
            r = new Random();
            this.featureNumber = featureNumber;
            this.paddle = p;
            rectangle = new RectangleShape(p.PaddleShape);
        }

        private void getRandomFeatureNumber()
        {
            r.Next(0, 3);
        }

        public void giveFeature()
        {
            switch (featureNumber)
            {
                case 0:
                    paddle.setSize(rectangle.Size.X * 1.5f);
                    break;
                case 1:
                    break;
                case 2:
                    break;

            }
        }

        public void takeFeature()
        {
            switch (featureNumber)
            {
                case 0:
                    paddle.setSize(rectangle.Size.X);
                    break;
                case 1:
                    break;
                case 2:
                    break;

            }
        }

        public bool timesUp()
        {
            Time now = featureTime.ElapsedTime;

            if (now.AsSeconds() >= 8)
            {
                return true;
            }
            return false;
        }
    }
}
