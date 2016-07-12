using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    class Player : AbstractCaracter
    {
        Vector2 positionChangedVector;

        public Player(Body body)
        {
            this.body = body;
            initAnimations("player", new Texture(@"Resources/Sprites/enemy1.png"));
            InitPhysics(@"Resources\physicsattributes.json");
            body.BodyType = BodyType.Dynamic;
            body.LinearVelocity = new Vector2(0, 0);
        }

        public event EventHandler onPositionChanged;

        protected virtual void PositionChanged(EventArgs e)
        {
            if (onPositionChanged != null) onPositionChanged(this, e);
        }

        public void CheckPositionChange()
        {
            if (Math.Abs(positionChangedVector.X - this.body.Position.X) > .5f || Math.Abs(positionChangedVector.Y - this.body.Position.Y) > .5f)
            {
                positionChangedVector = this.body.Position;
                PositionChanged(EventArgs.Empty);
            }
        }

        public override void updateExtension()
        {
            CheckPositionChange();
        }
    }
}
