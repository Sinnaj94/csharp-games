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

        AnimationManager animationList;
        private int columnSize;

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
                    int startRow = (int)(Int64)row["startRow"];
                    int startColumn = (int)(Int64)row["startColumn"];
                    int endRow = (int)(Int64)row["endRow"];
                    int endColumn = (int)(Int64)row["endColumn"];

                    name = (String)row["name"];
                    bool playOnce = (bool)row["playOnce"];
                    int pixelSize = (int)(Int64)row["pixelSize"];
                    //Pixel offset is an optional value
                    Vector2i pixelOffset = new Vector2i((int)(Int64)row["pixelLeft"], (int)(Int64)row["pixelAbove"]);
                    Animation _temp = new Animation(name, startRow, startColumn, endRow, endColumn, playOnce,pixelSize,pixelOffset);
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
        public float acceleration { get; set; }
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

        public Animation GetAnimation(String animationName, Texture texture)
        {
            Animation workingAnimation = SearchAnimation(animationName);
            workingAnimation.GetAnimation(texture);
            return workingAnimation;
        }

        private Animation SearchAnimation(String animationName)
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
        public int startRow { get; set; }
        public int startColumn { get; set; }
        public int endRow { get; set; }
        public int endColumn { get; set; }
        public bool playOnce { get; set; }

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

        public int PixelSize
        {
            get
            {
                return pixelSize;
            }

            set
            {
                pixelSize = value;
            }
        }

        private int rowSize;
        private List<IntRect> rectangleList;
        int size;
        int currentFrame;
        Time frameChangeTime;
        int pixelSize;
        Vector2i pixelOffset;
        public Animation(String name, int startRow, int startColumn, int endRow, int endColumn, bool playOnce,int pixelSize, Vector2i pixelOffset)
        {
            this.name = name;
            this.startRow = startRow;
            this.startColumn = startColumn;
            this.endRow = endRow;
            this.endColumn = endColumn;
            this.playOnce = playOnce;
            this.PixelSize = pixelSize;
            this.pixelOffset = pixelOffset;
            rowSize = 6;
            RectangleList = new List<IntRect>();
            sheetSize = new Vector2u(8, 9);
            c = new Clock();
            currentFrame = 0;
            frameChangeTime = Time.FromMilliseconds(200);


        }



        public List<IntRect> GetAnimation(Texture texture)
        {

            //TODO: Outsourcing
            //Go through Columns (Y)
            if (startColumn == endColumn)
            {
                for (int r = startRow; r <= endRow; r++)
                {
                    RectangleList.Add(GetRect(r, startColumn, texture));
                }
            }
            else
            {
                for (int c = startColumn; c < sheetSize.Y; c++)
                {
                    //Go thorugh Y
                    for (int r = 0; r < sheetSize.X; r++)
                    {

                        if (c == startColumn)
                        {
                            if (r >= startRow)
                            {
                                RectangleList.Add(GetRect(r, c, texture));
                            }
                        }
                        else if (c == endColumn)
                        {
                            if (r <= endRow)
                            {
                                RectangleList.Add(GetRect(r, c, texture));
                            }
                        }
                        else if (c > startColumn && c < endColumn)
                        {
                            RectangleList.Add(GetRect(r, c, texture));
                        }

                    }
                }
            }

            size = RectangleList.Count;
            return RectangleList;

        }

        private IntRect GetRect(int x, int y, Texture texture)
        {
            IntRect _temp = new IntRect(x * PixelSize +pixelOffset.X, y * PixelSize + pixelOffset.Y, PixelSize, PixelSize);


            return _temp;
        }

        public void Restart()
        {
            currentFrame = 0;
        }

        public IntRect Animate()
        {
            if (c.ElapsedTime > frameChangeTime)
            {
                ChangeFrame();
                c = new Clock();
            }
            return rectangleList[currentFrame];
        }

        public void SetSpeed(float factor) {
            frameChangeTime = Time.FromMilliseconds((int)(200/factor));
        }



        private void ChangeFrame()
        {
            if (currentFrame + 1 < size)
            {
                currentFrame++;
            }
            else
            {
                if (!playOnce)
                {

                    currentFrame = 0;
                }
            }
        }


    }

}
