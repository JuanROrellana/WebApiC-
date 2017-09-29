using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEbApi.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public string Description { get; set; }
        public DateTime OrderDate { get; set; }
        public float Amount { get; set; }
        public int Quantity { get; set; }
        public virtual Customer Customers { get; set; }

        public class OrderDetail
        {
            public int OrderID { get; set; }
            public string Description { get; set; }
            public string OrderDate { get; set; }
            public float Amount { get; set; }
            public int Quantity { get; set; }
            public int CustomersID { get; set; }

        }
    }
}