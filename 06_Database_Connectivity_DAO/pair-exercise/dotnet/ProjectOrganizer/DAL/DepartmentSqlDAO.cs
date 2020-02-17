using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class DepartmentSqlDAO : IDepartmentDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns a list of all of the departments.
        /// </summary>
        /// <returns></returns>
        public IList<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM department ORDER BY department_id", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Department dept = new Department();

                        dept.Id = Convert.ToInt32(reader["department_id"]);
                        dept.Name = Convert.ToString(reader["name"]);

                        departments.Add(dept);
                    }
                }
            }

                
            catch (SqlException ex)
            {

                Console.WriteLine("Error getting department");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return departments;


        }

        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="newDepartment">The department object.</param>
        /// <returns>The id of the new department (if successful).</returns>
        public int CreateDepartment(Department newDepartment)
        {
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO department (name) VALUES(@name);", conn);
                    cmd.Parameters.AddWithValue("@name", newDepartment.Name);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(department_id) from department", conn);
                    id = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error adding department");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return id;
        }



        /// <summary>
        /// Updates an existing department.
        /// </summary>
        /// <param name="updatedDepartment">The department object.</param>
        /// <returns>True, if successful.</returns>
        public bool UpdateDepartment(Department updatedDepartment)
        {
            bool successful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("UPDATE department SET name = @name WHERE department_id = @department_id;", conn);
                    cmd.Parameters.AddWithValue("@department_id", updatedDepartment.Id);
                    cmd.Parameters.AddWithValue("@name", updatedDepartment.Name);

                   int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        successful = true;
                    }
                        
                    
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error updating department");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return successful;
            
        }

    }
}
