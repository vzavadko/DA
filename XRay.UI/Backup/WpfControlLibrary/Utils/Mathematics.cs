using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Diagnostics;
//using System.Drawing;
//using System.Windows.Media;

namespace Utils
{
    public class LineEquationNumbers
    {
        //y = kx + b
        public double k;
        public double b;
    }

    public class Mathematics
    {
        public static int FindShortestDistanceBetweenPointandPolyghon(Point point, Polygon polygon)
        {
            System.Drawing.Drawing2D.GraphicsPath gp = new System.Drawing.Drawing2D.GraphicsPath();
            System.Drawing.Point[] sdp = new System.Drawing.Point[polygon.Points.Count];
            for (int i = 0; i < polygon.Points.Count; i++)
            {
                sdp[i] = new System.Drawing.Point((int)polygon.Points[i].X, (int)polygon.Points[i].Y);
            }
            
            Point[] p = polygon.Points.ToArray();
            System.Drawing.Point[] p1 = new System.Drawing.Point[8];

            return 0;

        }

        public static Point FindNearestPoligonPointToGivenPoint(Polygon polygon, Point point)
        {
            Point nearestPoint = Mathematics.FindNearestPolygonNodeToPoint(polygon, point);
            List<Line> edges = Mathematics.GetPolygonEndgesForGivenPoint(polygon, nearestPoint);

            Point p = Mathematics.GetIntersectionOfLineAndPerpendicularFromPoint(edges[0], point);
            if (PointIsInCut(p, edges[0]))
            {
                return p;
            }

            p = GetIntersectionOfLineAndPerpendicularFromPoint(edges[1], point);
            if (PointIsInCut(p, edges[1]))
            {
                return p;
            }

            return nearestPoint;
        }

        public static Point FindMostCloseXProjectionPoint(Point point, Polygon polygon, int AxisXOrientation, int AxisYOrientation)
        {
            Point result = polygon.Points[0];
            double YPoint = point.Y * Math.Sign(AxisYOrientation);
            //Select only pointd below or above given point            
            IEnumerable<Point> points = 
                polygon.Points.Where(x => Math.Sign(AxisYOrientation) * x.Y > Math.Sign(AxisYOrientation) * point.Y);

            if (AxisXOrientation > 0)
            {
                result = points.OrderBy(x => x.X).First();
            }
            else {
                result = points.OrderByDescending(x => x.X).First();
            }
            return result;

        }

        //public Pint FindMostClosePointByAngles(Point point, Polygon polygon, int AxisXOrientation, int AxisYOrientation)
        //{
        //    Point result = polygon.Points[0];
        //    double YPoint = point.Y * Math.Sign(AxisYOrientation);
        //    //Select only pointd below or above given point            
        //    IEnumerable<Point> points =
        //        polygon.Points.Where(x => Math.Sign(AxisYOrientation) * x.Y > Math.Sign(AxisYOrientation) * point.Y);
            
        //    Line l1 = new Line();
        //    l1.X1 = _toothSet.BoneEdge.Points[0].X;
        //    l1.Y1 = _toothSet.BoneEdge.Points[0].Y;
        //    l1.X2 = p.X;
        //    l1.Y2 = p.Y;

        //    Line l2 = new Line();
        //    l2.X1 = p.X;
        //    l2.Y1 = p.Y;
        //}

        //public static double GetAngleBetweenTwiLines1212(Line l1, Line l2)
        //{
        //    return Math.Atan2(l1.Y2 - l1.Y1, l1.X2 - l1.X1) - Math.Atan2(l2.Y2 - l2.Y1, l2.X2 - l2.X1);
        //    //return Math.Atan2(l1.Y1 - l1.Y2, l1.X1 - l1.X2) - Math.Atan2(l2.Y1 - l2.Y2, l2.X1 - l2.X2);
        //}

