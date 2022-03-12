using System.Collections.Generic;
using wwe.Controllers;
using wwe.Models;
using Xunit;

namespace Testing
{
    public class ComponentProcessingTest
    {

        ComponentProcessingsController _context;
        WweContext _service;

        public ComponentProcessingTest()
        {
            _service = new WweContext();
            _context = new ComponentProcessingsController(_service);
        }
        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            var result = _context.Get() as List<ComponentProcessing>;
            //Act
            Assert.IsType<List<ComponentProcessing>>(result);
            //Assert
            Assert.NotNull(result);
        }



    }
}