using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenVang.Customer.Models.Request
{
    public class BaseResponse
    {
        public bool Success { get; set; }
        public string MessageErrors { get; set; }
        public List<dynamic> DataResult { get; set; }
    }
}