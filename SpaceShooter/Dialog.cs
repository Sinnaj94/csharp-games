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
using SFML.System;
namespace SpaceShooter
{
    class Dialog : IRenderable
    {
        //Clock is for timed events
        Clock c;
        String dataSetName;
        List<DialogElement> dialogList;

        InputHandler input;
        List<MenuCommand> currentCommands;
        int milliseconds;
        bool active;

        public bool Active
        {
            get
            {
                return active;
            }

            set
            {
                active = value;
            }
        }

        /// <summary>
        ///Constructs a new Menu out of the json file "buttons.json" in the Resources Folder. Currently only works with the Main_Menu array.
        /// </summary>
        public Dialog(String dataSetName)
        {
            active = true;
            milliseconds = 200;
            c = new Clock();
            input = new InputHandler();
            currentCommands = new List<MenuCommand>(10);
            this.dataSetName = dataSetName;
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\dialogs.json"));
            DataTable dataTable = dataSet.Tables[dataSetName];

            dialogList = new List<DialogElement>();
            foreach (DataRow row in dataTable.Rows)
            {

                try
                {
                    //Button tempButton = new Button((String)row["name"], (double)row["x"], (double)row["y"]);
                    //buttonList.Add(tempButton);
                    String currentSpeaker = (String)row["speaker"];
                    String currentText = (String)row["text"];

                    if (currentSpeaker != "none")
                    {
                        DialogElement temp = new DialogElement(currentSpeaker,currentText);
                        dialogList.Add(temp);
                    }
                    else
                    {

                    }
                    
                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine(e.Data);
                }

            }

        }



        public void Draw(RenderTarget target, RenderStates states)
        {
            dialogList[0].Draw(target, states);
            
        }

        public void Update()
        {
            currentCommands = input.HandleInputMenu();
            
        }

        

        public void selectCurrent()
        {
            if (c.ElapsedTime.AsMilliseconds() >= milliseconds)
            {
                
                ManageSound.Instance.enter();
                c = new Clock();
            }

        }


    }
}
