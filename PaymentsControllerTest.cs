using System.Collections.Generic;
using wwe.Controllers;
using wwe.Models;
using Xunit;

namespace Testing
{
    public class PaymentsControllerTest
    {

        PaymentsController _context;
        WweContext _service;

        public PaymentsControllerTest()
        {
            _service = new WweContext();
            _context = new PaymentsController(_service);
        }
        [Fact]
        public void GetAllProducts()
        {
            //Arrange
            var result = _context.Get() as List<Payment>;
            //Act
            Assert.IsType<List<Payment>>(result);
            //Assert
            Assert.NotNull(result);
        }



    }
}