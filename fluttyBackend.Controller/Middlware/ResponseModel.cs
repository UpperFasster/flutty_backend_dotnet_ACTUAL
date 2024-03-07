using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fluttyBackend.Controller.Middlware
{
    public class ResponseModel
    {
        public int statusCode { get; set; }
        public string message { get; set; }
        public string path { get; set; }
    }
}