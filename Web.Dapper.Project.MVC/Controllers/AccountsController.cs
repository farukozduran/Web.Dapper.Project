using Microsoft.AspNetCore.Mvc;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class AccountsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
