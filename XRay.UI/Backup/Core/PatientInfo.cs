using System;
using System.Collections.Generic;

namespace XRay.UI.Core
{
    [Serializable]
    public class PatientInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ToothNumber { get; set; }
        public DateTime DateOfBirth { get; set; }

        public String Notes { get; set; }
        public String ImageFileName { get; set; }


        public List<XRayImage> XRayImages { get; set; }
    }
}