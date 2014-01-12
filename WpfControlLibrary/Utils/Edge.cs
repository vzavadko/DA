using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    [Serializable]
    public class Edge
    {

        #region Private fields

        #endregion

        #region Public Properties

        public ToothPoint StartPoint { get; set; }
        public ToothPoint EndPoint { get; set; }

        public int Distance { get; set; }

        #endregion

        #region Constructors

        public Edge()
        {
        }

        public Edge(ToothPoint from, ToothPoint to)
        {
            StartPoint = from;
            EndPoint = to;
            Distance = (int)Math.Sqrt(((EndPoint.X - StartPoint.X) * (EndPoint.X - StartPoint.X) + (EndPoint.Y - StartPoint.Y) * (EndPoint.Y - StartPoint.Y)));

        }

        #endregion

    }
}
