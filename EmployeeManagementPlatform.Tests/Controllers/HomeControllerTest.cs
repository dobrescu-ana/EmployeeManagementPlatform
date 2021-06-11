using EmployeeManagementPlatform;
using EmployeeManagementPlatform.Controllers;
using EmployeeManagementPlatform.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace EmployeeManagementPlatform.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
       
        //[TestMethod]
        //public void ListDeps()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.ListDeps() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void ListLeaves()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.ListLeaves() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void ListSalary()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.ListSalary() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        //[TestMethod]
        //public void ListEmployees()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.ListEmployees() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void AddDepartment()
        {
            // Act
            Department department = new Department();
            department.DepartmentName = "IT";

            // Assert
            Assert.AreEqual("IT", department.DepartmentName);

        }

        [TestMethod]
        public void AddEmployee()
        {
            Employee employee = new Employee();
            employee.FirstName = "Ana";
            employee.LastName = "Dobrescu";
            employee.Email = "ana.dobrescu@gmail.com";
            employee.Address = "str. Garoafelor, nr 8";
            employee.PhoneNo = "0712345568";
            // Assert
            Assert.AreEqual("Ana", employee.FirstName);
            Assert.AreEqual("Dobrescu", employee.LastName);
            Assert.AreEqual("ana.dobrescu@gmail.com", employee.Email);
            Assert.AreEqual("str. Garoafelor, nr 8", employee.Address);
            Assert.AreEqual("0712345568", employee.PhoneNo);

        }

        [TestMethod]
        public void AddLeave()
        {
            Leave leave = new Leave();
            leave.Reason = "concediu";
            leave.Status = "status";
            DateTime start = new DateTime(2021, 6, 6);
            leave.StartDate = start;
            DateTime end = new DateTime(2021, 7, 7);
            leave.EndDate = end;
            // Assert
            Assert.AreEqual("concediu", leave.Reason);
            Assert.AreEqual("status", leave.Status);
            Assert.AreEqual(start, leave.StartDate);
            Assert.AreEqual(end, leave.EndDate);

        }

        [TestMethod]
        public void AddSalary()
        {
            Salary salary = new Salary();
            salary.NetSalary = 36000;
            salary.GrossSalary = 40000;
            salary.EmploymentType = "fulltime";

            Assert.AreEqual(36000,salary.NetSalary);
            Assert.AreEqual(40000, salary.GrossSalary);
            Assert.AreEqual("fulltime",salary.EmploymentType);

        }

        [TestMethod]
        public void AddTask()
        {
            Task task = new Task();
            task.Description = "tododo";
            DateTime deadline = new DateTime(2021, 6, 6);
            task.Deadline = deadline;

            Assert.AreEqual(deadline, task.Deadline);
            Assert.AreEqual("tododo", task.Description);
        }

        //[TestMethod]
        //public void ListTask()
        //{
        //    // Arrange
        //    HomeController controller = new HomeController();

        //    // Act
        //    ViewResult result = controller.ToDoList() as ViewResult;

        //    // Assert
        //    Assert.IsNotNull(result);

        //}
    }
}
