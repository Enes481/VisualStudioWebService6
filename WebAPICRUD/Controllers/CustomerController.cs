using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPICRUD.Models;

namespace WebAPICRUD.Controllers
{
    public class CustomerController : ApiController
    {
        //GET - retrieve data
        public IHttpActionResult GetAllCustomer()
        {
            IList<CustomerViewModel> customers = null;
            using(var x = new WEBAPIEntities())
            {
                customers = x.Customers
                    .Select(c => new CustomerViewModel()
                    {
                        Id = c.id,
                        Name = c.name,
                        Email=c.email,
                        Address=c.address,
                        Phone=c.phone
                      
                    }).ToList<CustomerViewModel>();
            }
            if (customers.Count == 0)
                return NotFound();

            return Ok(customers);
        }
        //POST - Insert new record

        /*public IHttpActionResult PostNewCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.Please check.");
            

            using(var x = new WEBAPIEntities())
            {
                x.Customers.Add(new Customer()
                {
                    Name = customer.Name


                });
                
                x.SaveChanges();
            }
            return Ok();
        }*/
        //PUT - Update data base on id
        public IHttpActionResult PutCustomer(CustomerViewModel customer)
        {
            if (!ModelState.IsValid)
                return BadRequest("This is invalid model. Please recheck");

            using(var x = new WEBAPIEntities())
            {
                var chechExistingCustomer = x.Customers.Where(c => c.id == customer.Id)
                                                        .FirstOrDefault<Customer>;

                if (chechExistingCustomer != null)
                {
                    chechExistingCustomer.name = customer.Name;
                    chechExistingCustomer.address = customer.Address;
                    chechExistingCustomer.phone = customer.Phone;

                    x.SaveChanges();

                }
                else
                    return NotFound();
                   

            }

            return Ok();
        }
        //DELETE - Delete a record base on id

        public IHttpActionResult DeleteCustomer(int id) 
        {
            if(id < 0)
            {
                return BadRequest("Please Enter Valid Customer Id ");
            }
          

            using (var x = new WEBAPIEntities())
            {
                var customer = x.Customers
                    .Where(c => c.id == id)
                    .FirstOrDefault();

                x.Entry(customer).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }

    }
}
