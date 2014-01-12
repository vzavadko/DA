using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace Utils
{
    [Serializable]
    public class ToothPolygon
    {
        #region Constants
        public int MaxPoligonPointsNumber;
        #endregion

        #region Public propeties

        //public Dictionary<Point, List<int>> Points { get; set; }
        public List<ToothPoint> Points { get; set; }
        [XmlIgnoreAttribute]
        public Shape LastSetEdge { get; set; }

        public bool IsClosed {
            get {
                return Points.Count == MaxPoligonPointsNumber;
            }
        }

        #endregion

        #region Constructors

        public ToothPolygon()
        {
            Points = new List<ToothPoint>();
        }

        #endregion

        #region Public methods

        public IPoint GetPointByCoordinate(int x, int y)
        {
            IPoint result = (from point in this.Points where point.X == x && point.Y == y select point).SingleOrDefault();
            return result;
        }

        public IPoint GetPointByOrderNumber(int orderNubmer)
        {
            IPoint result = (from point in this.Points where point.OrderNumber == orderNubmer select point).SingleOrDefault();
            return result;
        }

        public bool AreTopsJoined(IPoint start, IPoint end)
        {

            foreach (Edge item in start.Edges)
            {
                if (item.StartPoint.OrderNumber == start.OrderNumber && item.EndPoint.OrderNumber == end.OrderNumber)
                    return true;
                if (item.EndPoint.OrderNumber == start.OrderNumber && item.StartPoint.OrderNumber == end.OrderNumber)
                    return true;
            }
            return false;
        }

        public void CancelFollowingPoints(int activePoint)
        {
            //Delete edges leading to removed nodes
            Edge e = (from c in Points[activePoint - 1].Edges
                      where c.EndPoint.OrderNumber == activePoint
                      select c).SingleOrDefault();
            Points[activePoint - 1].Edges.Remove(e);

            //if (Points[activePoint - 1].Edges.Count > 0 && 
            //    Points[activePoint - 1].Edges[0].EndPoint.OrderNumber == activePoint)
            //{
            //    Points[activePoint - 1].Edges.RemoveAt(0);
            //}

            //Delete edges outgoing from previous node 
            if (activePoint > 1)
            {
                e = (from c in Points[activePoint - 2].Edges
                     where c.EndPoint.OrderNumber == activePoint
                     select c).SingleOrDefault();
                Points[activePoint - 2].Edges.Remove(e);
            }

            //if (Points[activePoint - 2].Edges[1] != null &&
            //    Points[activePoint - 2].Edges[1].EndPoint.OrderNumber == activePoint)
            //{
            //    Points[activePoint - 2].Edges.RemoveAt(1);
            //}

            if (Points.Count == MaxPoligonPointsNumber)
            {
                e = (from c in Points[0].Edges
                          where c.EndPoint.OrderNumber == 1 &&
                          c.StartPoint.OrderNumber == MaxPoligonPointsNumber select c).SingleOrDefault();
                if (e != null)
                {
                    Points[0].Edges.Remove(e);
                }
            }

            Points.RemoveRange(activePoint - 1, Points.Count + 1 - activePoint);

        }

        public void DrawGraph(Action<ToothPoint> pointDrawingHandler, Action<Edge, bool> edgeDrawindHandler)
        {
            List<Edge> drawedEdges = new List<Edge>();
            for (int i = 0; i < Points.Count; i++ )
            {                
                pointDrawingHandler(Points[i]);
                foreach (Edge edgeItem in Points[i].Edges)
                {
                    Edge e = (from c in drawedEdges
                              where c.StartPoint.OrderNumber == edgeItem.StartPoint.OrderNumber &&
                              c.EndPoint.OrderNumber == edgeItem.EndPoint.OrderNumber select c).SingleOrDefault();
                    if (e == null)
                    {
                        edgeDrawindHandler(edgeItem, Points.Count == MaxPoligonPointsNumber);
                    }
                }
            }
        }

        public Polygon GetPolygon()
        {
            Polygon p = new Polygon();
            for (int i = 0; i < this.Points.Count; i++)
            {
                p.Points.Add(this.Points[i].GetPoint());
            }
            return p;
        }
        #endregion

        #region Operators



        #endregion
    }
}
