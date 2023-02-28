using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenVang.Customer.Models.Request
{
    public class CustomerResponseViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNo { get; set; }
        public bool IsActive { get; set; }
    }
}