using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Dead : State
    {
        AbstractCaracter caracter;
        public Dead(AbstractCaracter caracter, Animation animation)
        {
            setAnimation(animation);
            this.caracter = caracter;
        }
        public override void Update()
        {
            caracter.PlayerSprite.TextureRect = Animation.Animate();
            caracter.body.Awake = false;
        }
    }
}
