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
    public class ProjectDAOTests : EmployeeDBDAOTests
    {

        
        [TestMethod]
        public void GetAllProjectsTest()
        {
            // Arrange

            ProjectSqlDAO project = new ProjectSqlDAO(ConnectionString);

            // Act

            IList<Project> projects = project.GetAllProjects();

            // Assert
            Assert.AreEqual(1, projects.Count);

        }


        [TestMethod]
        public void AssignEmployeeToAProjectTest()
        {
            // Arrange
            ProjectSqlDAO project = new ProjectSqlDAO(ConnectionString);
        
            // Act
            bool isAdded = project.AssignEmployeeToProject(NewProjId, NewEmployeeId);

            // Assert
            Assert.AreEqual(true, isAdded);
        }


        [TestMethod]
        public void RemoveEmployeeFromAProjectTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [TestMethod]
        public void CreateANewProject()
        {
            // Arrange
            Project proj = new Project();
            proj.Name = "book reading";
            proj.StartDate = Convert.ToDateTime("2020-01-01");
            proj.EndDate = Convert.ToDateTime("2020-04-30");


            ProjectSqlDAO projsql = new ProjectSqlDAO(ConnectionString);
            int startCount = GetRowCount("project");

            // Act
            projsql.CreateProject(proj);
            int endCount = GetRowCount("project");
            // Assert
            Assert.AreEqual(startCount + 1, endCount);
        }
    }
}
