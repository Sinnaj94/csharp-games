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
    class Dialog : SFML.Graphics.Drawable
    {
        //Clock is for timed events
        Clock c;
        String dataSetName;
        List<DialogElement> dialogList;

        int milliseconds;
        bool active;

        int currentDialog;
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

            this.dataSetName = dataSetName;
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\json\dialogs.json"));
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
                    String currentPlane = (String)row["plane"];
                    String currentScene = (String)row["scene"];
                    if (currentSpeaker != "none")
                    {
                        DialogElement temp = new DialogElement(currentSpeaker,currentText,currentPlane,currentScene);
                        dialogList.Add(temp);
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
            Console.WriteLine(target.GetView().Center);
            Vector2f defaultSize = target.DefaultView.Size;
            target.SetView(new View(new Vector2f(defaultSize.X/2,defaultSize.Y/2),defaultSize));
            dialogList[currentDialog].Draw(target, states);
            
        }

        public void Update()
        {
            dialogList[currentDialog].Update();
        }


        

        public void selectCurrent()
        {
            if (c.ElapsedTime.AsMilliseconds() >= milliseconds)
            {
                if (dialogList[currentDialog].textDone())
                {
                    if (currentDialog + 1 < dialogList.Count)
                    {
                        currentDialog++;
                    }
                    else
                    {
                        Active = false;
                    }
                }
                else
                {
                    dialogList[currentDialog].showAllText();
                }
                
                c = new Clock();
            }

        }


    }
}
