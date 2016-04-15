using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Common;
using Microsoft.Xna.Framework;



namespace Pong
{
    class Box2DTest : GameObject
    {

        CircleShape testShape;


        SFML.Graphics.CircleShape asd;

        public Box2DTest()
        {
            testShape = new CircleShape(10, 10);
            testShape.Position = new Vector2(100, 100);
        }
       

        void test()
        {
       
        }

    }
}
