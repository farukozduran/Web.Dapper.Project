using Dapper;
using Web.Dapper.Project.MVC.Context;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Repositories
{
    public class TransactionRepository
    {
        private readonly DapperContext _context;

        public TransactionRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.GetAllAsync<Transaction>("sp_GetAllTransactions");
        }

        public async Task<Transaction?> GetTransactionByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            return await _context.GetByIdAsync<Transaction?>("sp_GetTransactionById", parameters);
        }

        public async Task<int> InsertTransactionAsync(Transaction transaction) 
        {
            var parameters = new DynamicParameters();
            parameters.Add("AccountId", transaction.AccountId);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("TransactionDate", transaction.TransactionDate);
            parameters.Add("TransactionType", transaction.TransactionType);

            return await _context.ExecuteScalarAsync<int>("sp_InsertTransaction", parameters);
        }

        public async Task UpdateTransactionAsync(Transaction transaction) 
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", transaction.Id);
            parameters.Add("AccountId", transaction.AccountId);
            parameters.Add("Amount", transaction.Amount);
            parameters.Add("TransactionDate", transaction.TransactionDate);
            parameters.Add("TransactionType", transaction.TransactionType);

            await _context.ExecuteAsync("sp_UpdateTransaction", parameters);
        }

        public async Task DeleteTransactionAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            await _context.ExecuteAsync("sp_DeleteTransaction", parameters);
        }
    }
}
