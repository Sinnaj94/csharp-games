using FarseerPhysics.Dynamics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Attack : State
    {
        Clock c;
        Time fireRate;
        AbstractCaracter caracter;
        AbstractProjectile projectile;
        int nr;
        public Attack(AbstractCaracter caracter, Animation animation, int nr)
        {
            setAnimation(animation);
            this.caracter = caracter;
            Status = StateStatus.Terminated;
            this.nr = nr;
            c = new Clock();
            fireRate = Time.FromMilliseconds(500);
        }

        public void dispose()
        {
            this.projectile.body.Dispose();
            this.projectile.body = null;
        }

        public override void Update()
        {
            if (Status == StateStatus.Running)
            {
                caracter.PlayerSprite.TextureRect = Animation.Animate();
                if (projectile == null)
                {
                    if (nr == 0)
                    {
                        projectile = new MeleeProjectile(caracter);
                        ManageSound.Instance.swoosh();
                    }
                    else if (nr == 1)
                    {
                        ManageSound.Instance.machinegun();
                        projectile = new RangeProjectile(caracter);
                        c = new Clock();
                    }
                }
                else
                {
                    projectile.Update();
                }
            }

            if (this.Animation.Terminated)
            {
                Status = StateStatus.Terminated;
                this.Animation.Restart();
                projectile.body.Dispose();
                projectile = null;
            }
        }
    }
}