        public static double GetAngleBetweenTwiLines1212(Line l1, Line l2)
        {
            //Normalize
            if (l1.X1 == l1.X2)
            { 
                l1.X2 += 0.01;
            }

            if (l2.X1 == l2.X2)
            {
                l2.X2 += 0.01;
            }

            LineEquationNumbers len1 = Mathematics.GetLineEquationNumbers(l1);
            LineEquationNumbers len2 = Mathematics.GetLineEquationNumbers(l2);
            //double angle = (Math.Abs(len1.k * len2.k + 1)) / 
            //    (Math.Sqrt(1 + len1.k * len1.k) * Math.Sqrt(1 + len2.k * len2.k));
            double angle = (len1.k * len2.k + 1) /
                (Math.Sqrt(1 + len1.k * len1.k) * Math.Sqrt(1 + len2.k * len2.k));
            Debug.WriteLine("cos(x)=" + angle.ToString());
            angle = Math.Acos(angle);
            return angle;
        }

        public static double GetAngleBetweenTwiLines1221(Line l1, Line l2)
        {
            //return Math.Atan2(l1.Y2 - l1.Y1, l1.X2 - l1.X1) - Math.Atan2(l2.Y2 - l2.Y1, l2.X2 - l2.X1);
            return Math.Atan2(l1.Y1 - l1.Y2, l1.X1 - l1.X2) - Math.Atan2(l2.Y1 - l2.Y2, l2.X1 - l2.X2);
        }

        public static Line GetParallelLineThroughThePoint(Line line, Point point)
        {
            LineEquationNumbers ln = GetLineEquationNumbers(line);
            double c = point.Y - ln.k * point.X;

            Line result = new Line();
            result.X1 = point.X;
            result.Y1 = point.Y;
            result.X2 += 1;
            result.Y2 = ln.k * result.X2 + c;


            //result.X1 = (point.Y - c)/ln.k;
            //result.X2 = line.X2 + line.X1 - result.X1;
            //result.Y1 = result.X1 * ln.k + c;
            //result.Y2 = result.X2 * ln.k + c;

            return result;
        }

        public static Line GetLongestLineFromPoints(List<Point> points)
        {
            Line line = new Line();
            Point p1;
            Point p2;

            if (points.GroupBy(x => x.X).Count() == 1)
            {
                p1 = points.OrderBy(x => x.Y).First();
                p2 = points.OrderBy(x => x.Y).Last();
            }
            else
            {
                p1 = points.OrderBy(x => x.X).First();
                p2 = points.OrderBy(x => x.X).Last();
            }

            line.X1 = p1.X;
            line.X2 = p2.X;
            line.Y1 = p1.Y;
            line.Y2 = p2.Y;

            return line;
        }

        public static List<Point> GetIntersectionOf(Line line, Polygon polygon)
        {
            LineEquationNumbers len = GetLineEquationNumbers(line);
            return GetIntersectionOf(len, polygon);
        }

        public static List<Point> GetIntersectionOf(LineEquationNumbers len, Polygon polygon)
        {
            List<Point> list = new List<Point>();

            for (int index = 0; index < polygon.Points.Count; index++)
            {
                int index2 = (index + 1) % polygon.Points.Count;
                Line line1 = new Line();
                line1.X1 = polygon.Points[index].X;
                line1.X2 = polygon.Points[index2].X;
                line1.Y1 = polygon.Points[index].Y;
                line1.Y2 = polygon.Points[index2].Y;
                LineEquationNumbers len1 = GetLineEquationNumbers(line1);
                Point p = GetIntersectionOfTwoLines(len, len1);

                if (p.X >= Math.Min(line1.X1, line1.X2) &&
                    p.X <= Math.Max(line1.X1, line1.X2) &&
                    p.Y >= Math.Min(line1.Y1, line1.Y2) &&
                    p.Y <= Math.Max(line1.Y1, line1.Y2))
                {
                    list.Add(p);
                }
            }

           
            return list;
        }

        public static Point GetLineCenter(Line line)
        {
            return new Point((line.X1 + line.X2) / 2, (line.Y1 + line.Y2) / 2);
        }

