using Azure;
using Microsoft.AspNetCore.Mvc;
using SanaWebShop.Api.Business.Implements;
using SanaWebShop.Api.Controllers;

namespace SanaWebShop.Api.Tests.Controller
{
    public class CustomerControllerTest
    {
        private CustomerController _currentController;
        private readonly Mock<IActionsCustomersBusiness> _bService = new();

        public CustomerControllerTest()
        {
            _currentController = new(_bService.Object);
        }

        [Theory]
        [InlineData(1, 200)]
        [InlineData(2, 400)]
        [InlineData(3, 404)]
        public async Task CreateTest(int index, int expected)
        {
            //Arrange
            switch (index)
            {
                case 1:
                    _bService
                        .Setup(t => t.Create(It.IsAny<string>()))
                        .Returns(Task.FromResult(1));
                    break;
                case 2:
                    _bService
                        .Setup(t => t.Create(It.IsAny<string>()))
                        .Throws(new Exception("Excepción"));
                    break;
                case 3:
                    _bService
                         .Setup(t => t.Create(It.IsAny<string>()))
                         .Returns(Task.FromResult(0));
                    break;
            }

            //Act 
            IActionResult response = await _currentController.Create("test");
            var result = response as ObjectResult;
            
            //Assert
            Assert.Equal(expected, result?.StatusCode);
        }
    }
}