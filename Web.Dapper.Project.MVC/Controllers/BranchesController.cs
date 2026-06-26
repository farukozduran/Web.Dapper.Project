using Microsoft.AspNetCore.Mvc;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class BranchesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
