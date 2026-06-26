using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accRepo;
        private readonly ICustomerRepository _cusRepo;
        private readonly IBranchRepository _branchRepo;

        public AccountsController(IAccountRepository accRepo, ICustomerRepository cusRepo, IBranchRepository branchRepo)
        {
            _accRepo = accRepo;
            _cusRepo = cusRepo;
            _branchRepo = branchRepo;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accRepo.GetAllAsync();
            return View(accounts);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accRepo.InsertAccountAsync(account);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdowns();
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accRepo.GetAccountByIdAsync(id);

            if(account is null)
            {
                return NotFound();
            }

            await PopulateDropdowns();
            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Account account)
        {
            if (ModelState.IsValid)
            {
                await _accRepo.UpdateAccountAsync(account);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdowns();
            return View(account);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accRepo.GetAccountByIdAsync(id);
            if(account is null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _accRepo.DeleteAccountAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdowns()
        {
            var customers = await _cusRepo.GetAllAsync();
            var branches = await _branchRepo.GetAllBranchesAsync();

            // SelectList oluştururken:
            // 1. parametre: Veri listesi
            // 2. parametre ("Id"): HTML 'value' kısmına gidecek alan (Arka planda kaydedilecek olan)
            // 3. parametre ("FirstName" / "BranchName"): HTML'de kullanıcının gözüyle göreceği metin
            ViewBag.Customers = new SelectList(customers, "Id", "FirstName");
            ViewBag.Branches = new SelectList(branches, "Id", "BranchName"
        }
    }
}
