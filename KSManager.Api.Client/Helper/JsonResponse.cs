using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace KSManager.Api.Client.Helper
{
    internal class JsonResponse
    {
        public string Json { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}
