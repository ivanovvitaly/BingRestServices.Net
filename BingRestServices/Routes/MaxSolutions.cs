using System;

namespace BingRestServices.Routes
{
    public class MaxSolutions: KeyType
    {
        public static readonly MaxSolutions One = Create<MaxSolutions>("1");
        public static readonly MaxSolutions Two = Create<MaxSolutions>("2");
        public static readonly MaxSolutions Three = Create<MaxSolutions>("3");

        public MaxSolutions()
        {
            Key = "1";
        }

        public MaxSolutions(int maxSolutions)
        {
            Key = Math.Max(Math.Min(maxSolutions, 3), 1).ToString();
        }
    }
}