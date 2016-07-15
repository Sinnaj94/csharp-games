using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
namespace JumpAndRun
{
    class Bullet : SFML.Graphics.Drawable
    {
        RectangleShape bullet;
        List<RectangleShape> glow;
        Random r;
        public Bullet(float rotation)
        {
            r = new Random();
            float length = r.Next(3, 10);
            bullet = new RectangleShape(new Vector2f(length, 1));
            bullet.Origin = new Vector2f(bullet.Scale.X / 2, bullet.Scale.Y / 2);
            bullet.Rotation = rotation;
            initGlow();

        }

        private void initGlow()
        {
            glow = new List<RectangleShape>();
            int sizeDifference = 1;
            for(int i = 256; i > 0; i-=256/32)
            {
                RectangleShape _tempGlow = new RectangleShape(bullet);
                _tempGlow.Size += new Vector2f(sizeDifference, sizeDifference);
                sizeDifference++;
                _tempGlow.FillColor = new Color(255, 232, 70, (byte)(i-1));
                glow.Add(_tempGlow);
            }

        }
        public void UpdatePosition(Vector2f newPosition)
        {
            bullet.Position = newPosition;
            foreach(RectangleShape _r in glow)
            {
                _r.Position = newPosition;
            }
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            states = new RenderStates(BlendMode.Alpha);
            UpdatePosition(bullet.Position + new Vector2f(1, 2));
            //glow.Draw(target, states);

            bullet.Draw(target, states);

        }
    }
}
