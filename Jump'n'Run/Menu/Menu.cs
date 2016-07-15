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

    class Menu : AbstractNavigation, SFML.Graphics.Drawable
    {
        bool active;
        //Clock is for timed events

        String dataSetName;
        List<Button> buttonList;
        int selected;
        //ManageMenu manager;



        public int Selected
        {
            get
            {
                return selected;
            }

            set
            {
                selected = value;
            }
        }

        internal List<Button> ButtonList
        {
            get
            {
                return buttonList;
            }

            set
            {
                buttonList = value;
            }
        }

        public override bool Active
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
        public Menu()
        {




        }

        /// <summary>
        /// Function to initialize the Menu.
        /// </summary>
        /// <param name="name"></param>
        public void Init(String name)
        {
            dataSetName = name;
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources/json/buttons.json"));
            DataTable dataTable = dataSet.Tables[dataSetName];
            ButtonList = new List<Button>();
            Active = true;
            foreach (DataRow row in dataTable.Rows)
            {

                try
                {
                    Button tempButton = new Button((String)row["name"], (double)row["x"], (double)row["y"]);
                    ButtonList.Add(tempButton);
                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine(e.Data);
                }

            }
            Selected = 0;
            ButtonList[Selected].Selected = true;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            //background.Draw(target, states);

            foreach (Button b in ButtonList)
            {
                b.Draw(target, states);
            }
        }



        public override void NavigateUp()
        {
            ButtonList[Selected].Selected = false;
            if (Selected - 1 < 0)
            {
                Selected = ButtonList.Count - 1;
            }
            else
            {
                Selected--;
            }
            ButtonList[Selected].Selected = true;
            //ManageSound.Instance.select();
        }

        public override void NavigateDown()
        {
            ButtonList[Selected].Selected = false;
            if (Selected + 1 >= ButtonList.Count)
            {
                Selected = 0;
            }
            else
            {
                Selected++;
            }
            ButtonList[Selected].Selected = true;
            //ManageSound.Instance.select();
        }

        public override void Enter()
        {
            //ManageSound.Instance.enter();
        }
    }

    class MainMenu : Menu
    {
        public MainMenu()
        {
            Init("Main_Menu");
        }
        public override void Enter()
        {
            base.Enter();
            switch (Selected)
            {
                case 0:
                    Active = false;
                    //ManageSound.Instance.StartPlayingMusic();
                    break;
                case 1:
                    //TODO
                    break;
                case 2:
                    System.Environment.Exit(0);
                    break;

                    
            }

        }
    }

}
