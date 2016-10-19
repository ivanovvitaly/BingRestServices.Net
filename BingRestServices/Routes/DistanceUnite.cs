namespace BingRestServices.Routes
{
    public class DistanceUnite: KeyType
    {
        public static readonly DistanceUnite Mile = Create<DistanceUnite>("mi");
        public static readonly DistanceUnite Kilometer = Create<DistanceUnite>("km");
    }
}