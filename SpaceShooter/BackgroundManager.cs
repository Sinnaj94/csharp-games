﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SpaceShooter
{
    // quite boring as of yet
    class BackgroundManager : IRenderable
    {
        private Sprite bg;
        
        public BackgroundManager()
        {
            bg = new Sprite(new Texture(@"Resources\bg.jpg"), new IntRect(0, 0, 2880, 1800));
            bg.Scale = new SFML.System.Vector2f(.7f, .7f);
            bg.Color = new Color(255, 255, 255, 255); 
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            bg.Draw(target, states);
        }
    }
}
