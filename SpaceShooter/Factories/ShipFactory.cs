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
using Microsoft.Xna.Framework;

namespace SpaceShooter.Factories
{
    static class ShipFactory
    {
        public static int GetShipUpgradePrice(String name)
        {
            Dictionary<string, Ship> values = JsonConvert.DeserializeObject<Dictionary<string, Ship>>(File.ReadAllText(@"Resources\ships.json"));
            if (values.ContainsKey(name))
            {
                Ship s = values[name];
                Console.WriteLine("Ship upgrade costs: " + s.price);
                return s.price;
            }
            Console.WriteLine("Ship with key " + name + " doesn't exist.");
            return 0;
        }


        public static Ship CreateRandomShip(World world)
        {
            Dictionary<string, Ship> values = JsonConvert.DeserializeObject<Dictionary<string, Ship>>(File.ReadAllText(@"Resources\ships.json"));
            List<String> sarray = values.Keys.ToList<String>();


            //Console.WriteLine(sarray[0].ToString());
            return CreateShip(sarray[new Random().Next(sarray.Count)].ToString(), 2000, new Random().NextDouble() * 1080, world);
            //return null;
        }

        public static Ship UpgradeShip(World world)
        {
            Dictionary<string, Ship> values = JsonConvert.DeserializeObject<Dictionary<string, Ship>>(File.ReadAllText(@"Resources\ships.json"));
            List<String> sarray = values.Keys.ToList<String>();
           // sarray.FindIndex("asd");
            //Console.WriteLine(sarray[0].ToString());
            return CreateShip(sarray[new Random().Next(sarray.Count)].ToString(), 2000, new Random().NextDouble() * 1080, world);
            //return null;
        }

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
                Console.WriteLine("Xmin: " + s.SpriteBounds[0] + "XMax: " + s.SpriteBounds[1] + "YMin: " + s.SpriteBounds[2] + "YMax: " + s.SpriteBounds[3]);
                // Creates Coll. Box with sprite bounds
                s.body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(s.SpriteBounds[1] - s.SpriteBounds[0]), ConvertUnits.ToSimUnits(s.SpriteBounds[3] - s.SpriteBounds[2]), 10f);
                s.body.Position = new Microsoft.Xna.Framework.Vector2(ConvertUnits.ToSimUnits(x), ConvertUnits.ToSimUnits(y));
                s.body.BodyType = BodyType.Dynamic;
                s.body.Rotation = 0;
                s.init();
                return s;
            }
            Console.WriteLine("Ship with key " + name + " doesn't exist.");
            return null;
        }
    }
}

