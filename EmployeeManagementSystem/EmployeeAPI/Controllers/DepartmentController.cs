using Microsoft.AspNetCore.Mvc;
using BLL.Services;
using System.Collections.Generic;
using DAL.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentService _departmentService;

        public DepartmentController(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // Get all departments
        [HttpGet("getDepartments")]
        public IActionResult GetDepartments()
        {
            try
            {
                var departments = _departmentService.GetDepartmentList();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Get department by ID
        [HttpGet("getDepartment/{id}")]
        public IActionResult GetDepartmentById(int id)
        {
            try
            {
                var department = _departmentService.GetDepartmentById(id);
                if (department == null)
                    return NotFound("Department not found");

                return Ok(department);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Update department
        [HttpPut("updateDepartment")]
        [Consumes("application/json")]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            try
            {
                if (department == null)
                    return BadRequest("Department data is null");

                var existingDepartment = _departmentService.GetDepartmentById(department.DeptNo);
                if (existingDepartment == null)
                    return NotFound("Department not found");

                _departmentService.UpdateDepartment(department);
                return Ok("Department updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Delete department
        [HttpDelete("deleteDepartment/{id}")]
        public IActionResult DeleteDepartment(int id)
        {
            try
            {
                var department = _departmentService.GetDepartmentById(id);
                if (department == null)
                    return NotFound("Department not found");

                _departmentService.DeleteDepartment(id);
                return Ok("Department deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
