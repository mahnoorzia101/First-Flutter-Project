using DAL.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class DepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all departments
        public List<Department> GetDepartments()
        {
            var departments = new List<Department>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDepartment", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        departments.Add(new Department
                        {
                            DeptNo = reader.GetInt32(0),
                            DName = reader.GetString(1),
                            Loc = reader.GetString(2)
                        });
                    }
                }
            }
            return departments;
        }

        // Get a department by ID
        public Department GetDepartmentById(int deptNo)
        {
            Department department = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetDepartmentById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DeptNo", deptNo);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            DeptNo = reader.GetInt32(0),
                            DName = reader.GetString(1),
                            Loc = reader.GetString(2)
                        };
                    }
                }
            }
            return department;
        }

        // Update a department
        public void UpdateDepartment(Department department)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateDepartment", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DeptNo", department.DeptNo);
                cmd.Parameters.AddWithValue("@Dname", department.DName);
                cmd.Parameters.AddWithValue("@Loc", department.Loc);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a department
        public void DeleteDepartment(int deptNo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteDepartment", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@DeptNo", deptNo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
