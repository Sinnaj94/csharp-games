using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public enum StateStatus
    {
        Running,
        Terminated
    }

    public abstract class State
    {
        Animation animation;
        public void setAnimation(Animation animation)
        {
            this.Animation = animation;
        }
        public abstract void Update();
        private StateStatus status;    
        public void Started()
        {
            this.Status = StateStatus.Running;
        }
        public void Terminate()
        {
            this.Status = StateStatus.Terminated;
        }
        public StateStatus Status
        {
            get
            {
                return status;
            }

            set
            {
                status = value;
            }
        }
        public Animation Animation
        {
            get
            {
                return animation;
            }

            set
            {
                animation = value;
            }
        }
    }
}
