using aml.SinglePageApplicationMVC.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace aml.SinglePageApplicationMVC.Web.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerRepository customerRepository;

        public CustomerController()
        {
            this.customerRepository = new CustomerRepository(new CustomerDbContext());
        }

        public CustomerController(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        // GET: Home
        public ActionResult Index()
        {
            var model = this.customerRepository.FindAll();

            return View(model);
        }

        public ActionResult IndexTemplate()
        {
            var model = Enumerable.Empty<Customer>();

            return PartialView("~/Views/Partials/_ShowCustomers.cshtml", model);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult CreateTemplate()
        {
            return PartialView("~/Views/Partials/_CreateCustomer.cshtml");
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customerRepository.Create(customer);
                    customerRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Could not save customer");
            }
            return View(customer);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            var model = this.customerRepository.Get(id);

            return View(model);
        }

        public ActionResult EditTemplate()
        {
            var model = new Customer();

            return PartialView("~/Views/Partials/_EditCustomer.cshtml", model);
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    customerRepository.Update(customer);
                    customerRepository.Save();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Could not save customer");
            }
            return View(customer);
        }
    }
}