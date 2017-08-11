using System;
namespace LandSurveyClosure.Model
{
    /// <summary>
    /// Closure line.
    /// Consists of a Bearing Object (degrees, minutes and seconds) and distance
    /// </summary>
    public class ClosureLine
    {
      
      
        public int ClosureLineId
        {
            get;
            set;
        }

        public double Distance
        {
            get;
            set;
        }

        public int Degrees
        {
            get;
            set;
        }

        public int Minutes
        {
            get;
            set;
        }

        public int Seconds
        {
            get;
            set;
        }
    }
}
