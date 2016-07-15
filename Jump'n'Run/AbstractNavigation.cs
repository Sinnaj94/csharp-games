using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace JumpAndRun
{
    abstract class AbstractNavigation : SFML.Graphics.Drawable
    {


        public virtual void NavigateUp()
        {

        }

        public virtual void NavigateDown()
        {

        }

        public virtual void Enter()
        { 

        }

        public virtual bool Active
        {
            get
            {
                return false;
            }

            set
            {
                
            }
        }

        public virtual void Update()
        {

        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            throw new NotImplementedException();
        }
    }
}
