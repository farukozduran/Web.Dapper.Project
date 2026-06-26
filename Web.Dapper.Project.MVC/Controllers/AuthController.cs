using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Dapper.Project.MVC.Data.Repositories;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly CustomerRepository _customerRepository;
        // Login metodumuz CustomerRepository içinde olduğu için onu DI ile alıyoruz
        public AuthController(CustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if(User.Identity!= null && User.Identity.IsAuthenticated) 
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerRepository.Login(model.IdentityNumber, model.Password);

                if (customer is null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                        new Claim(ClaimTypes.Name, customer.FirstName),
                        new Claim(ClaimTypes.Surname, customer.LastName),
                        new Claim(ClaimTypes.Email, customer.Email)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Gecersiz kimlik numarasi veya sifre.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");   
        }
    }
}}
