using LibraryCardAPI.Controllers;
using LibraryCardAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;
using Xunit;
using LibraryCardAPI.Models;

namespace LibraryCardAPI.UnitTest.Controller
{
    public class StudentsControllerTest
    {
        StudentsController _controller;
        IStudentService _services;

        public StudentsControllerTest()
        {
            _services = new StudentService();
            _controller = new StudentsController(_services);
        }
        [Fact]
        public async Task Get_WhenCalled_ReturnsOkResults()
        {
            var okResult = await _controller.Get(1);
            Assert.IsType<OkObjectResult>(okResult);

        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controller.Get(" ", "asc", 10, 1).Result as OkObjectResult;
            // Assert
            var items = Assert.IsType<List<Student>>(okResult.Value);
            Assert.IsType<OkObjectResult>(okResult);
        }
    }
}
