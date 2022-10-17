using AireSpringWeb.Models;
using AutoMapper;
using DataAccess.Entities;
using DataAccess.Services;
using Microsoft.AspNetCore.Mvc;

namespace AireSpringWeb.Controllers
{
    /// <summary>
    /// Controller handling employee related actions
    /// </summary>
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Service of Employee
        /// </summary>
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Object-object mapping
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="EmployeeController"/>
        /// </summary>
        /// <param name="employeeService">Service of Employee</param>
        /// <param name="mapper">Object-object mapping</param>
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Employee, EmployeeViewModel>();
            }));
        }

        // GET: EmployeeController

        /// <summary>
        /// Returns the full list of employees
        /// </summary>
        /// <returns>List of <see cref="Employee"/></returns>
        public async Task<ActionResult> IndexAsync()
        {
            var employeeList = await _employeeService.GetAllEmployeesAsync();
            var employeeViewModelList = _mapper.Map<List<EmployeeViewModel>>(employeeList);
            return View("Index", employeeViewModelList);
        }

        // GET: EmployeeController/Details/5

        /// <summary>
        /// Returns the information of the requested employee
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <returns>Requested <see cref="Employee"/></returns>
        public async Task<ActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View(nameof(Details), employeeViewModel);
        }

        // GET: EmployeeController/Create

        /// <summary>
        /// Shows the form to enter a new employee
        /// </summary>
        /// <returns>View with the form</returns>
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeController/Create

        /// <summary>
        /// Inserts a new employee
        /// </summary>
        /// <param name="collection">New employee information form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            var employee = new Employee
            {
                EmployeeFirstName = collection["EmployeeFirstName"],
                EmployeeLastName = collection["EmployeeLastName"],
                EmployeePhone = collection["EmployeePhone"],
                EmployeeZip = collection["Employeezip"],
                HireDate = Convert.ToDateTime(collection["HireDate"]),
            };

            await _employeeService.CreateEmployeeAsync(employee);

            return RedirectToAction("Index");
        }

        // GET: EmployeeController/Edit/5
        public async Task<ActionResult> EditAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            var employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);
            return View("Edit", employeeViewModel);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, IFormCollection collection)
        {
            var employee = new Employee
            {
                EmployeeID = id,
                EmployeeFirstName = collection["EmployeeFirstName"],
                EmployeeLastName = collection["EmployeeLastName"],
                EmployeePhone = collection["EmployeePhone"],
                EmployeeZip = collection["Employeezip"],
                HireDate = Convert.ToDateTime(collection["HireDate"]),
            };

            await _employeeService.UpdateEmployeeAsync(employee);

            return RedirectToAction("Details", new { id });
        }

        // GET: EmployeeController/Delete/5
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            return View("Delete", employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id, IFormCollection collection)
        {
            try
            {
                await _employeeService.DeleteEmployeeAsync(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
