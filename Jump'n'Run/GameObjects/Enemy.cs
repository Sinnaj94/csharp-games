using JumpAndRun;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    abstract class Enemy : GameObject, SFML.Graphics.Drawable
    {
        float damage;

        public void Kill()
        {
            //TODO
        }

        public void GivePlayerDamage(Player p)
        {
            
        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            //TODO: Machen
        }
    }

    class JumpingEnemy : Enemy
    {
        public JumpingEnemy(Body body)
        {
            //TODO: Make him Jump
            this.body = body;
            body.FixedRotation = true;
            body.Restitution = 1f;
            body.BodyType = BodyType.Dynamic;
        }
        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
