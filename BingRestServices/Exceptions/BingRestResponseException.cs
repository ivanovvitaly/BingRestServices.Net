using System;
using System.Runtime.Serialization;

namespace BingRestServices.Exceptions
{
    [Serializable]
    public class BingRestResponseException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public BingRestResponseException()
        {
        }

        public BingRestResponseException(string message)
            : base(message)
        {
        }

        public BingRestResponseException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected BingRestResponseException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}