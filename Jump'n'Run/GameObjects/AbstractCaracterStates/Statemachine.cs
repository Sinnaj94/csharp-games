using Microsoft.Xna.Framework;
using SFML.Graphics;
using SFML.System;
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
        State attack;

        public void initAnimations(String spriteJsonAttribute, Texture caracterTexture)
        {
            SpriteBuilder _temp = new SpriteBuilder(spriteJsonAttribute);
            idle = new Idle(caracter, _temp.AnimationList.GetAnimation("idle", caracterTexture));
            walk = new Walk(caracter, _temp.AnimationList.GetAnimation("walk", caracterTexture));
            attack = new Attack(caracter, _temp.AnimationList.GetAnimation("attack", caracterTexture));
        }
        public Statemachine(AbstractCaracter caracter, String spriteJsonAttribute, Texture caracterTextur)
        {
            this.caracter = caracter;
            initAnimations(spriteJsonAttribute, caracterTextur);
            currentState = idle;
        }
        public void SwitchState()
        {
            Vector2 speed = caracter.GetSpeed();
            speed.X = Math.Abs(speed.X);
            speed.Y = Math.Abs(speed.Y);

            if (attack.Status == StateStatus.Running)
            {
                currentState = attack;
            }
            else if (speed.X < .1 && speed.Y < .1)
            {
                currentState = idle;
            } else if (speed.X < 1 || speed.X < 1)
            {
                currentState = walk;
            }

        }
        public void Update()
        {
            SwitchState();
            CurrentState.Update();
        }
        public void triggerAttack()
        {
            if(attack.Status == StateStatus.Terminated)
            {
                attack.Status = StateStatus.Running;
            }
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
