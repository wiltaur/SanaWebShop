using Microsoft.AspNetCore.Mvc;
using SanaWebShop.Api.Business.Implements;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanaWebShop.Api.Tests.Business
{
    public class CustomersBusinessTest
    {
        private ActionsCustomersBusiness _currentBusiness;
        private readonly Mock<IUnitOfWork> _bService = new();

        public CustomersBusinessTest()
        {
            _currentBusiness = new(_bService.Object);
        }

        [Theory]
        [InlineData(1, 0)]
        [InlineData(2, 1)]
        public async Task CreateTest(int index, int expected)
        {
            //Arrange
            switch (index)
            {
                case 1:
                    _bService
                        .Setup(t => t.Customers.Add(It.IsAny<Customer>())).Verifiable();
                    _bService
                        .Setup(t => t.Customers.Save()).Verifiable();
                    break;
                case 2:
                    _bService
                        .Setup(t => t.Customers.Add(It.IsAny<Customer>()))
                        .Throws(new Exception("Error"));
                    break;
            }

            //Act 
            try
            {
                var response = await _currentBusiness.Create("test");

                //Assert
                Assert.Equal(expected, response);
            }catch(Exception ex)
            {
                //Assert
                Assert.Equal("Error", ex.Message);
            }
        }
    }
}