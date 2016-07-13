using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public class Statemachine
    {
        State currentState;
        AbstractCaracter caracter;
        State idle;
        State walk;

        public Statemachine(AbstractCaracter caracter)
        {
            this.caracter = caracter;
            idle = new Idle(caracter);
            walk = new Walk(caracter);
        }
        public Statemachine()
        {
           
        }

        public void SwitchState()
        {
            Vector2 speed = caracter.GetSpeed();
            speed.X = Math.Abs(speed.X);
            speed.Y = Math.Abs(speed.Y);

            if (speed.X < .1 && speed.Y < .1)
            {
                currentState = idle;
            }
            else if (speed.X < 1 && speed.Y < 1)
            {
                currentState = walk;
            }
        }

        public void Update()
        {
            SwitchState();
            CurrentState.Update();
        }

        public State CurrentState
        {
            get
            {
                return currentState;
            }

            set
            {
                currentState = value;
            }
        }
    }
}
