namespace BingRestServices.Routes
{
    public class TravelMode : KeyType
    {
        public static readonly TravelMode Driving = Create<TravelMode>("Driving");
        public static readonly TravelMode Walking = Create<TravelMode>("Walking");
        public static readonly TravelMode Transit = Create<TravelMode>("Transit");
    }
}