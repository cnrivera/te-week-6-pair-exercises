using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.Extensions.Configuration;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class EmployeeDBDAOTests
    {

        protected string ConnectionString; //{ get; } = "Server=.\\SQLEXPRESS;Database=EmployeeDB;Trusted_Connection=True;";

        /// <summary>
        /// Holds the newly generated dept id.
        /// </summary>
        protected int NewDeptId { get; private set; }

        /// <summary>
        /// The transaction for each test.
        /// </summary>
        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            var configuration = GetIConfigurationRoot(Environment.CurrentDirectory);
            //configuration = configuration;
            ConnectionString = configuration.GetConnectionString("Project");
            // Begin the transaction    
            transaction = new TransactionScope();

            //Get the SQL Script to run
            string sql = File.ReadAllText("dbscript.sql");

            //Execute the script
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                // If there is a row to read
                if (reader.Read())
                {
                    this.NewDeptId = Convert.ToInt32(reader["newDeptId"]);
                }
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        protected int GetRowCount(string table)
        {
            int rows = 0;


            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT count(*) from {table}", conn);
                rows = Convert.ToInt32(cmd.ExecuteScalar());

            }

            return rows;

        }

        public static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }

    }
}
