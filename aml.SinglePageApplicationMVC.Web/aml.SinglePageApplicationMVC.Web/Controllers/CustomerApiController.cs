using aml.SinglePageApplicationMVC.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace aml.SinglePageApplicationMVC.Web.Controllers
{
    public class CustomerApiController : ApiController
    {
        private ICustomerRepository customerRepository;

        public CustomerApiController()
        {
            this.customerRepository = new CustomerRepository(new CustomerDbContext());
        }

        public CustomerApiController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: api/HomeApi
        public IEnumerable<Customer> Get()
        {
            return this.customerRepository.FindAll();
        }

        // GET: api/HomeApi/5
        public Customer Get(int id)
        {
            return this.customerRepository.Get(id);
        }

        // POST: api/HomeApi
        public void Post(Customer customer)
        {
            this.customerRepository.Create(customer);
            this.customerRepository.Save();
        }

        // PUT: api/HomeApi/5
        public void Put(int id, Customer customer)
        {
            customer.Id = id;

            this.customerRepository.Update(customer);
            this.customerRepository.Save();
        }
    }
}
