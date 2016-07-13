using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Idle : State
    {
        AbstractCaracter caracter;
        Animation animation;
        public Idle(AbstractCaracter caracter)
        {
            this.caracter = caracter;
            animation = caracter.IdleAnimation;
        }
        public override void Update()
        {
            caracter.PlayerSprite.TextureRect = animation.Animate();
        }
    }
}