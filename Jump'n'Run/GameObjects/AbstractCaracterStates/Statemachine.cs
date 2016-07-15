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
        State alternateAttack;
        State dead;
        State waiting;

        public void initAnimations(String spriteJsonAttribute, Texture caracterTexture)
        {
            SpriteBuilder _temp = new SpriteBuilder(spriteJsonAttribute);
            idle = new Idle(caracter, _temp.AnimationList.GetAnimation("idle", caracterTexture));
            walk = new Walk(caracter, _temp.AnimationList.GetAnimation("walk", caracterTexture));
            attack = new Attack(caracter, _temp.AnimationList.GetAnimation("attack", caracterTexture),0);
            dead = new Dead(caracter, _temp.AnimationList.GetAnimation("die", caracterTexture));
            alternateAttack = new Attack(caracter, _temp.AnimationList.GetAnimation("altattack", caracterTexture),1);
            waiting = new Waiting(caracter, _temp.AnimationList.GetAnimation("idle", caracterTexture));

            attack.Animation.SetSpeed(4);
            alternateAttack.Animation.SetSpeed(4);

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
            currentState = idle;
            if(caracter.isDead == true)
            {
                currentState = dead;
            } else if(caracter.isWaiting) {
                currentState = waiting;
            }
            else if (attack.Status == StateStatus.Running)
            {
                currentState = attack;
            }else if(alternateAttack.Status == StateStatus.Running)
            {
                currentState = alternateAttack;
            }
            else if (speed.X > .1 || speed.Y > .1)
            {
                currentState = walk;
                walk.Animation.SetSpeed(caracter.GetTotalSpeed()*4);
            }


        }
        public void Update()
        {
            SwitchState();
            CurrentState.Update();
        }
        public void triggerAttack(int nr)
        {

            if(caracter.BulletCount > 0)
            {
                nr = 1;
                caracter.BulletCount--;
            } else
            {
                nr = 0;
            }

            switch (nr)
            {
                case 0:
                    if (attack.Status == StateStatus.Terminated)
                    {
                        attack.Status = StateStatus.Running;
                    }
                    break;
                case 1:
                    if (alternateAttack.Status == StateStatus.Terminated)
                    {
                        alternateAttack.Status = StateStatus.Running;
                    }
                    break;
            }
            
        }

        public void setToWaiting() { CurrentState = waiting; }
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
