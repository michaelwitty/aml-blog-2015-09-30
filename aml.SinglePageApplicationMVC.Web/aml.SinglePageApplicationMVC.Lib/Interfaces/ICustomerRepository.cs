using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aml.SinglePageApplicationMVC.Lib
{
    public interface ICustomerRepository : IRepository<Customer,int>
    {
    }
}
