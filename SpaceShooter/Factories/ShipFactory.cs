using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using Newtonsoft.Json;


namespace SpaceShooter.Factories
{
    static class ShipFactory
    {

        

        public static Ship CreateShip(String name)
        {



            // TODO: Move to file
            string json = @"{
            'Name': 'Falcon',
            'maxHP' : 1,
            'MaxSpeed' : 2
            }";

            Ship s = JsonConvert.DeserializeObject<Ship>(json);


            return null;
        }

    }
}
