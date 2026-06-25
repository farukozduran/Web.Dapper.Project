using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Web.Dapper.Project.MVC.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration, string connectionString)
        {
            _configuration = configuration;
            _connectionString = _connectionString = _configuration.GetConnectionString("DefaultConnection")
                                                    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>(string spName)
        {
            using (var conn = CreateConnection())
            {
                return await conn.QueryAsync<T>(spName, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<T?> GetByIdAsync<T>(string spName, DynamicParameters parameters)
        {
            using (var conn = CreateConnection())
            {
                return await conn.QuerySingleOrDefaultAsync<T>(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task<int> ExecuteAsync(string spName, DynamicParameters parameters)
        {
            using (var conn = CreateConnection())
            {
                return await conn.ExecuteAsync(spName, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        // TO-DO
    }
}
