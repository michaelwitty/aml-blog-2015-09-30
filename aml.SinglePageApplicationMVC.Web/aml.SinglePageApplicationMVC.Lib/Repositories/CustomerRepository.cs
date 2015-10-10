using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aml.SinglePageApplicationMVC.Lib
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _dbContext;

        public CustomerRepository()
        {
            this._dbContext = new CustomerDbContext();
        }

        public CustomerRepository(CustomerDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public Customer Get(int id)
        {
            return this._dbContext.Customers.FirstOrDefault(x => x.Id == id);
        }

        public void Update(Customer entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Create(Customer entity)
        {
            this._dbContext.Customers.Add(entity);
        }

        public void Save()
        {
            this._dbContext.SaveChanges();
        }

        public IEnumerable<Customer> FindAll()
        {
            return this._dbContext.Customers;
        }
    }
}
