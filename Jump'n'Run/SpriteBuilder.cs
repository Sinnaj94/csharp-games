using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using SFML.Graphics;
using SFML.System;
namespace JumpAndRun
{
    class SpriteBuilder
    {
        String dataSetName;

        public SpriteBuilder(String name)
        {
            Init(name);
        }
        public void Init(String name)
        {
            dataSetName = name;

            //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\Sprite.json"));
            String json = File.ReadAllText(@"Resources\animations.json");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
            
            DataTable dataTable = dataSet.Tables["animations"];
            foreach (DataRow row in dataTable.Rows)
            {

                try
                {
                    Output.Instance.print((String)row["name"]);
                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine(e.Data);
                }

            }
            
        }
    }
    class Animation
    {
        public string name { get; set; }
        public int startColumn { get; set; }
        public int columnSize { get; set; }
        


    }

}
