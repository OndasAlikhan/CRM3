using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM3.Models;
using System.Diagnostics;
using CRM3.Data;

namespace CRM3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CRMContext _context;

        public CustomerController(CRMContext context)
        {
            _context = context;
        }
        // GET: Customer
        public ActionResult Index(int id)
        {
            ViewData["CustomerList"] = _context.Customers.ToList<Customer>();
            return View();
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return Redirect("~/Home");
            }

            ViewData["Filials"] = _context.Filials.ToList<Filial>();
            ViewData["Customer"] = _context.Customers.SingleOrDefault(c => c.ID == id) as Customer;
            ViewData["CustomerAccountList"] = _context.CustomerAccounts.Where(c => c.CustomerId == id).ToList<CustomerAccount>();
            return View();
        }

        public IActionResult Filial()
        {
            ViewData["Filials"] = _context.Filials.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult CreateFilial(IFormCollection collection)
        {
            _context.Filials.Add(new Filial {Name = collection["Name"]});
            _context.SaveChanges();
            return Redirect("/Customer/Filial");
        }
        // GET: Customer/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                _context.Customers.Add(new Customer {FullName = collection["FullName"], Phone = collection["Phone"]});
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}