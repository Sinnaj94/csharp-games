using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Walk : State
    {
        AbstractCaracter caracter;
        Animation animation;
        public Walk(AbstractCaracter caracter)
        {
            this.caracter = caracter;
            animation = caracter.WalkAnimation;
        }
        public override void Update()
        {
            caracter.PlayerSprite.TextureRect = animation.Animate();
        }
    }
}
