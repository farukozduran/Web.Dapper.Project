using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Interfaces
{
    public interface ICustomerRepository
    {
        Customer? Login(string identityNumber, string password);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<int> InsertCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int id);
    }
}
