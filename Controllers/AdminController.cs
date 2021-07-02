using electronics.Data;
using electronics.Models;
using electronics.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace electronics.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationContext _context;
        public AdminController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminLogin login)
        {
                if (ModelState.IsValid)
                {
                    Admin admin = _context.Admin.FirstOrDefault(a => a.Email == login.Email && a.Password == Extention.EncryptPlainTextToCipherText(login.Password));
                    if (admin != null)
                    {
                        HttpContext.Session.SetString("Email", login.Email);
                        return RedirectToAction("Index", "Product");
                    }
                }

            return View(login);
        }


        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(AdminRegister register)
        {
            if (ModelState.IsValid)
            {
                Admin admin = _context.Admin.FirstOrDefault(a => a.Email == register.Email);
                if (admin == null)
                {
                    if (register.Password.Equals(register.ConfirmPassword))
                    {
                        _context.Admin.Add(new Admin { Email = register.Email, Password = Extention.EncryptPlainTextToCipherText(register.Password) });
                        _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Login", "Admin");
                }
            }


            return View(register);

        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Login", "Admin");
        }


    }
}
