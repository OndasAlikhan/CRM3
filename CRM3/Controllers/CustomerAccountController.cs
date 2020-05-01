using System.Collections.Generic;
using System.Linq;
using CRM3.Models;
using CRM3.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using SQLitePCL;

namespace CRM3.Controllers
{
    public class CustomerAccountController : Controller
    {
        private readonly CRMContext _context;

        public CustomerAccountController(CRMContext context)
        {
            _context = context;
        }
        // GET
        public IActionResult Index(int custaccid)
        {
            var caProd = _context.CustomerAccountProducts.Where(c => c.CustomerAccoountId == custaccid).ToList();
            List<Product> products = caProd
                .Select(i => _context.Products.Where(p => p.ID == i.ProductId).SingleOrDefault()).ToList();
            var custAcc = _context.CustomerAccounts.SingleOrDefault(c => c.ID == custaccid);
            var customer = _context.Customers.SingleOrDefault(c => c.ID == custAcc.CustomerId);
            var filial = _context.Filials.SingleOrDefault(c => c.ID == custAcc.FilialId) as Filial;

            ViewData["CurrentFilial"] = filial;
            ViewData["Customer"] = customer;
            ViewData["CurrentCustomerAccount"] = custAcc;
            ViewData["Products"] = products;
            
            return View();
        }

        public IActionResult Create(int custid)
        {
            // _context.CustomerAccounts.Add(new CustomerAccount{})
            ViewData["Filials"] = _context.Filials.ToList();
            ViewData["CurrentCustomer"] = _context.Customers.SingleOrDefault(c => c.ID == custid);
            return View();
        }

        [HttpPost]
        public IActionResult Create(IFormCollection collection)
        {

            int filialId;
            int.TryParse(collection["Filial"], out filialId);
            int customerId;
            int.TryParse(collection["CustomerId"], out customerId);
            var customerAccount = new CustomerAccount { CustomerId = customerId, FilialId = filialId };
            if (!TryValidateModel(customerAccount))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.CustomerAccounts.Add(customerAccount);
            _context.SaveChanges();
            return Redirect("/Customer/Details?id="+customerId);
        }

        [HttpPost]
        public IActionResult Delete(IFormCollection collection)
        {
            int cId;
            int.TryParse(collection["custacc"], out cId);

            var cA = new CustomerAccount { ID = cId };
            if (!TryValidateModel(cA))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.CustomerAccounts.Remove(cA);
            _context.SaveChanges();
            return Redirect("/Customer/Details?id="+collection["customerId"]);

        }

        [HttpPost]
        public IActionResult CreateProduct(IFormCollection collection)
        {
            var newProduct = new Product {Name = collection["Name"]};
            _context.Products.Add(newProduct);
            _context.SaveChanges();

            int caId;
            int.TryParse(collection["custacc"], out caId);
            var cap = new CustomerAccountProduct{ ProductId = newProduct.ID, CustomerAccoountId = caId };
            if (!TryValidateModel(cap))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.CustomerAccountProducts.Add(cap);
            _context.SaveChanges();
            return Redirect("/CustomerAccount/Index?custaccid="+caId);
        }

        [HttpPost]
        public IActionResult DeleteProduct(IFormCollection collection)
        {
            int prodId;
            int.TryParse(collection["prodId"], out prodId);

            var p = new Product { ID = prodId };
            if (!TryValidateModel(p))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.Products.Remove(p);
            _context.SaveChanges();
            return Ok();
        }
    }
}