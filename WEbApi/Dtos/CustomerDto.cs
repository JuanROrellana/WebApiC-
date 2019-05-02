using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEbApi.Dtos
{
    public class CustomerDto
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string TelephoneNumber { get; set; }
        public string InfoLink { get; set; }
    }
}