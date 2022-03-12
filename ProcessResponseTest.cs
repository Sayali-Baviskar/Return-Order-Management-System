using System.Collections.Generic;
using wwe.Controllers;
using wwe.Models;
using Xunit;

namespace Testing
{
    public class ProcessResponseTest
    {

        PackageController _context;
        WweContext _service;

        public ProcessResponseTest()
        {
            _service = new WweContext();
            _context = new PackageController(_service);
        }
        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            var result = _context.Get() as List<ProcessResponse>;
            //Act
            Assert.IsType<List<ProcessResponse>>(result);
            //Assert
            Assert.NotNull(result);
        }
        [Fact]
        public void GetById()
        {
            //Arrange
            var result = _context.GetByUsername("uname") as List<ProcessResponse>;
            //Act
            Assert.IsType<List<ProcessResponse>>(result);
            //Assert
            Assert.NotNull(result);
        }


    }
}