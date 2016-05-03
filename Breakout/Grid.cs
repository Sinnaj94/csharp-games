using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Breakout.GameObjects;
using System.IO;

namespace Breakout
{
    class Grid : Drawable
    {
        Box[,] array2D;
        Vector2f windowSize;
        Vector2i arraySize;
        bool allGone;
        List<Item> itemList;

        

      //  public Grid(Vector2f windowSize, List<Item> itemList);
        ScoreBoard board;

        public Grid(Vector2f windowSize, ScoreBoard scoreBoard)
        {
            array2D = new Box[10, 5];
            this.windowSize = windowSize;
            buildMap(@"Levels/Level1.txt");
            this.board = scoreBoard;
            AllGone = false;
        }
        public Grid(Vector2f windowSize, ScoreBoard scoreBoard, List<Item> itemList)
        {
            array2D = new Box[10, 5];
            this.windowSize = windowSize;
            buildMap(@"Levels/Level1.txt");
            this.board = scoreBoard;
            this.itemList = itemList;
        }

        private void buildMap(String textfile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(textfile))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;
                    int index = 0;
                    arraySize = defineArraySize(textfile);
                    array2D = new Box[arraySize.X, arraySize.Y];
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        for(int i = 0; i < line.Length; i++)
                        {
                            char cChar = line[i];
                            if(cChar == '0')
                            {
                                array2D[i, index] = null;
                            }
                            else
                            {
                                createBox(i, index,cChar);
                            }
                        }
                        index++;

                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Was not able to read the file.");
            }
        }

        private void createBox(int i, int j,char type)
        {
            //Die heilige Formel // wat? :D
            array2D[i, j] = new Box(new Vector2f(windowSize.X / (arraySize.X) * i + windowSize.X / (arraySize.X + 2), windowSize.Y*2/6 / (arraySize.Y + 2) * j + windowSize.Y*2/6 / (arraySize.Y+2)), new Vector2f(windowSize.X / (arraySize.X+2), windowSize.Y*2/6 / (arraySize.Y+2)),type);
            Vector2f pos = new Vector2f(i*windowSize.X/arraySize.X,(j*windowSize.Y/arraySize.Y)*2/3);
            Vector2f size = new Vector2f(windowSize.X/arraySize.X-1,( windowSize.Y/arraySize.Y)*2/3-1);
            array2D[i, j] = new Box(pos,size, type);
        }

        private Vector2i defineArraySize(String textfile)
        {
            try
            {
                using (StreamReader sr = new StreamReader(textfile))
                {
                    // Read the stream to a string, and write the string to the console.
                    string line;
                    int maxX = 0;
                    int maxY = 0;
                    while ((line = sr.ReadLine()) != null)
                    {

                        if(line.Length > maxX)
                        {
                            maxX = line.Length;
                        }

                        maxY++;


                    }
                    return new Vector2i(maxX, maxY);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Was not able to read the file.");
                return new Vector2i(0,0);
            }
        }

        public bool AllBlocksGone()
        {
            foreach(Box a in array2D)
            {
                if (a != null)
                {
                    if(!a.IsItem && !a.IsSolid)
                    {
                        AllGone = false;
                        return false;
                    }
                }
            }
            AllGone = true;
            return true;
        }

        public Vector2f CollideWithBlock(CircleShape shape)
        {
            for (int i = 0; i < arraySize.X; i++)
            {
                for (int j = 0; j < arraySize.Y; j++)
                {
                    if (array2D[i, j] != null)
                    {
                        Vector2f tmp = ManagerCollision.Instance.collideWithDirection(array2D[i, j].BoxShape, shape);
                        if (tmp.X == -1 || tmp.Y == -1){
                            if (array2D[i, j].IsItem)
                            {
                                Item _tempItem = new Item(windowSize, array2D[i, j].Position, array2D[i, j].Size);
                                ItemList.Add(_tempItem);
                            }
                            if (array2D[i, j].destroyBox())
                            {
                                array2D[i, j] = null;
                                board.Score++;
                            }

                            return tmp;
                        }                               
                    }
                }
            }
            return new Vector2f(1,1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            board.Draw(target, states);

            for (int i = 0; i < arraySize.X; i++)
            {
                for (int j = 0; j < arraySize.Y; j++)
                {
                    if(array2D[i, j] != null)
                    {
                        array2D[i, j].Draw(target, states);
                    }
                }
            }
        }

        internal List<Item> ItemList
        {
            get
            {
                return itemList;
            }

            set
            {
                itemList = value;
            }
        }

        public bool AllGone
        {
            get
            {
                return allGone;
            }

            set
            {
                allGone = value;
            }
        }
    }
}
