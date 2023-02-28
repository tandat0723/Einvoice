using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenVang.Customer.Models.Request
{
    public class LoadCustomerResponseViewModel
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Area { get; set; }
        public int Sex { get; set; }
    }
}