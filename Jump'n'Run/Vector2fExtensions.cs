using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics;
using Microsoft.Xna.Framework;

namespace JumpAndRun
{
    public static class Vector2fExtensions
    {
        public static SFML.System.Vector2f ToSf(this Vector2 v) { return new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(v.X), ConvertUnits.ToDisplayUnits(v.Y)); }
    }
}
