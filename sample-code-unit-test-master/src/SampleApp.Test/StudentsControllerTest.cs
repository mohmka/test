using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Repository;
using SampleApp.Models;

namespace SampleApp.Test
{
    [TestClass]
    public class StudentsControllerTest
    {

        [TestMethod]
        public async Task Index_ReturnsAllStudents()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            studentsRepositoryMock.Setup(repo => repo.GetAll()).Returns(Task.FromResult(GetTestStudents()));
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            var viewResult = await controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult);
            var students = viewResult.ViewData.Model as List<Student>;
            Assert.AreEqual(3, students.Count);

        }

       [TestMethod]
        public async Task Details_ReturnsStudent()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            studentsRepositoryMock.Setup(repo => repo.Find(2)).Returns(Task.FromResult(GetTestStudents().ElementAt(1)));
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            var viewResult = await controller.Details(2) as ViewResult;


            //assert
            Assert.IsNotNull(viewResult);
            var student = viewResult.ViewData.Model as Student;
            Assert.AreEqual("Garden", student.FristName);

        }


        [TestMethod]
        public async Task Details_ReturnsNotFoundWithId()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            studentsRepositoryMock.Setup(repo => repo.Find(2)).Returns(Task.FromResult<Student>(null));
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            IActionResult actionResult = await controller.Details(2) ;


            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }


        [TestMethod]
        public async Task Details_ReturnsNotFoundWithNullId()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            IActionResult actionResult = await controller.Details(null);

            //assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }



        [TestMethod]
        public async Task Create_ReturnsRedirectToAction()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            var result = await controller.Create(new Student { Id=4, Email="a.Damien@gmail.com", FristName="Damien", LastName="Alain" }) as RedirectToActionResult;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }


        [TestMethod]
        public async Task Create_InvalidModelState()
        {

            //Arrange
            var studentsRepositoryMock = new Mock<IStudentsRepository>();
            var controller = new StudentsController(studentsRepositoryMock.Object);

            // Act
            controller.ModelState.AddModelError("Email", "Required");
            var viewResult = await controller.Create(new Student ()) as ViewResult;

            //assert
            Assert.IsNotNull(viewResult);
            var student = viewResult.Model as Student;
            Assert.IsNotNull(student);
        }


        private IEnumerable<Student> GetTestStudents()
        {

            IEnumerable<Student> students = new List<Student>() {
            new Student {Id = 1, Email = "j.papavoisi@gmail.com", FristName="Papavoisi", LastName="Jean" },
            new Student { Id = 2, Email = "p.garden@gmail.com", FristName = "Garden", LastName = "Pierre" },
            new Student { Id = 3, Email = "r.derosi@gmail.com", FristName = "Derosi", LastName = "Ronald" }
            };
            return students;
        }

    }
}
