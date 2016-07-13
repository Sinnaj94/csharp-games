using System;
using SFML.Graphics;
using FarseerPhysics;
using SFML.System;

namespace JumpAndRun
{
    class Background : SFML.Graphics.Drawable
    {
        RectangleShape test;
        Color _rectColor;
        Random r;
        byte _r;
        byte _g;
        byte _b;
        Color _tempColor;
        Clock c;
        Time a;
        int time;
        int actualFrames;
        public Background()
        {
            _rectColor = new Color(255,255,255);
            test = new RectangleShape();
            r = new Random();
            c = new Clock();
            a = Time.FromMilliseconds(2000);
           

        }

        public void Update(Vector2f newPosition)
        {
            test.Position = newPosition;
        }

        private void SwitchColorThrough()
        {
            if(c.ElapsedTime > a)
            {
                _tempColor = _rectColor;
                RandomColor();
                c = new Clock();
                actualFrames = time;

                time = 0;
            }
            time++;
            float percent = (float)time / (float)actualFrames;

            
            
            _rectColor = new Color((byte)(_tempColor.R*(1-percent)+_r*percent), (byte)(_tempColor.G *(1-percent)+ _g * percent), (byte)(_tempColor.B*(1-percent) + _b * percent));
            test.FillColor = _rectColor;
        }

        private void RandomColor()
        {
            _r = (byte)r.Next(0, 255);
            _g = (byte)r.Next(0, 255);
            _b = (byte)r.Next(0, 255);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            SwitchColorThrough();

            test.Size = (new SFML.System.Vector2f(target.DefaultView.Size.X, target.DefaultView.Size.Y));
            
            test.Draw(target, states);
        }
    }
}