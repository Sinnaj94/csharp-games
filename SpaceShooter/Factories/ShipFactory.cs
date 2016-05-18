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
            Ship s = JsonConvert.DeserializeObject<Ship>(File.ReadAllText(@"Resources\ships.json"));
            return s;
        }
    }
}
