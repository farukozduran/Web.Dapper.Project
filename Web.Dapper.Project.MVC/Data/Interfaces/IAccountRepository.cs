using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAsync();
        Task<Account?> GetAccountByIdAsync(int id);
        Task<int> InsertAccountAsync(Account account);
        Task UpdateAccountAsync(Account account);
        Task DeleteAccountAsync(int id);
    }
}
