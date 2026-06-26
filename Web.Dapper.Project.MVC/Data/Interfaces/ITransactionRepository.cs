using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllAsync();
        Task<Transaction?> GetTransactionByIdAsync(int id);
        Task<int> InsertTransactionAsync(Transaction transaction);
        Task UpdateTransactionAsync(Transaction transaction);
        Task DeleteTransactionAsync(int id);
    }
}