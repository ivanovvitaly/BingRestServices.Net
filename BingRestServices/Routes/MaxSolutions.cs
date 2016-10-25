using System;

namespace BingRestServices.Routes
{
    public class MaxSolutions: KeyType
    {
        public static readonly MaxSolutions One = Create<MaxSolutions>("1");
        public static readonly MaxSolutions Two = Create<MaxSolutions>("2");
        public static readonly MaxSolutions Three = Create<MaxSolutions>("3");
        
        private static readonly int MinValue = 1;
        private static readonly int MaxValue = 3;

        public MaxSolutions()
        {
            Key = MinValue.ToString();
        }

        public MaxSolutions(int maxSolutions)
        {
            Key = Math.Max(Math.Min(maxSolutions, MaxValue), MinValue).ToString();
        }
    }
}