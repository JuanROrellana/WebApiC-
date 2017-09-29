using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEbApi.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string ContactName { get; set; }
        public string TelephoneNumber { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public class CustomerDetail
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
}