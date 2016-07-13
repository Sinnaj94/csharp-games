using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Attack : State
    {
        AbstractCaracter caracter;

        public Attack(AbstractCaracter caracter, Animation animation)
        {
            setAnimation(animation);
            this.caracter = caracter;
            Status = StateStatus.Terminated;
        }
        public override void Update()
        {
            if(Status == StateStatus.Running)
            {
                caracter.PlayerSprite.TextureRect = Animation.Animate();
            }    

            if (this.Animation.Terminated)
            {
                Status = StateStatus.Terminated;
                this.Animation.Restart();
            } 
        }
    }
}
