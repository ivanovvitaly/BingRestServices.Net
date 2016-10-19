namespace BingRestServices.Routes
{
    public class TransiteTimeType : KeyType
    {
        public static readonly TransiteTimeType Arrival = Create<TransiteTimeType>("arrival");
        public static readonly TransiteTimeType Departure = Create<TransiteTimeType>("departure");
        public static readonly TransiteTimeType LastAvailable = Create<TransiteTimeType>("lastAvailable");
    }
}