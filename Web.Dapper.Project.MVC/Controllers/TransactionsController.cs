using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepo;

        public TransactionsController(IAccountRepository accountRepository, ITransactionRepository transactionRepo)
        {
            _accountRepository = accountRepository;
            _transactionRepo = transactionRepo;
        }

        public async Task<IActionResult> Index()
        {
            var transactions = _transactionRepo.GetAllAsync();
            return View(transactions);
        }

        public async Task<IActionResult> Create()
        {
            await PopulateDropdown();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                await _transactionRepo.InsertTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdown();
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var transaction = await _transactionRepo.GetTransactionByIdAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            await PopulateDropdown(); 
            return View(transaction);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                await _transactionRepo.UpdateTransactionAsync(transaction);
                return RedirectToAction(nameof(Index));
            }

            await PopulateDropdown();
            return View(transaction);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var transaction = await _transactionRepo.GetTransactionByIdAsync(id);

            if (transaction is null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _transactionRepo.DeleteTransactionAsync(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdown()
        {
            var accounts = await _accountRepository.GetAllAsync();

            // SelectList oluştururken:
            // 1. parametre: Veri listesi
            // 2. parametre ("Id"): HTML 'value' kısmına gidecek alan (Arka planda kaydedilecek olan)
            // 3. parametre ("FirstName" / "BranchName"): HTML'de kullanıcının gözüyle göreceği metin
            ViewBag.Customers = new SelectList(accounts, "Id", "AccountNumber");
        }
    }
}
