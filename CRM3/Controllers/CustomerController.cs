using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRM3.Models;
using System.Diagnostics;
using CRM3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CRM3.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CRMContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        public CustomerController(CRMContext context, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        // GET: Customer
        public ActionResult Index(int id)
        {
            ViewData["CustomerList"] = _context.Customers.ToList<Customer>();
            return View();
        }

        // GET: Customer/Details/5
        [Authorize]
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
            var NewFilial = new Filial { Name = collection["Name"] };
            if (!TryValidateModel(NewFilial))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.Filials.Add(NewFilial);
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

                var customer = new Customer { FullName = collection["FullName"], Phone = collection["Phone"] };
                if (!TryValidateModel(customer))
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                _context.Customers.Add(customer);
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
            ViewData["EditCustomer"] = _context.Customers.SingleOrDefault(c => c.ID == id) as Customer;
            return View();
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                int cId;
                int.TryParse(collection["ID"], out cId);
                var customerEdit = _context.Customers.FirstOrDefault<Customer>(c => c.ID == cId);
                if (!TryValidateModel(customerEdit))
                {
                    return View("~/Views/Shared/Error.cshtml");
                }
                customerEdit.FullName = collection["FullName"];
                customerEdit.Phone = collection["Phone"];
                _context.SaveChanges();
                
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete
        [HttpPost]
        public ActionResult Delete(IFormCollection collection)
        {

            // TODO: Add delete logic here
            int deleteId;
            int.TryParse(collection["custId"], out deleteId);

            Customer customer = _context.Customers.SingleOrDefault(c => c.ID == deleteId) as Customer;
            if (!TryValidateModel(customer))
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
            
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyName(string FullName)
        {
            var customerFound = _context.Customers.SingleOrDefault(c => c.FullName == FullName) as Customer;
            if (customerFound != null)
            {
                return Json($"Name {FullName} is already in use.");
            }

            return Json(true);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            var user = await _userManager.FindByNameAsync(Email);
            if (user != null)
            {

                //sign in logic
                var signInResult = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> MakeAdmin()
        {
            var admin = await _userManager.FindByNameAsync("Admin");
            if (admin == null)
            {
                admin = new User
                {
                    UserName = "Admin",
                    Password = "Admin"
                };
                await _userManager.CreateAsync(admin);
                admin = await _userManager.FindByNameAsync("Admin");
            }

            await _userManager.AddToRoleAsync(admin, "Admin");
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Register(string Username, string Password)
        {

            var user = new User
            {
                UserName = Username
            };
            var result = await _userManager.CreateAsync(user, Password);
            var currentUser = await _userManager.FindByNameAsync(user.UserName);
            var roleresult = await _userManager.AddToRoleAsync(currentUser, "User");

            if (result.Succeeded)
            {
                // sign up logics
                var signInResult = await _signInManager.PasswordSignInAsync(user, Password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index");
        }
    }
}