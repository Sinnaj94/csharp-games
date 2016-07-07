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
        String name;
        int startColumn;
        int columnSize;
        Animation returnedAnimation;



        public SpriteBuilder(String name)
        {
            Init(name);
        }
        public void Init(String databaseName)
        {
            dataSetName = databaseName;

            //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\Sprite.json"));
            String json = File.ReadAllText(@"Resources\animations.json");
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(json);
            DataTable dataTable = dataSet.Tables[databaseName];
            foreach (DataRow row in dataTable.Rows)
            {

                try
                {
                    startColumn = (int)(Int64)row["startColumn"];
                    columnSize = (int)(Int64)row["columnSize"];
                    name = (String)row["name"];
                    returnedAnimation = new Animation(name, startColumn, columnSize);

                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine("There was a cast error. Output: " + e.Data);
                    break;
                }
            }
        }

        internal Animation ReturnedAnimation
        {
            get
            {
                return returnedAnimation;
            }

            set
            {
                returnedAnimation = value;
            }
        }


    }
    class Animation
    {
        public Animation(string name, int startColumn, int columnSize)
        {
            this.name = name;
            this.startColumn = startColumn;
            this.columnSize = columnSize;
            //Output.Instance.print("Animation " + name + " created. Startcolumn: " + startColumn + ", Columnsize: " + columnSize);

        }
        public string name { get; set; }
        public int startColumn { get; set; }
        public int columnSize { get; set; }



    }

}
