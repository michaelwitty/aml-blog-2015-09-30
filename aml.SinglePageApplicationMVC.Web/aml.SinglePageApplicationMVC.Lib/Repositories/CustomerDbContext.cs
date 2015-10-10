using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aml.SinglePageApplicationMVC.Lib
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext()
            : base("CustomerContext")
        {
            Database.SetInitializer<CustomerDbContext>(new DropCreateDatabaseAlways<CustomerDbContext>());
        }

        public DbSet<Customer> Customers { get; set; }
    }
}
