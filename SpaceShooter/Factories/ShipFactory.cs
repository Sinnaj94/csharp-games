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

namespace SpaceShooter.Factories
{
    static class ShipFactory
    {
        // Builds a Ship Object from JSON Data, returns null if the Ship doesn't exist 
        public static Ship CreateShip(String name)
        {
            Dictionary<string, Ship> values = JsonConvert.DeserializeObject<Dictionary<string, Ship>>(File.ReadAllText(@"Resources\ships.json"));

            if (values.ContainsKey(name))
            {
                Console.WriteLine(name + " created with " + values[name].maxHP + "hp" + " and " + values[name].maxSpeed + " maximum Speed");
                return values[name];
            }
            Console.WriteLine("Ship with key " + name + " doesn't exist.");
            return null;
        }
    }
}
