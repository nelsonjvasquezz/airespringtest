namespace DataAccess.Entities
{
    /// <summary>
    /// Contains the data of employees
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Unique identifier of the employee
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Last name of the employee
        /// </summary>
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// First name of the employee
        /// </summary>
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Telephone number of the employee
        /// </summary>
        public string EmployeePhone { get; set; }

        /// <summary>
        /// Postal Code of the employee
        /// </summary>
        public string EmployeeZip { get; set; }

        /// <summary>
        /// Date of hire of the employee
        /// </summary>
        public DateTime HireDate { get; set; }
    }
}
