using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace JumpAndRun
{
    public class Tile : SFML.Graphics.Drawable
    {
        int x;
        int y;
        SFML.Graphics.Sprite tileSprite { get; set; }
        bool isCollidable;

        public int X
        {
            get
            {
                return x;
            }

            set
            {
                x = value;
            }
        }

        public int Y
        {
            get
            {
                return y;
            }

            set
            {
                y = value;
            }
        }

        public bool IsCollidable
        {
            get
            {
                return isCollidable;
            }

            set
            {
                isCollidable = value;
            }
        }

        RectangleShape test;

        public Tile(int x, int y, Sprite tileSprite, bool isCollidable)
        {
            this.x = x;
            this.y = y;
            this.tileSprite = tileSprite;
            this.isCollidable = isCollidable;
            initDebug();
        }

        public void initDebug()
        {
            test = new RectangleShape(new SFML.System.Vector2f(2, 2));
            test.Position = new SFML.System.Vector2f(x * 32, y * 32);
            if (this.isCollidable)
            {
                test.FillColor = Color.Red;
            }
            else
            {
                test.FillColor = Color.Green;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if(tileSprite != null)
            {
                tileSprite.Draw(target, states);
            }
            DebugDraw(target, states);

        }

        public void DebugDraw(RenderTarget target, RenderStates states)
        {

            test.Draw(target, states);
        }

    }
}
