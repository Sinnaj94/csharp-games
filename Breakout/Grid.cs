using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using Pong.GameObjects;
using System.IO;

namespace Pong
{
    class Grid : Drawable
    {
        Box[,] array2D;
        Vector2f windowSize;
        Vector2i arraySize;
        public Grid(Vector2f windowSize)
        {
            array2D = new Box[10, 5];
            this.windowSize = windowSize;
            
            buildMap(@"Levels/Level1.txt");
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
                                createBox(i, index);
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

        private void createBox(int i, int j)
        {
            //Die heilige Formel
            array2D[i, j] = new Box(new Vector2f(windowSize.X / (arraySize.X+2) * i + windowSize.X / (arraySize.X + 2), windowSize.Y*2/3 / (arraySize.Y + 2) * j + windowSize.Y*2/3 / (arraySize.Y+2)), new Vector2f(windowSize.X / (arraySize.X+2), windowSize.Y*2/3 / (arraySize.Y+2)));

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
                            array2D[i, j] = null;
                            return tmp;
                        }                               
                    }
                }
            }
            return new Vector2f(1,1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
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
    }
}
