using Dapper;
using Web.Dapper.Project.MVC.Context;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Repositories
{
    public class AccountRepository
    {
        private readonly DapperContext _context;

        public AccountRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.GetAllAsync<Account>("sp_GetAllAccounts");
        }

        public async Task<Account?> GetAccountByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            return await _context.GetByIdAsync<Account>("sp_GetAccountById", parameters);
        }

        public async Task<int> InsertAccountAsync(Account account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountNumber", account.AccountNumber);
            parameters.Add("Balance", account.Balance);
            parameters.Add("BranchId", account.BranchId);
            parameters.Add("Currency", account.Currency);
            parameters.Add("CustomerId", account.CustomerId);
            parameters.Add("IsActive", account.IsActive);

            return await _context.ExecuteScalarAsync<int>("sp_InsertAccount", parameters);
        }

        public async Task UpdateAccountAsync(Account account)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", account.Id);
            parameters.Add("AccountNumber", account.AccountNumber);
            parameters.Add("Balance", account.Balance);
            parameters.Add("BranchId", account.BranchId);
            parameters.Add("Currency", account.Currency);
            parameters.Add("CustomerId", account.CustomerId);
            parameters.Add("IsActive", account.IsActive);

            await _context.ExecuteAsync("sp_UpdateAccount", parameters);
        }

        public async Task DeleteAccountAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            await _context.ExecuteAsync("sp_DeleteAccount", parameters);
        }
    }
}
