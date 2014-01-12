using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public interface IPoint
    {
        int X {get; set;}
        int Y { get; set;}
        int OrderNumber { get; set;}
        List<Edge> Edges { get; set; }
    }
}