        public static List<Point> FindMostDistantPolygonPointsUsingPerpendicularToMiddleLine(Line middleLine, Polygon polygon)
        {
            List<Point> result = new List<Point>();

            LineEquationNumbers len_perpendicular = Mathematics.GetPerpendicularLine(middleLine);

            ////take first polygon point as starting point
            Point p = polygon.Points[0];
            len_perpendicular.b = p.Y - len_perpendicular.k * p.X;

            double step = len_perpendicular.k / 2;
            bool flag = true;

            Point p1 = new Point();
            Point p2 = new Point();
            double width = double.MinValue;
            //List<Point> prevList = new List<Point>();

            int ii = 0;
            //move to positive direction
            while (flag)
            {
                List<Point> list = GetIntersectionOf(len_perpendicular, polygon);

                if (list == null || list.Count() == 0)
                {
                    break;
                }
                else if (list.Count() > 1)
                {
                    Line l1 = GetLongestLineFromPoints(list);
                    double w = Mathematics.GetDistanceBetweenTwoPoints(l1);
                    if (w > width)
                    {
                        width = w;
                        p1.X = l1.X1;
                        p1.Y = l1.Y1;
                        p2.X = l1.X2;
                        p2.Y = l1.Y2;
                    }
                }

                len_perpendicular.b+=step;
                Debug.WriteLine("Step: " + ii.ToString());
                ii++;
            }

            //move negative direction
            flag = true;
            len_perpendicular.b = p.Y - len_perpendicular.k * p.X;
            ii = 0;
            while (flag)
            {
                List<Point> list = GetIntersectionOf(len_perpendicular, polygon);

                if (list == null || list.Count() == 0)
                {
                    break;
                }
                else if (list.Count() > 1)
                {
                    Line l1 = GetLongestLineFromPoints(list);
                    double w = Mathematics.GetDistanceBetweenTwoPoints(l1);
                    if (w > width)
                    {
                        width = w;
                        p1.X = l1.X1;
                        p1.Y = l1.Y1;
                        p2.X = l1.X2;
                        p2.Y = l1.Y2;
                    }
                }

                len_perpendicular.b-=step;
                Debug.WriteLine("Step: " + ii.ToString());
                ii++;

            }

            result.Add(p1);
            result.Add(p2);
            return result;
        }


        public static List<Point> FindMostDistantPolygonPointsUsingMiddleLine(Line line, Polygon polygon)
        {
            LineEquationNumbers len = GetLineEquationNumbers(line);
            //Move to left
            bool flag = true;
            Point p1 = new Point();
            Point p2 = new Point();
            List<Point> prevList = new List<Point>();
            //Point result;

            while (flag)
            {
                List<Point> list = GetIntersectionOf(len, polygon);
                if (list.Count == 1)
                {
                    p1 = list[0];
                    break;
                    //return list[0];
                }
                else 
                if (list == null || list.Count == 0)
                { 
                    Line l1 = GetLongestLineFromPoints(prevList);
                    //result = GetLineCenter(l1);
                    p1 = GetLineCenter(l1);
                    break;
                    //return result;
                }
                
                len.b++;
                prevList = list;
                flag = list.Count > 0;
            }

            //Move to right
            len = GetLineEquationNumbers(line);
            while (flag)
            {
                List<Point> list = GetIntersectionOf(len, polygon);
                if (list.Count == 1)
                {
                    p2 = list[0];
                    break;
                    //return list[0];
                }
                else
                    if (list == null || list.Count == 0)
                    {
                        Line l1 = GetLongestLineFromPoints(prevList);
                        //result = GetLineCenter(l1);
                        p2 = GetLineCenter(l1);
                        break;
                        //return result;
                    }

                len.b--;
                prevList = list;
                flag = list.Count > 0;
            }

            return new List<Point> { p1, p2 };
        }

