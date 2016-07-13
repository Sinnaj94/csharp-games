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
        public Walk(AbstractCaracter caracter, Animation animation)
        {
            setAnimation(animation);
            this.caracter = caracter;
        }
        public override void Update()
        {
            caracter.PlayerSprite.TextureRect = Animation.Animate();
        }
    }
}