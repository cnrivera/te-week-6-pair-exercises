using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using ProjectOrganizer.DAL;
using System.Collections.Generic;
using ProjectOrganizer.Models;

namespace ProjectOrganizerTests
{
    [TestClass]
    public class EmployeeDAOTests : EmployeeDBDAOTests
    {

        
        [TestMethod]
        public void GetAllEmployeesTest()
        {
            // Arrange

            EmployeeSqlDAO employee = new EmployeeSqlDAO(ConnectionString);

            // Act

            IList<Employee> employees = employee.GetAllEmployees();

            // Assert
            Assert.AreEqual(1, employees.Count);

        }


        [TestMethod]
        public void SearchForEmployeeTest()
        {
            // Arrange
            EmployeeSqlDAO employee = new EmployeeSqlDAO(ConnectionString);

            // Act
            IList<Employee> employees = employee.GetEmployeesWithoutProjects();

            // Assert
            Assert.AreEqual(1, employees.Count);
           
        }
        [TestMethod]
        public void GetEmployeesWithoutProjectsTest()
        {
            // Arrange
            EmployeeSqlDAO employee = new EmployeeSqlDAO(ConnectionString);

            // Act
            IList<Employee> employees = employee.Search("Sarah", "Watkins");

            // Assert
            Assert.AreEqual(1, employees.Count);
        }
    }
}
