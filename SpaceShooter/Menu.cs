using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceShooter.GameObjects;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using SFML.Graphics;

namespace SpaceShooter
{
    class Menu : IRenderable
    {
        String dataSetName;
        List<Button> buttonList;
        /// <summary>
        ///Constructs a new Menu out of the json file "buttons.json" in the Resources Folder. Currently only works with the Main_Menu array.
        /// </summary>
        public Menu()
        {
            dataSetName = "Main_Menu";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\buttons.json"));
            DataTable dataTable = dataSet.Tables[dataSetName];
            Console.WriteLine(dataTable.Rows.Count);
            buttonList = new List<Button>();
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["name"]);
                try
                {
                    Button tempButton = new Button((String)row["name"], (double)row["x"], (double)row["y"]);
                    buttonList.Add(tempButton);
                }
                catch(InvalidCastException e)
                {
                    Console.Out.WriteLine(e.Data);
                }

            }
        }

        public Menu(String dataSet)
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            foreach(Button b in buttonList)
            {
                b.Draw(target,states);
            }
        }
    }
}
