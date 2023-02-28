using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SenVang.Customer.Models.Request
{
    public class CustomerHeader
    {
        public CustomerRequest header { get; set; }
    }
    public class CustomerRequest
    {
        public int ObjCreateBy { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Area { get; set; }
        public int Sex { get; set; }
    }
    public class CheckEmailRequest
    {
        public int custid { get; set; }
        public string email { get; set; }
    }
    public class GetVoucherOnlineRequest
    {
        public int CustomerId { get; set; }
    }
    
    public class GetVoucherOnlineResponse
    {
        public string VoucherCode { get; set; }
        public string ExpiredDate { get; set; }
    }
    public class SendvoucherRequest
    {
        public int CustomerId { get; set; }
        public string Email { get;set; }
    }
    public class TaxCustInfo
    {
        public string ma_so_thue { get; set; }
        public string ten_cty { get; set; }
        public string dia_chi { get; set; }

    }
}