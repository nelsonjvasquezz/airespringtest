using DataAccess.Entities;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DataAccess.Services
{
    /// <summary>
    /// <see cref="EmployeeService"/> implementation
    /// </summary>
    public class EmployeeService : IEmployeeService
    {
        /// <summary>
        /// Database Connection
        /// </summary>
        private readonly DbConnection _dbConnection;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeeService"/>
        /// </summary>
        /// <param name="dbConnection">Database Connection</param>
        public EmployeeService(IOptions<DbConnection> dbConnection)
        {
            _dbConnection = dbConnection.Value;
        }

        /// <inheritdoc />
        public virtual async Task<IEnumerable<Employee>> GetAllEmployeesAsync(string lastOrFirstName)
        {
            var employeeList = new List<Employee>();
            var query = new StringBuilder("SELECT * FROM dbo.emp_employees ");

            if (!string.IsNullOrEmpty(lastOrFirstName))
            {
                query.Append("WHERE emp_employee_first_name LIKE @last_or_first_name OR emp_employee_last_name LIKE @last_or_first_name");
            }

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = query.ToString(),
                };

                if (!string.IsNullOrEmpty(lastOrFirstName))
                {
                    cmd.Parameters.AddWithValue("@last_or_first_name", $"%{lastOrFirstName}%");
                }

                using (var result = await cmd.ExecuteReaderAsync())
                {
                    while (await result.ReadAsync())
                    {
                        employeeList.Add(new Employee
                        {
                            EmployeeID = Convert.ToInt32(result["emp_employee_id"]),
                            EmployeeFirstName = result["emp_employee_first_name"].ToString(),
                            EmployeeLastName = result["emp_employee_last_name"].ToString(),
                            EmployeePhone = result["emp_employee_phone"].ToString(),
                            EmployeeZip = result["emp_employee_zip"].ToString(),
                            HireDate = Convert.ToDateTime(result["emp_hire_date"]),
                        });
                    }
                }

                cmd.Parameters.Clear();
                connection.Close();
            }

            return employeeList;
        }

        /// <inheritdoc />
        public virtual async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            Employee employee = null;

            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT * FROM dbo.emp_employees WHERE emp_employee_id = @id"
                };

                cmd.Parameters.AddWithValue("@id", id);

                using (var result = await cmd.ExecuteReaderAsync())
                {
                    if (await result.ReadAsync())
                    {
                        employee = new Employee
                        {
                            EmployeeID = Convert.ToInt32(result["emp_employee_id"]),
                            EmployeeFirstName = result["emp_employee_first_name"].ToString(),
                            EmployeeLastName = result["emp_employee_last_name"].ToString(),
                            EmployeePhone = result["emp_employee_phone"].ToString(),
                            EmployeeZip = result["emp_employee_zip"].ToString(),
                            HireDate = Convert.ToDateTime(result["emp_hire_date"]),
                        };
                    }
                }

                cmd.Parameters.Clear();
                connection.Close();
            }

            return employee;
        }

        /// <inheritdoc />
        public virtual async Task CreateEmployeeAsync(Employee employee)
        {
            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "INSERT INTO dbo.emp_employees " +
                                  "(emp_employee_first_name, emp_employee_last_name, emp_employee_phone, emp_employee_zip, emp_hire_date)" +
                                  "VALUES (@first_name, @last_name, @phone, @zip, @hire_date)"
                };

                cmd.Parameters.AddWithValue("@first_name", employee.EmployeeFirstName);
                cmd.Parameters.AddWithValue("@last_name", employee.EmployeeLastName);
                cmd.Parameters.AddWithValue("@phone", employee.EmployeePhone);
                cmd.Parameters.AddWithValue("@zip", employee.EmployeeZip);
                cmd.Parameters.AddWithValue("@hire_date", employee.HireDate);

                _ = await cmd.ExecuteNonQueryAsync();

                cmd.Parameters.Clear();
                connection.Close();
            }
        }

        /// <inheritdoc />
        public virtual async Task UpdateEmployeeAsync(Employee employee)
        {
            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE dbo.emp_employees " +
                                  "SET emp_employee_first_name = @first_name, emp_employee_last_name = @last_name, emp_employee_phone = @phone, " +
                                  "emp_employee_zip = @zip, emp_hire_date = @hire_date " +
                                  "WHERE emp_employee_id = @id"
                };

                cmd.Parameters.AddWithValue("@first_name", employee.EmployeeFirstName);
                cmd.Parameters.AddWithValue("@last_name", employee.EmployeeLastName);
                cmd.Parameters.AddWithValue("@phone", employee.EmployeePhone);
                cmd.Parameters.AddWithValue("@zip", employee.EmployeeZip);
                cmd.Parameters.AddWithValue("@hire_date", employee.HireDate);
                cmd.Parameters.AddWithValue("@id", employee.EmployeeID);

                _ = await cmd.ExecuteNonQueryAsync();

                cmd.Parameters.Clear();
                connection.Close();
            }
        }

        /// <inheritdoc />
        public virtual async Task DeleteEmployeeAsync(int id)
        {
            using (var connection = new SqlConnection(_dbConnection.ConnectionString))
            {
                connection.Open();
                var cmd = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "DELETE FROM dbo.emp_employees WHERE emp_employee_id = @id"
                };
                cmd.Parameters.AddWithValue("@id", id);

                _ = await cmd.ExecuteNonQueryAsync();

                cmd.Parameters.Clear();
                connection.Close();
            }
        }
    }
}
