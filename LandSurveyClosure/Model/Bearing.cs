using System;
namespace LandSurveyClosure.Model
{
    /// <summary>
    /// Bearing.
    /// Is made of Degrees, Minutes and Seconds
    /// </summary>
    public class Bearing
    {
        public int BearingId
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
