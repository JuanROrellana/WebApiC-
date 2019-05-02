using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEbApi.Dtos
{
    public class OrderDto
    {
        public int OrderID { get; set; }
        public string Description { get; set; }
        public string OrderDate { get; set; }
        public float Amount { get; set; }
        public int Quantity { get; set; }
        public int CustomersID { get; set; }
    }
}