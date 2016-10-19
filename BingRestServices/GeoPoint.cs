namespace BingRestServices
{
    public class GeoPoint : IGeoLocation
    {
        public GeoPoint(double lat, double lng)
        {
            Lat = lat;
            Lng = lng;
        }

        public double Lat { get; set; }

        public double Lng { get; set; }

        public override string ToString()
        {
            return string.Format("{0},{1}", Lat, Lng);
        }

        public string GetFormattedString()
        {
            return ToString();
        }


        public static GeoPoint Create(double lat, double lng)
        {
            return new GeoPoint(lat, lng);
        }
    }
}