using DAL.Models;
using DAL.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Method to get list of employees
        public List<Employee> GetEmployeeList()
        {
            return _employeeRepository.GetEmployees();
        }

        // Method to get a single employee by ID
        public Employee GetEmployeeById(int empNo)
        {
            return _employeeRepository.GetEmployeeById(empNo);
        }

        // Method to update an employee
        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.UpdateEmployee(employee);
        }

        // Method to delete an employee
        public void DeleteEmployee(int empNo)
        {
            _employeeRepository.DeleteEmployee(empNo);
        }
    }
}
