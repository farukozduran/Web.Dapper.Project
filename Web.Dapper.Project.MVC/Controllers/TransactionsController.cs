using Microsoft.AspNetCore.Mvc;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class TransactionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
