using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Schema;
using System.Data;
using FarseerPhysics;
using FarseerPhysics.Factories;
using FarseerPhysics.Dynamics;

namespace SpaceShooter.Factories
{
    static class ShipFactory
    {
        // Builds a Ship Object from JSON Data, returns null if the Ship doesn't exist 
        public static Ship CreateShip(String name, double x, double y, World world)
        {
            Dictionary<string, Ship> values = JsonConvert.DeserializeObject<Dictionary<string, Ship>>(File.ReadAllText(@"Resources\ships.json"));

            if (values.ContainsKey(name))
            {
                Console.WriteLine(name + " created with " + values[name].maxHP + "hp" + " and " + values[name].maxSpeed + " maximum Speed");
                Ship s = values[name];
                s.initSprite();
                s.world = world;
                // Creates Coll. Box with sprite bounds
                s.body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(s.SpriteBounds[1] - s.SpriteBounds[0]), ConvertUnits.ToSimUnits(s.SpriteBounds[3] - s.SpriteBounds[2]), 10f);
                s.body.Position = new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToSimUnits(x), ConvertUnits.ToSimUnits(y));
                s.body.BodyType = BodyType.Dynamic;
                s.init();
                return s;
            }
            Console.WriteLine("Ship with key " + name + " doesn't exist.");
            return null;
        }
    }
}

