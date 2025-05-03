using HRMgmtWeb.Data.Interfaces;
using HRMgmtWeb.Models;
using HRMgmtWeb.Services.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace HRMgmtWeb.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryAsync<Employee>("SELECT * FROM Employees");
            }
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.QueryFirstOrDefaultAsync<Employee>(
                    "SELECT * FROM Employees WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public async Task<int> CreateAsync(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO Employees 
                            (EmployeeCode, FirstName, LastName, Email, DateOfBirth, JoinDate, 
                             DepartmentId, Position, Status, PhoneNumber, Address, CreatedDate)
                            VALUES 
                            (@EmployeeCode, @FirstName, @LastName, @Email, @DateOfBirth, @JoinDate, 
                             @DepartmentId, @Position, @Status, @PhoneNumber, @Address, @CreatedDate);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

                return await db.ExecuteScalarAsync<int>(sql, employee);
            }
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sql = @"UPDATE Employees SET 
                            FirstName = @FirstName,
                            LastName = @LastName,
                            Email = @Email,
                            DateOfBirth = @DateOfBirth,
                            DepartmentId = @DepartmentId,
                            Position = @Position,
                            Status = @Status,
                            PhoneNumber = @PhoneNumber,
                            Address = @Address,
                            ModifiedDate = @ModifiedDate
                            WHERE Id = @Id";

                var affectedRows = await db.ExecuteAsync(sql, employee);
                return affectedRows > 0;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var affectedRows = await db.ExecuteAsync(
                    "DELETE FROM Employees WHERE Id = @Id",
                    new { Id = id });
                return affectedRows > 0;
            }
        }

        public async Task<bool> EmployeeExists(string employeeCode)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                return await db.ExecuteScalarAsync<bool>(
                    "SELECT 1 FROM Employees WHERE EmployeeCode = @EmployeeCode",
                    new { EmployeeCode = employeeCode });
            }
        }
    }
}
