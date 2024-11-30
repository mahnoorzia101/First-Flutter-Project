using DAL.Models;
using DAL.Repositories;
using System.Collections.Generic;

namespace BLL.Services
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;

        public DepartmentService(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        // Method to get all departments
        public List<Department> GetDepartmentList()
        {
            return _departmentRepository.GetDepartments();
        }

        // Method to get a department by ID (DeptNo)
        public Department GetDepartmentById(int deptNo)
        {
            return _departmentRepository.GetDepartmentById(deptNo);
        }

        // Method to update department
        public void UpdateDepartment(Department department)
        {
            _departmentRepository.UpdateDepartment(department);
        }

        // Method to delete department
        public void DeleteDepartment(int deptNo)
        {
            _departmentRepository.DeleteDepartment(deptNo);
        }
    }
}
