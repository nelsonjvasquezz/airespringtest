using System.ComponentModel.DataAnnotations;

namespace AireSpringWeb.Models
{
    /// <summary>
    /// Model for create a new employee
    /// </summary>
    public class EmployeeCreateViewModel
    {
        /// <summary>
        /// Last name of the employee
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(100)]
        public string EmployeeLastName { get; set; }

        /// <summary>
        /// First name of the employee
        /// </summary>
        [Required(ErrorMessage = "Please enter your first name")]
        [StringLength(100)]
        public string EmployeeFirstName { get; set; }

        /// <summary>
        /// Telephone number of the employee
        /// </summary>
        [Required(ErrorMessage = "Please enter your phone number")]
        [Phone]
        public string EmployeePhone { get; set; }

        /// <summary>
        /// Postal Code of the employee
        /// </summary>
        [Required(ErrorMessage = "Please enter your last name")]
        [StringLength(10)]
        public string EmployeeZip { get; set; }

        /// <summary>
        /// Date of hire of the employee
        /// </summary>
        public DateTime HireDate { get; set; }
    }

    /// <summary>
    /// Model for show or update an employee
    /// </summary>
    public class EmployeeViewModel : EmployeeCreateViewModel
    {
        /// <summary>
        /// Id of employee
        /// </summary>
        public int EmployeeID { get; set; }
    }
}
