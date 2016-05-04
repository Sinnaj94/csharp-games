using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Graphics;
namespace Breakout.GameObjects
{
    class Feature : GameObject
    {
        Paddle paddle;
        RectangleShape rectangle;
        Clock featureTime;
        int featureNumber;
        Random r;
        bool animationDone;
        int steps;
        int currentStep;
        Text featureTimeText;
        public int FeatureNumber
        {
            get
            {
                return featureNumber;
            }

            set
            {
                featureNumber = value;
            }
        }

        public Feature(int featureNumber, Paddle p)
        {
            featureTime = new Clock();
            r = new Random();
            this.FeatureNumber = featureNumber;
            this.paddle = p;
            rectangle = new RectangleShape(p.PaddleShape);
            animationDone = false;
            steps = 10;
            currentStep = 0;
        }

        public Feature(Paddle p)
        {
            featureTime = new Clock();
            r = new Random();
            this.FeatureNumber = getRandomFeatureNumber();
            this.paddle = p;
            rectangle = new RectangleShape(p.PaddleShape);
            animationDone = false;
            steps = 10;
            currentStep = 0;
        }

        public void resetClock()
        {
            featureTime = new Clock();
        }

        private int getRandomFeatureNumber()
        {
            return r.Next(0, 3);
        }

        public void giveFeature()
        {
            switch (FeatureNumber)
            {
                case 0:
                    smoothSizeChange(.5f);
                    break;
                case 1:
                    break;
                case 2:
                    break;

            }
        }

        //new Size -> percent
        private void smoothSizeChange(float percent)
        {
            float newValue = rectangle.Size.X + (rectangle.Size.X * currentStep / steps)*percent;
            paddle.setSize(newValue);
            if(currentStep < 10)
            {
                currentStep++;
            }  
        }

        private void revertSmoothSizeChange(float percent)
        {
            float newValue = rectangle.Size.X + (rectangle.Size.X * currentStep / steps) * percent;
            paddle.setSize(newValue);
            if (currentStep > 0)
            {
            
                currentStep--;
            }
        }

        public void takeFeature()
        {
            switch (FeatureNumber)
            {
                case 0:
                    revertSmoothSizeChange(.5f);
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

        public override void Draw(RenderTarget target, RenderStates states)
        {

        }

        public override void update()
        {

        }
    }
}
