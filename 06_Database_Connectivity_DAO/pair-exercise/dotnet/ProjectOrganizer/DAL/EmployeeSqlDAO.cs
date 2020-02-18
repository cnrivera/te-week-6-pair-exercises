using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class EmployeeSqlDAO : IEmployeeDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public EmployeeSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the employees.
        /// </summary>
        /// <returns>A list of all employees.</returns>
        public IList<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee empl = new Employee();

                        empl.EmployeeId = Convert.ToInt32(reader["employee_ID"]);
                        empl.FirstName = Convert.ToString(reader["first_name"]);
                        empl.LastName = Convert.ToString(reader["last_name"]);
                        empl.JobTitle = Convert.ToString(reader["job_title"]);
                        empl.Gender = Convert.ToString(reader["gender"]);
                        empl.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        employees.Add(empl);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting employee list");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return employees;
        }

        /// <summary>
        /// Searches the system for an employee by first name or last name.
        /// </summary>
        /// <remarks>The search performed is a wildcard search.</remarks>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <returns>A list of employees that match the search.</returns>
        public IList<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employeeSearch = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * from employee WHERE first_name LIKE @first_name AND last_name LIKE @last_name", conn);
                    cmd.Parameters.AddWithValue("@first_name", "%" + firstname + "%");
                    cmd.Parameters.AddWithValue("@last_name", "%" + lastname + "%");

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee empl = new Employee();

                        empl.EmployeeId = Convert.ToInt32(reader["employee_ID"]);
                        empl.FirstName = Convert.ToString(reader["first_name"]);
                        empl.LastName = Convert.ToString(reader["last_name"]);
                        empl.JobTitle = Convert.ToString(reader["job_title"]);
                        empl.Gender = Convert.ToString(reader["gender"]);
                        empl.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        employeeSearch.Add(empl);
                    }

                    return employeeSearch;



                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error finding employee");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return employeeSearch ;
        }

        /// <summary>
        /// Gets a list of employees who are not assigned to any active projects.
        /// </summary>
        /// <returns></returns>
        public IList<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employeeNoProject = new List<Employee>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM employee LEFT JOIN project_employee ON project_employee.employee_id = employee.employee_id WHERE project_id IS NULL", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee emplnp = new Employee();

                        emplnp.EmployeeId = Convert.ToInt32(reader["employee_ID"]);
                        emplnp.FirstName = Convert.ToString(reader["first_name"]);
                        emplnp.LastName = Convert.ToString(reader["last_name"]);
                        emplnp.JobTitle = Convert.ToString(reader["job_title"]);
                        emplnp.Gender = Convert.ToString(reader["gender"]);
                        emplnp.BirthDate = Convert.ToDateTime(reader["birth_date"]);

                        employeeNoProject.Add(emplnp);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting employee list");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return employeeNoProject;
        }
    }
}
