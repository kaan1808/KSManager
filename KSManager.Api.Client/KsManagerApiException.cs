using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace KSManager.Api
{
    [Serializable]
    public class KsManagerApiException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public KsManagerApiException()
        {
        }

        public KsManagerApiException(string message) : base(message)
        {
        }

        public KsManagerApiException(string message, Exception inner) : base(message, inner)
        {
        }

        protected KsManagerApiException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }

        public HttpStatusCode StatusCode { get; set; }
    }
}