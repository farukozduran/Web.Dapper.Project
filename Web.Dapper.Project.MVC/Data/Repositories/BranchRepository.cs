using Dapper;
using Web.Dapper.Project.MVC.Context;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Repositories
{
    public class BranchRepository : IBranchRepository
    {
        private readonly DapperContext _context;

        public BranchRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Branch>> GetAllBranchesAsync()
        {
            return await _context.GetAllAsync<Branch>("sp_GetAllBranches");
        }

        public async Task<Branch?> GetBranchById(int id) 
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            return await _context.GetByIdAsync<Branch>("sp_GetBranchById", parameters);
        }

        public async Task<int> InsertBranchAsync(Branch branch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("BranchName", branch.BranchName);
            parameters.Add("City", branch.City);

            return await _context.ExecuteScalarAsync<int>("sp_InsertBranch", parameters);
        }

        public async Task UpdateBranchAsync(Branch branch)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", branch.Id);
            parameters.Add("BranchName", branch.BranchName);
            parameters.Add("City", branch.City);

            await _context.ExecuteAsync("sp_UpdateBranch", parameters);
        }

        public async Task DeleteBranchAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            await _context.ExecuteAsync("sp_DeleteBranch", parameters);
        }
    }
}
