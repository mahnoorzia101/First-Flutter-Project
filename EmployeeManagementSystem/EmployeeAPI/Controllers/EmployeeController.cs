using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Get all employees
        [HttpGet("getEmployee")]
        public IActionResult GetEmployees()
        {
            try
            {
                List<Employee> employees = _employeeService.GetEmployeeList();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get employee by ID
        [HttpGet("getEmployee/{id}")]
        public IActionResult GetEmployeeById(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return NotFound("Employee not found");

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update employee
        [HttpPut("updateEmployee")]
        [Consumes("application/json")]
        public IActionResult UpdateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest("Employee data is null");

                var existingEmployee = _employeeService.GetEmployeeById(employee.Empno);
                if (existingEmployee == null)
                    return NotFound("Employee not found");

                _employeeService.UpdateEmployee(employee);
                return Ok("Employee updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete employee
        [HttpDelete("deleteEmployee/{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(id);
                if (employee == null)
                    return NotFound("Employee not found");

                _employeeService.DeleteEmployee(id);
                return Ok("Employee deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
