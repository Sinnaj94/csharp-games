using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using SFML.System;

namespace JumpAndRun
{
    abstract class AbstractPhysicsObject : GameObject, SFML.Graphics.Drawable
    {
        Sprite sprite;

        public Sprite Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;
            }
        }

        public void Init(Body b)
        {
            body = b;
            body.Position = new Vector2(ConvertUnits.ToSimUnits(200), ConvertUnits.ToSimUnits(128));

        }
        public void Draw(RenderTarget target, RenderStates states)
        {
            Sprite.Origin = new Vector2f(Sprite.Texture.Size.X/2, Sprite.Texture.Size.Y/2);
            Sprite.Rotation = body.Rotation*180/(float)Math.PI;

            Sprite.Position = Vector2fExtensions.toVector2f(body.Position);

            Sprite.Draw(target, states);
        }
    }
}
