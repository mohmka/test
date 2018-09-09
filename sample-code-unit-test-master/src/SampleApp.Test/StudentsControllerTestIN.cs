using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SampleApp.Models;
using SampleApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleApp.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace SampleApp.Test
{
    [TestClass]
    public class StudentsControllerTestIN
    {


        private IStudentsRepository _studentRepository;

        private static DbContextOptions<SampleAppContext> CreateNewContextOptions()
        {
            
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

           
            var builder = new DbContextOptionsBuilder<SampleAppContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

       [TestInitialize]
       public async Task Init()
        {
            
            
             var  options = CreateNewContextOptions();

            _studentRepository = new StudentsRepository(new SampleAppContext(options));

            // var service = new StudentsRepository(context);
             _studentRepository.Add(new Student { Id = 1, Email = "j.papavoisi@gmail.com", FristName = "Papavoisi", LastName = "Jean" });
             _studentRepository.Add(new Student { Id = 2, Email = "p.garden@gmail.com", FristName = "Garden", LastName = "Pierre" });
             _studentRepository.Add(new Student { Id = 3, Email = "r.derosi@gmail.com", FristName = "Derosi", LastName = "Ronald" });

            await _studentRepository.Save();
          

        }


        [TestMethod]
        public async Task Index_ReturnsAllStudentsIn()
        {

            //Arrange
            var controller = new StudentsController(_studentRepository);

            // Act
            var viewResult = await controller.Index() as ViewResult;

            //assert
            Assert.IsNotNull(viewResult);
            var students = viewResult.ViewData.Model as List<Student>;
            Assert.AreEqual(3, students.Count);

        }


        [TestMethod]
        public async Task Details_ReturnsStudentIn()
        {

            //Arrange
            var controller = new StudentsController(_studentRepository);

            // Act
            var viewResult = await controller.Details(2) as ViewResult;


            //assert
            Assert.IsNotNull(viewResult);
            var student = viewResult.ViewData.Model as Student;
            Assert.AreEqual("Garden", student.FristName);

        }


        [TestMethod]
        public async Task Create_ReturnsRedirectToActionIn()
        {

            //Arrange
            var controller = new StudentsController(_studentRepository);

            // Act
            var result = await controller.Create(new Student {Id = 4, Email = "a.Damien@gmail.com", FristName = "Damien", LastName = "Alain" }) as RedirectToActionResult;

            //assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

    }
}
