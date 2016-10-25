using System;

namespace BingRestServices.Locations
{
    public class MaxResults : KeyType
    {
        private static readonly int MinValue = 1;
        private static readonly int MaxValue = 20;

        public MaxResults(int maxResults)
        {
            Key = Math.Max(Math.Min(maxResults, MaxValue), MinValue).ToString();
        }
    }
}