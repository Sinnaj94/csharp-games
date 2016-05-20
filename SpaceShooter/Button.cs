using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace SpaceShooter.GameObjects
{
    class Button : GameObject, IRenderable
    {
        String text;

        public void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
