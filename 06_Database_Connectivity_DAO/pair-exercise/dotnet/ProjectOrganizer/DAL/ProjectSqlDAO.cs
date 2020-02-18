using ProjectOrganizer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOrganizer.DAL
{
    public class ProjectSqlDAO : IProjectDAO
    {
        private string connectionString;

        // Single Parameter Constructor
        public ProjectSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        /// <summary>
        /// Returns all projects.
        /// </summary>
        /// <returns></returns>
        public IList<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM project", conn);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Project proj = new Project();

                        proj.ProjectId = Convert.ToInt32(reader["project_id"]);
                        proj.Name = Convert.ToString(reader["name"]);
                        proj.StartDate = Convert.ToDateTime(reader["from_date"]);
                        proj.EndDate = Convert.ToDateTime(reader["to_date"]);

                        projects.Add(proj);
                    }
                }
            }


            catch (SqlException ex)
            {

                Console.WriteLine("Error getting project list");
                Console.WriteLine("The error was: " + ex.Message);
            }

            return projects;
        }

        /// <summary>
        /// Assigns an employee to a project using their IDs.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            bool successful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO project_employee (project_id, employee_id) VALUES (@project_id, @employee_id)", conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        successful = true;
                    }


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error assigning employee to project");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return successful;
        }

        /// <summary>
        /// Removes an employee from a project.
        /// </summary>
        /// <param name="projectId">The project's id.</param>
        /// <param name="employeeId">The employee's id.</param>
        /// <returns>If it was successful.</returns>
        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            bool successful = false;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM project_employee WHERE project_id = @project_id AND employee_id = @employee_id", conn);
                    cmd.Parameters.AddWithValue("@project_id", projectId);
                    cmd.Parameters.AddWithValue("@employee_id", employeeId);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        successful = true;
                    }


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error removing employee from project");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return successful;
        }

        /// <summary>
        /// Creates a new project.
        /// </summary>
        /// <param name="newProject">The new project object.</param>
        /// <returns>The new id of the project.</returns>
        public int CreateProject(Project newProject)
        {
            int id = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO project (name, from_date, to_date) VALUES(@name, @from_date, @to_date);", conn);
                    cmd.Parameters.AddWithValue("@name", newProject.Name);
                    cmd.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    cmd.Parameters.AddWithValue("@to_date", newProject.EndDate);

                    cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("SELECT MAX(project_id) from project", conn);
                    id = Convert.ToInt32(cmd.ExecuteScalar());

                }
            }
            catch (Exception ex)
            {

                Console.WriteLine("Error adding project");
                Console.WriteLine("The error was: " + ex.Message);
            }
            return id;
        }

    }
}
