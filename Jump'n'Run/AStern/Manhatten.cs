using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Extension for the solver to use Manhatten metric

namespace JumpAndRun
{
    public class Manhatten<TPathNode, TUserContext> : SpatialAStar<TPathNode,
    TUserContext> where TPathNode : IPathNode<TUserContext>
    {
        protected override Double Heuristic(PathNode inStart, PathNode inEnd)
        {
            return Math.Abs(inStart.X - inEnd.X) + Math.Abs(inStart.Y - inEnd.Y);
        }

        protected override Double NeighborDistance(PathNode inStart, PathNode inEnd)
        {
            return Heuristic(inStart, inEnd);
        }

        public Manhatten(TPathNode[,] inGrid)
            : base(inGrid)
        {
        }
    }
}
