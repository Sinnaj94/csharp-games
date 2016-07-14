using System;
using SFML.Graphics;
using FarseerPhysics;
using SFML.System;

namespace JumpAndRun
{
    class Background : SFML.Graphics.Drawable
    {
        RectangleShape test;

        ColorRainbow colorRainbow;


        public Background()
        {
            colorRainbow = new ColorRainbow(Time.FromMilliseconds(3000), new Color(255, 255, 255),0);


            test = new RectangleShape();


            test.FillColor = colorRainbow.SwitchColorThrough();
            //test.OutlineThickness = 20;


        }

        public void Update(Vector2f newPosition)
        {
            newPosition += new Vector2f(test.OutlineThickness, test.OutlineThickness);
            test.Position = newPosition;
            
        }


        private void SetSize()
        {

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            test.Size = (new SFML.System.Vector2f(target.DefaultView.Size.X - test.OutlineThickness*2, target.DefaultView.Size.Y - test.OutlineThickness*2));

            test.FillColor = colorRainbow.SwitchColorThrough();


            test.Draw(target, states);
        }
    }

    class ColorRainbow
    {
        int time;
        int actualFrames;
        Color _rectColor;
        Color _tempColor;
        Color _randomColor;
        Clock c;
        Time a;
        Random r;
        public ColorRainbow(Time fadeTime, Color startColor,int seed)
        {
            _rectColor = startColor;
            r = new Random(seed);
            c = new Clock();
            a = fadeTime;
        }

        public Color SwitchColorThrough()
        {
            if (c.ElapsedTime > a)
            {
                _tempColor = _randomColor;
                RandomColor();
                c = new Clock();
                actualFrames = time;

                time = 0;
            }
            time++;
            float percent = (float)time / (float)actualFrames;



            return ColorDifference(percent);
        }

        private Color ColorDifference(float percent)
        {
            return new Color((byte)(_tempColor.R * (1 - percent) + _randomColor.R * percent), (byte)(_tempColor.G * (1 - percent) + _randomColor.G * percent), (byte)(_tempColor.B * (1 - percent) + _randomColor.B * percent));

        }

        private void RandomColor()
        {
            _randomColor.R = (byte)r.Next(0, 255);
            _randomColor.G = (byte)r.Next(0, 255);
            _randomColor.B = (byte)r.Next(0, 255);
        }

    }
    class MultipleRectangle
    {

    }
}