using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class CalculatedInformation
    {
        public enum Retensy {A, B, C, ND};
        public enum Angulation { a1, a2, a3, a4, ND};
        public enum Sectioning {Single, Double, NotNeeded, ND};
        public enum ComplexityLevel { l1, l2, l3, ND };

        public Retensy DepthOfRetensy = Retensy.ND;
        public string DepthOfRetensyText
        {
            get
            {
                string result = string.Empty;
                switch (DepthOfRetensy)
                { 
                    case Retensy.A:
                        result = "висока, тип \"A\"";
                        break;
                    case Retensy.B:
                        result = "середня, тип \"B\"";
                        break;
                    case Retensy.C:
                        result = "глибока, тип \"C\"";
                        break;

                }
                return result;
            }
        }

        public string AngulationType = "мезіальна";
        public double AngulationAngle;
        public string AngulationAngleText
        {
            get {
                return Math.Round(AngulationAngle, 0).ToString() + "°";
            }
        }

        public Angulation AngulationLevel
        {
            get {
                Angulation result = Angulation.ND;
                if (AngulationAngle >= 0 && AngulationAngle <= 22.5)
                {
                    result = Angulation.a1;
                }
                else if (AngulationAngle > 22.5 && AngulationAngle <= 45)
                {
                    result = Angulation.a2;
                }
                else if (AngulationAngle > 45 && AngulationAngle <= 67.5)
                {
                    result = Angulation.a3;
                }
                else if (AngulationAngle > 67.5)
                {
                    result = Angulation.a4;
                }
                return result;
            }
        }

        public string AngulationLevelText
        {
            get
            {
                string result = string.Empty;
                switch (AngulationLevel)
                {
                    case Angulation.a1:
                        result = "1-ий тип";
                        break;
                    case Angulation.a2:
                        result = "2-ий тип";
                        break;
                    case Angulation.a3:
                        result = "3-ій тип";
                        break;
                    case Angulation.a4:
                        result = "4-ий тип";
                        break;
                }
                return result;
            }
        }
        //public double AngulationLevelInDegrees;

        public bool SрreadFormingNeeded
        {
            get
            {
                return !(DepthOfRetensy == Retensy.A && (AngulationLevel == Angulation.a1 || AngulationLevel == Angulation.a2));
            }
        }
        public Sectioning ToothSectioning = Sectioning.ND;

        public string ToothSectioningText
        {
            get {
                string result = string.Empty;

                if (ToothSectioning == Sectioning.NotNeeded)
                {
                    result = "не протебується";
                }
                else {
                    result = "протебується";

                    if (ToothSectioning == Sectioning.Single)
                    {
                        result += ", в один етап";
                    }
                    else if (ToothSectioning == Sectioning.Double)
                    {
                        result += ", в два етапи";
                    }
                }


                return result;
            }
        }

        public string ToothSectioningText_1
        {
            get
            {
                string result = string.Empty;

                if (ToothSectioning == Sectioning.NotNeeded)
                {
                    result = "не протебується";
                }
                else
                {
                    result = "протебується, ";
                }


                return result;
            }
        }

        public string ToothSectioningText_2
        {
            get
            {
                string result = string.Empty;

                if (ToothSectioning != Sectioning.NotNeeded)
                {

                    if (ToothSectioning == Sectioning.Single)
                    {
                        result = "в один етап";
                    }
                    else if (ToothSectioning == Sectioning.Double)
                    {
                        result = "в два етапи";
                    }
                }


                return result;
            }
        }

        public bool CheeckBoneCutNeeded
        {
            get
            {
                return !(DepthOfRetensy == Retensy.A && (AngulationLevel == Angulation.a1 || AngulationLevel == Angulation.a2));
            }
        }

        public bool DistalBoneCutNeeded = false;

        public string SurgicalInjuryLevel
        {
            get 
            {
                //A1, A2, В1, – незначний
                //А3, А4, В2, С1, С2 – середнього ступеня
                //В3, В4, С3, С4 – значний
                string result = string.Empty;
                switch (GetComplexityLevel())
                {
                    case ComplexityLevel.l1:
                        result = "незначний";
                        break;
                    case ComplexityLevel.l2:
                        result = "середнього ступеня";
                        break;
                    case ComplexityLevel.l3:
                        result = "значний";
                        break;
                }

                return result;
            }
        }

        public string EstimatedDuration
        {
            get
            {
                //Прогнозована тривалість
                //A1, A2, В1, – 15- 25 хвилин
                //А3, А4, В2, С1, С2 – 25 – 35 хвилин
                //В3, В4, С3, С4 – 35 – 45 хвилин

                string result = string.Empty;
                switch (GetComplexityLevel())
                {
                    case ComplexityLevel.l1:
                        result = "15- 25 хвилин";
                        break;
                    case ComplexityLevel.l2:
                        result = "25 – 35 хвилин";
                        break;
                    case ComplexityLevel.l3:
                        result = "35 – 45 хвилин";
                        break;
                }

                return result;
            }
        }

        public string OperationComplexityLevel
        {
            get
            {
                //A1, A2, В1, – низький
                //А3, А4, В2, С1, С2 – середній
                //В3, В4, С3, С4 – важкий

                string result = string.Empty;
                switch (GetComplexityLevel())
                {
                    case ComplexityLevel.l1:
                        result = "низький";
                        break;
                    case ComplexityLevel.l2:
                        result = "середній";
                        break;
                    case ComplexityLevel.l3:
                        result = "важкий";
                        break;
                }

                return result;
            }
        }

        public double SectioningN1Angle;

        public string SectioningN1AngleText
        { 
            get {
                return SectioningN1Angle.ToString() + "°";
            }
        }

        public string SectioningN1AngleTypeText = string.Empty;

        private ComplexityLevel GetComplexityLevel()
        {
            //A1, A2, В1, – незначний
            //А3, А4, В2, С1, С2 – середнього ступеня
            //В3, В4, С3, С4 – значний
            ComplexityLevel result = ComplexityLevel.ND;

            if (
                (DepthOfRetensy == Retensy.A && (AngulationLevel == Angulation.a1 || AngulationLevel == Angulation.a2)) ||
                (DepthOfRetensy == Retensy.B && AngulationLevel == Angulation.a1)
                )
            {
                result = ComplexityLevel.l1;
            }
            else if (
                (DepthOfRetensy == Retensy.A && (AngulationLevel == Angulation.a3 || AngulationLevel == Angulation.a4)) ||
                (DepthOfRetensy == Retensy.B && AngulationLevel == Angulation.a2) ||
                (DepthOfRetensy == Retensy.C && (AngulationLevel == Angulation.a1 || AngulationLevel == Angulation.a2))
                )
            {
                result = ComplexityLevel.l2;
            }
            else if (
                (DepthOfRetensy == Retensy.B && (AngulationLevel == Angulation.a3 || AngulationLevel == Angulation.a4)) ||
                (DepthOfRetensy == Retensy.C && (AngulationLevel == Angulation.a3 || AngulationLevel == Angulation.a4))
                )
            {
                result = ComplexityLevel.l3;
            }

            return result; 
        }


    }
}
