using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    [Serializable]
    public class ToothSet
    {
        #region Constants
        public static int MaxNormalToothPointsCount = 8;
        public static int MaxProblemToothPointsCount = 14;
        public static int MaxBonePointsCount = 1;

        #endregion

        #region Constructor

        public ToothSet()
        {
            normalTooth.MaxPoligonPointsNumber = MaxNormalToothPointsCount;
            problemTooth.MaxPoligonPointsNumber = MaxProblemToothPointsCount;
            boneEdge.MaxPoligonPointsNumber = MaxBonePointsCount;
        }
        #endregion

        #region Fields
        private ToothPolygon normalTooth = new ToothPolygon();
        private ToothPolygon problemTooth = new ToothPolygon();
        private ToothPolygon boneEdge = new ToothPolygon();

        public ToothPolygon NormalTooth {
            get
            {
                return normalTooth;
            }
            set
            {
                if (value != null)
                    normalTooth = value;
            }
        }
        public ToothPolygon ProblemTooth
        {
            get
            {
                return problemTooth;
            }
            set
            {
                if (value != null)
                    problemTooth = value;
            }
        }

        public ToothPolygon BoneEdge
        {
            get
            {
                return boneEdge;
            }
            set
            {
                if (value != null)
                    boneEdge = value;
            }
        }

        //public bool IsRightOrientation
        //{
        //    get { 
        //        if (normalTooth == null || normalTooth.Points.Count < 1 ||
        //            boneEdge == null || boneEdge.Points.Count < 1)
        //        {
        //            return true;
        //        }
        //        return normalTooth.Points[0].X < boneEdge.Points[0].X;
        //    }
        //}

        //public bool IsRightOriented = true;

        public bool IsClosed
        {
            get {
                return NormalTooth.IsClosed && ProblemTooth.IsClosed && BoneEdge.IsClosed;
            }
        }
        #endregion
    }
}
