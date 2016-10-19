using System;
using System.Runtime.Serialization;

namespace BingRestServices.Exceptions
{
    [Serializable]
    public class BingConfigurationException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BingConfigurationException()
        {
        }

        public BingConfigurationException(string message)
            : base(message)
        {
        }

        public BingConfigurationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BingConfigurationException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}