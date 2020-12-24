using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms
{
    public class NodePoint
    {
        public List<(int, double)> Points { get; set; }

        public NodePoint(List<(int, double)> PointList = null)
        {
            Points = PointList is null ? new List<(int, double)>() : PointList;
        }
    }
}
