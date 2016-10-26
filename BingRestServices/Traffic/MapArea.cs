namespace BingRestServices.Traffic
{
    public class MapArea
    {
        public MapArea(double southLatitude, double westLongitude, double northLatitude, double eastLongitude)
        {
            SouthLatitude = southLatitude;
            WestLongitude = westLongitude;
            NorthLatitude = northLatitude;
            EastLongitude = eastLongitude;
        }

        public double SouthLatitude { get; set; }

        public double WestLongitude { get; set; }

        public double NorthLatitude { get; set; }

        public double EastLongitude { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1},{2},{3}", SouthLatitude, WestLongitude, NorthLatitude, EastLongitude);
        }
    }
}