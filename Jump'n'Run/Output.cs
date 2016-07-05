using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    public class Output
    {
        private static Output instance;

        public static Output Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Output();
                }
                return instance;

            }
        }

        public void print(String output)
        {
            Console.Out.WriteLine(output);
        }

    }
}
