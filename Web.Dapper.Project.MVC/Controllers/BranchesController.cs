using Microsoft.AspNetCore.Mvc;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Controllers
{
    public class BranchesController : Controller
    {
        private readonly IBranchRepository _branchRepo;

        public BranchesController(IBranchRepository branchRepo)
        {
            _branchRepo = branchRepo;
        }

        public async Task<IActionResult> GetAllAsync()
        {
            var branches = _branchRepo.GetAllBranchesAsync();
            return View(branches);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                await _branchRepo.InsertBranchAsync(branch);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var branch = _branchRepo.GetBranchById(id);

            if (branch is null)
            {
                return NotFound();
            }

            return View(branch);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Branch branch)
        {
            if (ModelState.IsValid)
            {
                await _branchRepo.UpdateBranchAsync(branch);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var branch = _branchRepo.GetBranchById(id);

            if (branch is null)
            {
                return NotFound();
            }

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _branchRepo.DeleteBranchAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
