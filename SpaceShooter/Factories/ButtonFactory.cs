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
    static class ButtonFactory
    {
        // Builds a Ship Object from JSON Data, returns null if the Ship doesn't exist 
        public static List<Button> CreateButtons()
        {
            List<Button> buttonList = new List<Button>();

            Dictionary<string, Button> values = JsonConvert.DeserializeObject<Dictionary<string, Button>>(File.ReadAllText(@"Resources\buttons.json"));
            
            return buttonList;
            
        }
    }
}