        public static double GetDistanceBetweenTwoPoints(Point p1, Point p2)
        {
            double d1 = p1.X - p2.X;
            double d2 = p1.Y - p2.Y;

            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        public static double GetDistanceBetweenTwoPoints(Line line)
        {
            double d1 = line.X1 - line.X2;
            double d2 = line.Y1 - line.Y2;

            return Math.Sqrt(d1 * d1 + d2 * d2);
        }

        #region Private methods
        //public static PointF FindLineIntersection(PointF start1, PointF end1, PointF start2, PointF end2)
        //{
        //    float denom = ((end1.X - start1.X) * (end2.Y - start2.Y)) - ((end1.Y - start1.Y) * (end2.X - start2.X));

        //    //  AB & CD are parallel 
        //    if (denom == 0)
        //        return Point.Empty;

        //    float numer = ((start1.Y - start2.Y) * (end2.X - start2.X)) - ((start1.X - start2.X) * (end2.Y - start2.Y));

        //    float r = numer / denom;

        //    float numer2 = ((start1.Y - start2.Y) * (end1.X - start1.X)) - ((start1.X - start2.X) * (end1.Y - start1.Y));

        //    float s = numer2 / denom;

        //    if ((r < 0 || r > 1) || (s < 0 || s > 1))
        //        return PointF.Empty;

        //    // Find intersection point
        //    PointF result = new PointF();
        //    result.X = start1.X + (r * (end1.X - start1.X));
        //    result.Y = start1.Y + (r * (end1.Y - start1.Y));

        //    return result;
        //}


        private static bool PointIsInCut(Point point, Line cut)
        {
            bool result = false;
            if ((point.X >= Math.Min(cut.X1, cut.X2) && point.X <= Math.Max(cut.X1, cut.X2)) &&
                (point.Y >= Math.Min(cut.Y1, cut.Y2) && point.Y <= Math.Max(cut.Y1, cut.Y2)))
            {
                result = true;
            }
            return result;
        }

        private static Point GetIntersectionOfTwoLines(LineEquationNumbers len1, LineEquationNumbers len2)
        {
            Point p = new Point();
            if (len1.k == len2.k)
            {
                return p;
            }

            p.X = (len2.b - len1.b) / (len1.k - len2.k);
            p.Y = len1.k * p.X + len1.b;
            return p;
        }

        public static LineEquationNumbers GetPerpendicularLine(Line line)
        {
            LineEquationNumbers ln1 = GetLineEquationNumbers(line);
            LineEquationNumbers ln2 = new LineEquationNumbers();
            //Find orthogonal line
            //y = (-1/k1)*x + y0 + (1/k1)*x0
            ln2.k = -1 / ln1.k;
            ln2.b = 0;
            return ln2;
        }

        public static Point GetIntersectionOfLineAndPerpendicularFromPoint(Line line, Point point)
        {
            LineEquationNumbers ln1 = GetLineEquationNumbers(line);

            LineEquationNumbers ln2 = new LineEquationNumbers();
            //Find orthogonal line
            //y = (-1/k1)*x + y0 + (1/k1)*x0
            ln2.k = -1 / ln1.k;
            ln2.b = point.Y + (1 / ln1.k) * point.X;

            //Find intersectionof 2 lines
            Point result = new Point();
            result.X = (ln2.b - ln1.b) / (ln1.k - ln2.k);
            result.Y = ln1.k * result.X + ln1.b;

            return result;
        }

        private static LineEquationNumbers GetLineEquationNumbers(Line line)
        {
            LineEquationNumbers ln = new LineEquationNumbers();
            ln.k = (line.Y2 - line.Y1) / (line.X2 - line.X1);
            ln.b = line.Y1 - ln.k * line.X1;

            return ln;
        }

        private static List<Line> GetPolygonEndgesForGivenPoint(Polygon polygon, Point point)
        {
            List<Line> edges = new List<Line>();
            int i = polygon.Points.IndexOf(point);
            int prevIndex = i > 0 ? i-1 : polygon.Points.Count;
            int nextIndex = i < polygon.Points.Count-1 ? i+1 : 0;

            Line l1 = new Line();
            l1.X1 = point.X;
            l1.X2 = polygon.Points[prevIndex].X;
            l1.Y1 = point.Y;
            l1.Y2 = polygon.Points[prevIndex].Y;

            edges.Add(l1);
            Line l2 = new Line();
            l2.X1 = point.X;
            l2.X2 = polygon.Points[nextIndex].X;
            l2.Y1 = point.Y;
            l2.Y2 = polygon.Points[nextIndex].Y;
            edges.Add(l2);
            return edges;
        }


        private static Point FindNearestPolygonNodeToPoint(Polygon polygon, Point point)
        {
            double minDistance = double.MaxValue;
            Point nearestPoint = polygon.Points[0];

            foreach (Point p in polygon.Points)
            {
                double distance = Mathematics.GetDistanceBetweenTwoPoints(p, point);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestPoint = p;
                }
            }
            return nearestPoint;
        }

        #endregion

    }
}
