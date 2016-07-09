using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JumpAndRun
{
    /// <summary>
    /// Defines the parameters which will be used to find a path across a section of the map
    /// </summary>
    public class SearchParameters
    {
        public Point StartLocation { get; set; }

        public Point EndLocation { get; set; }
        
        public bool[,] Map { get; set; }

        public SearchParameters(Point startLocation, Point endLocation, Map map)
        {
            this.StartLocation = startLocation;
            this.EndLocation = endLocation;
            Map = new bool[100,100];
            foreach(Tile t in map.TileArray)
            {
                this.Map[t.X, t.Y] = !t.IsCollidable;
            }
        }
    }
}
