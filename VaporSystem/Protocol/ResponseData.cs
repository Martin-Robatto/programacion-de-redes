using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol
{
    public class ResponseData
    {
        public int StatusCode { get; set; }
        public int Function { get; set; }
        public string Data { get; set; }

        public ResponseData() { }
    }
}
