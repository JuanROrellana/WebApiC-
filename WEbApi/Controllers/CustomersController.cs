using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using WEbApi.Models;
using WEbApi.Dtos;

namespace WEbApi.Controllers
{
    public class CustomersController : ApiController
    {
        private DBContext database = new DBContext();

        public CustomersController() => database = new DBContext();


        protected override void Dispose(bool disposing)
        {
            database.Dispose();
        }

        /// <summary>
        /// Gets all customers.
        /// </summary>
        /// 
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomers()
        {
            try
            {
                var suds = await database.Customers.ToListAsync();
                var customers = await database.Customers
                    .Select(c => new CustomerDto ()
                    {
                        CustomerID = c.CustomerID,
                        Address = c.Address,
                        CompanyName = c.CompanyName,
                        ContactName = c.ContactName,
                        Country = c.Country,
                        //InfoLink = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=" + c.CustomerID,
                        TelephoneNumber = c.TelephoneNumber
                    }).ToListAsync();

                return Ok(customers);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Get customer by Id.
        /// </summary>
        /// <param name="id">Id of the customer</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomer(int id)
        {
            try
            {
                var customer =
                await (from u in database.Customers
                       where u.CustomerID == id
                       select new CustomerDto ()
                       {
                           CustomerID = u.CustomerID,
                           Address = u.Address,
                           CompanyName = u.CompanyName,
                           ContactName = u.ContactName,
                           Country = u.Country,
                           //InfoLink = HttpContext.Current.Request.Url.Authority + "/api/Customers/Customer?id=" + u.CustomerID,
                           TelephoneNumber = u.TelephoneNumber
                       }).SingleOrDefaultAsync();

                if (customer == null)
                    return NotFound();

                return Ok(customer);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Create a customer.
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> CreateCustomer(Customer model)
        {
            try
            {
                var customer = new Customer
                {
                    Country = model.Country,
                    Address = model.Address,
                    CompanyName = model.CompanyName,
                    TelephoneNumber = model.TelephoneNumber,
                    ContactName = model.ContactName,
                };

                database.Customers.Add(customer);
                await database.SaveChangesAsync();

                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Get orders by Id of the customer.
        /// </summary>
        /// <param name="id">Id of the customer.</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetOrdersByCustomer(int id)
        {
            try
            {
                var orders =
                await (from u in database.Orders
                       where u.Customers.CustomerID == id
                       select new OrderDto
                       {
                           OrderID = u.OrderID,
                           Amount = u.Amount,
                           Description = u.Description,
                           Quantity = u.Quantity,
                           CustomersID = u.Customers.CustomerID,
                           OrderDate = u.OrderDate.ToString()
                       }).ToListAsync();

                return Ok(orders);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Get order by Id.
        /// </summary>
        /// <param name="id">Order Id</param>
        [HttpGet]
        public async Task<IHttpActionResult> GetOrder(int id)
        {
            try
            {
                var order =
                await (from u in database.Orders
                 where u.OrderID == id
                 select new OrderDto ()
                 {
                     OrderID = u.OrderID,
                     Amount = u.Amount,
                     Description = u.Description,
                     OrderDate = u.OrderDate.ToString(),
                     Quantity = u.Quantity,
                     CustomersID = u.Customers.CustomerID
                 }).SingleOrDefaultAsync();

                return Ok(order);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        /// <summary>
        /// Create Order
        /// </summary>
        [HttpPost]
        public async Task<IHttpActionResult> CreateOrder(OrderDto model)
        {
            try
            {
                var order = new Order
                {
                    Amount = model.Amount,
                    Description = model.Description,
                    Quantity = model.Quantity,
                    OrderDate = DateTime.Now
                };

                database.Orders.Add(order);

                var customer = await database.Customers.FindAsync(model.CustomersID);

                if (customer == null)
                    return NotFound();

                customer.Orders.Add(order);
                await database.SaveChangesAsync();
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

    }
}