using Microsoft.AspNetCore.Mvc;
using Web.Dapper.Project.MVC.Data.Repositories;

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
    }
}
