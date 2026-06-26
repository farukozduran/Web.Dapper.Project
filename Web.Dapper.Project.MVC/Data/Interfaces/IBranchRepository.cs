using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Interfaces
{
    public interface IBranchRepository
    {
        Task<IEnumerable<Branch>> GetAllBranchesAsync();
        Task<Branch?> GetBranchById(int id);
        Task<int> InsertBranchAsync(Branch branch);
        Task UpdateBranchAsync(Branch branch);
        Task DeleteBranchAsync(int id);
    }
}
