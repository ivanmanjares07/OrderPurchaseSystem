using OrderPurchaseSystem.Models;
using OrderPurchaseSystem.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderPurchaseSystem.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            CustomerRepository custRepo = new CustomerRepository();
            var customers = custRepo.GetCustomers();

            return View(customers);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(customer);
                }

                CustomerRepository custRepo = new CustomerRepository();
                custRepo.Insert(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            CustomerRepository custRepo = new CustomerRepository();
            var customer = custRepo.GetCustomers().Find(m => m.Id == id);

            return View(customer);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                CustomerRepository custRepo = new CustomerRepository();
                custRepo.Update(customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                CustomerRepository custRepo = new CustomerRepository();
                custRepo.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
     
    }
}
