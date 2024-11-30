using DAL.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace DAL.Repositories
{
    public class EmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Get all employees
        public List<Employee> GetEmployees()
        {
            var employees = new List<Employee>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetEmployees", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            Empno = reader.GetInt32(reader.GetOrdinal("empno")),  // Ensure correct mapping to Int32
                            Ename = reader.GetString(reader.GetOrdinal("ename")),  // Ensure correct mapping to string
                            Job = reader.GetString(reader.GetOrdinal("job")),  // Ensure correct mapping to string
                            Mgr = reader.IsDBNull(reader.GetOrdinal("mgr")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("mgr")),  // Nullable Int32 for mgr
                            Hiredate = reader.GetDateTime(reader.GetOrdinal("hiredate")),  // DateTime mapping
                            Sal = reader.GetDecimal(reader.GetOrdinal("sal")),  // Decimal mapping
                            Comm = reader.IsDBNull(reader.GetOrdinal("comm")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("comm")),  // Nullable decimal for comm
                            Deptno = reader.GetInt32(reader.GetOrdinal("deptno"))  // Ensure correct mapping to Int32
                        });
                    }
                }
            }
            return employees;
        }

        // Get employee by ID
        public Employee GetEmployeeById(int empNo)
        {
            Employee employee = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("GetEmployeeById", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpNo", empNo);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        employee = new Employee
                        {
                            Empno = reader.GetInt32(reader.GetOrdinal("empno")),  // Ensure correct mapping to Int32
                            Ename = reader.GetString(reader.GetOrdinal("ename")),  // Ensure correct mapping to string
                            Job = reader.GetString(reader.GetOrdinal("job")),  // Ensure correct mapping to string
                            Mgr = reader.IsDBNull(reader.GetOrdinal("mgr")) ? (int?)null : reader.GetInt32(reader.GetOrdinal("mgr")),  // Nullable Int32 for mgr
                            Hiredate = reader.GetDateTime(reader.GetOrdinal("hiredate")),  // DateTime mapping
                            Sal = reader.GetDecimal(reader.GetOrdinal("sal")),  // Decimal mapping
                            Comm = reader.IsDBNull(reader.GetOrdinal("comm")) ? (decimal?)null : reader.GetDecimal(reader.GetOrdinal("comm")),  // Nullable decimal for comm
                            Deptno = reader.GetInt32(reader.GetOrdinal("deptno"))  // Ensure correct mapping to Int32
                        };
                    }
                }
            }
            return employee;
        }

        // Update employee
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpNo", employee.Empno);
                cmd.Parameters.AddWithValue("@Ename", employee.Ename);
                cmd.Parameters.AddWithValue("@Job", employee.Job);
                cmd.Parameters.AddWithValue("@Mgr", employee.Mgr);
                cmd.Parameters.AddWithValue("@Hiredate", employee.Hiredate);
                cmd.Parameters.AddWithValue("@Sal", employee.Sal);
                cmd.Parameters.AddWithValue("@Comm", employee.Comm);
                cmd.Parameters.AddWithValue("@DeptNo", employee.Deptno);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete employee
        public void DeleteEmployee(int empNo)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteEmployee", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@EmpNo", empNo);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
