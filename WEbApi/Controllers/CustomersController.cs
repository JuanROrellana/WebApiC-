using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WEbApi.Models;

namespace WEbApi.Controllers
{
    public class CustomersController : ApiController
    {
        private DBContext database = new DBContext();

        public CustomersController()
        {
            database = new DBContext();
        }

        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetCustomers()
        {
            try
            {
                string url = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=";

                List<Customer.CustomerDetail> customers =
                (from u in database.Customers
                 select new Customer.CustomerDetail
                 {
                     CustomerID = u.CustomerID,
                     Address = u.Address,
                     CompanyName = u.CompanyName,
                     ContactName = u.ContactName,
                     Country = u.Country,
                     InfoLink = url + u.CustomerID,
                     TelephoneNumber = u.TelephoneNumber
                 }).ToList<Customer.CustomerDetail>();

                return Ok(customers);

            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }
        
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=";

                Customer.CustomerDetail customers =
                (from u in database.Customers
                 where u.CustomerID == id
                 select new Customer.CustomerDetail
                 {
                     CustomerID = u.CustomerID,
                     Address = u.Address,
                     CompanyName = u.CompanyName,
                     ContactName = u.ContactName,
                     Country = u.Country,
                     InfoLink = url + u.CustomerID,
                     TelephoneNumber = u.TelephoneNumber
                 }).SingleOrDefault();

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IHttpActionResult PostCreateCustomer(Customer model)
        {
            try
            {
                Customer customer = new Customer
                {
                    Country = model.Country,
                    Address = model.Address,
                    CompanyName = model.CompanyName,
                    TelephoneNumber = model.TelephoneNumber,
                    ContactName = model.ContactName,
                };

                database.Customers.Add(customer);
                database.SaveChanges();
                return Ok(customer.CustomerID);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }

        [HttpGet]
        public IHttpActionResult GetOrders(int id)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=";

                List<Order.OrderDetail> orders =
                (from u in database.Orders
                 where u.Customers.CustomerID == id
                 select new Order.OrderDetail
                 {
                     OrderID = u.OrderID,
                     Amount = u.Amount,
                     Description = u.Description,
                     Quantity = u.Quantity,
                     CustomersID = u.Customers.CustomerID,
                     OrderDate = u.OrderDate.ToString()
                 }).ToList<Order.OrderDetail>();

                return Ok(orders);

            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }

        [HttpGet]
        public IHttpActionResult GetOrder(int id)
        {
            try
            {
                string url = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=";

                Order.OrderDetail order =
                (from u in database.Orders
                 where u.OrderID == id
                 select new Order.OrderDetail
                 {
                     OrderID = u.OrderID,
                     Amount = u.Amount,
                     Description = u.Description,
                     OrderDate = u.OrderDate.ToString(),
                     Quantity = u.Quantity,
                     CustomersID = u.Customers.CustomerID
                 }).SingleOrDefault();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }

        [HttpPost]
        public IHttpActionResult PostCreateOrder(Order.OrderDetail model)
        {
            try
            {
                Order order = new Order
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    Quantity = model.Quantity,
                    OrderDate = DateTime.Now
                };

                database.Orders.Add(order);

                Customer customer = database.Customers.Find(model.CustomersID);
                customer.Orders.Add(order);
                database.SaveChanges();
                return Ok(order.OrderID);
            }
            catch (Exception ex)
            {
                return Ok(0);
            }
        }

    }
}