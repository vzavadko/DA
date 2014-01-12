using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;

namespace Utils
{
    [Serializable]
    public class ToothPoint: IPoint
    {
        #region constants

        public const int RADIUS = 4;

        #endregion

        #region Private fields

        #endregion

        #region Public properties

        public int X { get; set; }
        public int Y { get; set; }
        public int OrderNumber { get; set; }
        [XmlIgnoreAttribute]
        public List<Edge> Edges { get; set; }

        #endregion

        #region Overrided methods

        public override string ToString()
        {
            return OrderNumber.ToString();            
        }

        public override int GetHashCode()
        {
            return OrderNumber;
        }

        #endregion

        #region Public methods     

        public Edge GetJoinEdge(ToothPoint point)
        {
            foreach (Edge item in this.Edges)
            {
                if (item.StartPoint.OrderNumber == this.OrderNumber && item.EndPoint.OrderNumber == point.OrderNumber)
                    return item;
                if (item.EndPoint.OrderNumber == this.OrderNumber && item.StartPoint.OrderNumber == point.OrderNumber)
                    return item;
            }
            return null;
        }

        public System.Windows.Point GetPoint()
        {
            return new System.Windows.Point(this.X, this.Y);
        }
        #endregion

        #region Public constructors
        public ToothPoint()
        {
            Edges = new List<Edge>();
        }

        public ToothPoint(Point p): base()
        {
            this.X = p.X;
            this.Y = p.Y;
        }
        #endregion
    }
}
