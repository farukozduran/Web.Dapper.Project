using Microsoft.AspNetCore.Mvc;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepo;

        public CustomersController(ICustomerRepository customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IActionResult> Index()
        {
            var customers = _customerRepo.GetAllAsync();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult>Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _customerRepo.InsertCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = _customerRepo.GetCustomerByIdAsync(id);
            if(customer is null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if(ModelState.IsValid) 
            {
                await _customerRepo.UpdateCustomerAsync(customer);
                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerRepo.GetCustomerByIdAsync(id);
            if (customer is null)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerRepo.DeleteCustomerAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
