using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenVang.Customer.Models.Request
{
    public class CustomerResponse
    {
        public bool Success { get; set; }
        public string MessageErrors { get; set; }
        public List<CustomerResponseViewModel> DataResult { get; set; }
    }
    public class Provices
    {
        public int Id { get; set; }
        public string Name { get; set; }    
    }
}