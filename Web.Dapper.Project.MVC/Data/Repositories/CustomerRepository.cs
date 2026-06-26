using Dapper;
using Microsoft.Data.SqlClient;
using Web.Dapper.Project.MVC.Context;
using Web.Dapper.Project.MVC.Data.Interfaces;
using Web.Dapper.Project.MVC.Models;

namespace Web.Dapper.Project.MVC.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DapperContext _context;

        public CustomerRepository(DapperContext context)
        {
            _context = context;
        }

        public Customer? Login(string identityNumber, string password)
        {
            Customer? customer = null;

            using (var conn = _context.CreateSqlConnection())
            {
                string query = "SELECT Id, IdentityNumber, FirstName, LastName," +
                    "PasswordHash, Email, CreatedAt FROM Customers WHERE IdentityNumber = " +
                    "@IdentityNumber AND PasswordHash = @PasswordHash";

                using (var command = new SqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@IdentityNumber", identityNumber);
                    command.Parameters.AddWithValue("Password", password);

                    conn.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            customer = new Customer
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                IdentityNumber = reader.GetString(reader.GetOrdinal("IdentityNumber")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                        }
                    }
                }
            }
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.GetAllAsync<Customer>("sp_GetAllCustomers");
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            return await _context.GetByIdAsync<Customer>("sp_GetCustomerById", parameters);
        }

        public async Task<int> InsertCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("IdentityNumber", customer.IdentityNumber);
            parameters.Add("FirstName", customer.FirstName);
            parameters.Add("LastName", customer.LastName);
            parameters.Add("PasswordHash", customer.PasswordHash);
            parameters.Add("Email", customer.Email);

            return await _context.ExecuteScalarAsync<int>("sp_InsertCustomer", parameters);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", customer.Id);
            parameters.Add("IdentityNumber", customer.IdentityNumber);
            parameters.Add("FirstName", customer.FirstName);
            parameters.Add("LastName", customer.LastName);
            parameters.Add("PasswordHash", customer.PasswordHash);
            parameters.Add("Email", customer.Email);

            await _context.ExecuteAsync("sp_UpdateCustomer", parameters);
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);

            await _context.ExecuteAsync("sp_DeleteCustomer", parameters);
        }
}
}