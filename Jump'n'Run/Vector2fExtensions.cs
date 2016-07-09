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
        //public static SFML.System.Vector2f ToVector2f(this Vector2 v) { return new SFML.System.Vector2f(v.X, v.Y);}
        public static SFML.System.Vector2f ToVector2f(this SFML.System.Vector2u v) { return new SFML.System.Vector2f(v.X, v.Y); }
        public static Vector2 ToSimVector(this SFML.System.Vector2f v) {return new Vector2(ConvertUnits.ToSimUnits(v.X), ConvertUnits.ToSimUnits(v.Y)); }
        public static SFML.System.Vector2f toVector2f(Vector2 _in)
        {
            return new SFML.System.Vector2f(ConvertUnits.ToDisplayUnits(_in.X), ConvertUnits.ToDisplayUnits(_in.Y ));
        }
    }
}
