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
    public class DepartmentSQLTests : EmployeeDBDAOTests
    {

        
        [TestMethod]
        public void GetDepartmentsTest()
        {
            // Arrange

            DepartmentSqlDAO department = new DepartmentSqlDAO(ConnectionString);

            // Act

            IList<Department> departments = department.GetDepartments();

            // Assert
            Assert.AreEqual(1, departments.Count);

        }


        [TestMethod]
        public void AddDept_Should_IncreaseCountBy1()
        {
            // Arrange
            Department dept = new Department();
            dept.Name = "book binding";

            DepartmentSqlDAO deptsql = new DepartmentSqlDAO(ConnectionString);
            int startCount = GetRowCount("department");

            // Act
            deptsql.CreateDepartment(dept);
            int endCount = GetRowCount("department");
            // Assert
            Assert.AreEqual(startCount + 1, endCount);
        }
    }
}
