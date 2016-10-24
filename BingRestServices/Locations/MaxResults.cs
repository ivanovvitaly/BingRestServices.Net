using System;

namespace BingRestServices.Locations
{
    public class MaxResults : KeyType
    {
        public MaxResults(int maxResults)
        {
            Key = Math.Max(Math.Min(maxResults, 20), 1).ToString();
        }
    }
}