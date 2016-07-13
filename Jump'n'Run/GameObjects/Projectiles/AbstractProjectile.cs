using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public abstract class AbstractProjectile : GameObject
    {
        AbstractCaracter caracter;
        public AbstractCaracter Caracter
        {
            get
            {
                return caracter;
            }

            set
            {
                caracter = value;
            }
        }
        public abstract override void Update();
    }
}
