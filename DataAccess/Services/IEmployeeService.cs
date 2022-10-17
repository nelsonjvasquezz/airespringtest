using DataAccess.Entities;

namespace DataAccess.Services
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Inserts a new record of employee
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns></returns>
        Task CreateEmployeeAsync(Employee employee);

        /// <summary>
        /// Deletes the specified employee by id
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        Task DeleteEmployeeAsync(int id);

        /// <summary>
        /// Gets the full list of employees
        /// </summary>
        /// <returns>List of <see cref="Employee"/></returns>
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        /// <summary>
        /// Gets the specified employee by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Requested <see cref="Employee"/></returns>
        Task<Employee> GetEmployeeByIdAsync(int id);

        /// <summary>
        /// Update the specified employee by id
        /// </summary>
        /// <param name="employee">Employee data</param>
        /// <returns></returns>
        Task UpdateEmployeeAsync(Employee employee);
    }
}
