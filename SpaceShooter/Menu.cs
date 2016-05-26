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
    class Menu : IRenderable
    {
        //Clock is for timed events
        Clock c;
        String dataSetName;
        List<Button> buttonList;
        int selected;
        InputHandler input;
        List<MenuCommand> currentCommands;
        int milliseconds;
        bool active;
        RectangleShape background;
        Texture backgroundImage;
        Sprite backgroundSprite;

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
        public Menu()
        {
            active = true;
            milliseconds = 200;
            c = new Clock();
            input = new InputHandler();
            currentCommands = new List<MenuCommand>(10);
            dataSetName = "Main_Menu";
            DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\buttons.json"));
            DataTable dataTable = dataSet.Tables[dataSetName];
            Console.WriteLine(dataTable.Rows.Count);
            background = new RectangleShape(new Vector2f(1920,1080));
            background.FillColor = new Color(0, 0, 0, 255);
            backgroundImage = new Texture(@"Resources/mainscreen.png");
            backgroundSprite = new Sprite(backgroundImage);
            buttonList = new List<Button>();
            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine(row["name"]);
                try
                {
                    Button tempButton = new Button((String)row["name"], (double)row["x"], (double)row["y"]);
                    buttonList.Add(tempButton);
                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine(e.Data);
                }

            }
            selected = 0;
            buttonList[selected].Selected = true;
        }

        public Menu(String dataSet)
        {

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            background.Draw(target,states);
            backgroundSprite.Draw(target, states);
            foreach (Button b in buttonList)
            {
                b.Draw(target, states);
            }
        }

        public void Update()
        {
            currentCommands = input.HandleInputMenu();
            foreach (MenuCommand com in currentCommands)
            {
                com.Execute(this);
            }
            currentCommands.Clear();

        }

        public void navigateUp()
        {
            if (c.ElapsedTime.AsMilliseconds() >= milliseconds)
            {
                buttonList[selected].Selected = false;
                if (selected - 1 < 0)
                {
                    selected = buttonList.Count - 1;
                }
                else
                {
                    selected--;
                }
                buttonList[selected].Selected = true;
                c = new Clock();
            }

        }

        public void navigateDown()
        {
            if (c.ElapsedTime.AsMilliseconds() >= milliseconds)
            {
                buttonList[selected].Selected = false;
                if (selected + 1 >= buttonList.Count)
                {
                    selected = 0;
                }
                else
                {
                    selected++;
                }
                buttonList[selected].Selected = true;
                c = new Clock();
            }

        }

        public void selectCurrent()
        {
            if(selected == 0)
            {
                Active = false;
            }
        }


    }
}
