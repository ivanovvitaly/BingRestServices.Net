namespace BingRestServices.Traffic
{
    public class Severity : KeyType
    {
        public static readonly Severity LowImpact = Create<Severity>("1");
        public static readonly Severity Minor = Create<Severity>("2");
        public static readonly Severity Moderate = Create<Severity>("3");
        public static readonly Severity Serious = Create<Severity>("4");
    }
}