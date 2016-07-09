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
        AnimationManager animationList;

        internal AnimationManager AnimationList
        {
            get
            {
                return animationList;
            }

            set
            {
                animationList = value;
            }
        }

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
            //Create an Animation Manager
            AnimationList = new AnimationManager();
            foreach (DataRow row in dataTable.Rows)
            {

                try
                {
                    startColumn = (int)(Int64)row["startColumn"];
                    columnSize = (int)(Int64)row["columnSize"];
                    name = (String)row["name"];
                    Animation _temp = new Animation(name, startColumn, columnSize);
                    AnimationList.Add(_temp);

                }
                catch (InvalidCastException e)
                {
                    Console.Out.WriteLine("There was a cast error. Output: " + e.Data);
                    break;
                }
            }
        }


    }

    class PhysicsBuilder
    {
        String dataSetName;
        PhysicsSettings _physicsReturn;

        public PhysicsBuilder(String databaseName)
        {
            Init(databaseName);
        }

        internal PhysicsSettings PhysicsReturn
        {
            get
            {
                return _physicsReturn;
            }

            set
            {
                _physicsReturn = value;
            }
        }

        public void Init(String databaseName)
        {
            dataSetName = databaseName;

            //DataSet dataSet = JsonConvert.DeserializeObject<DataSet>(File.ReadAllText(@"Resources\Sprite.json"));
            String json = File.ReadAllText(databaseName);
            PhysicsReturn = JsonConvert.DeserializeObject<PhysicsSettings>(json);


        }
    }

    class PhysicsSettings
    {
        public int mass { get; set; }
        public int acceleration { get; set; }
        public int maxSpeed { get; set; }
        public int jumpStrength { get; set; }
        public int friction { get; set; }
    }

    class AnimationManager
    {
        List<Animation> animationList;
        public AnimationManager()
        {
            animationList = new List<Animation>();
        }

        public void Add(Animation _animation)
        {
            animationList.Add(_animation);
        }

        public Animation getAnimation(String animationName, Texture texture)
        {
            Animation workingAnimation = searchAnimation(animationName);
            workingAnimation.getAnimation(texture);
            return workingAnimation;
        }

        private Animation searchAnimation(String animationName)
        {
            foreach (Animation _a in animationList)
            {
                if (animationName == _a.name)
                {

                    return _a;
                }
            }
            //TODO: throw an exception
            return null;
        }
    }



    class Animation
    {

        public string name { get; set; }
        public int startColumn { get; set; }
        public int columnSize { get; set; }
        Vector2u sheetSize;
        Clock c;
        public List<IntRect> RectangleList
        {
            get
            {
                return rectangleList;
            }

            set
            {
                rectangleList = value;
            }
        }

        private int rowSize;
        private List<IntRect> rectangleList;
        int size;
        int currentFrame;
        Time frameChangeTime;

        public Animation(string name, int startColumn, int columnSize)
        {
            this.name = name;
            this.startColumn = startColumn;
            this.columnSize = columnSize;
            //Output.Instance.print("Animation " + name + " created. Startcolumn: " + startColumn + ", Columnsize: " + columnSize);
            rowSize = 6;
            RectangleList = new List<IntRect>();
            sheetSize = new Vector2u(8, 9);
            c = new Clock();
            currentFrame = 0;
            frameChangeTime = Time.FromMilliseconds(100);
        }



        public List<IntRect> getAnimation(Texture texture)
        {

            //TODO: Outsourcing
            //Go through X

            for (int c = startColumn; c < startColumn + columnSize - 1; c++)
            {

                //Go thorugh Y
                for (int r = 0; r < rowSize; r++)
                {


                    RectangleList.Add(getRect(r, c, texture));
                }
            }
            size = RectangleList.Count;
            return RectangleList;

        }

        private IntRect getRect(int x, int y, Texture texture)
        {
            IntRect _temp = new IntRect((int)(x * texture.Size.X / sheetSize.X), (int)(y* texture.Size.Y / sheetSize.Y), (int)(texture.Size.X / sheetSize.X ), (int)(texture.Size.Y / sheetSize.Y ));
            Console.Out.WriteLine(" " + _temp);
            return _temp;
        }

        public IntRect animate()
        {
            if(c.ElapsedTime > frameChangeTime)
            {
                changeFrame();
                c = new Clock();
            }
            return rectangleList[currentFrame];
        }

        private void changeFrame()
        {
            if(currentFrame + 1 < size)
            {
                currentFrame++;
            }
            else
            {
                currentFrame = 0;
            }
        }
    }

}
