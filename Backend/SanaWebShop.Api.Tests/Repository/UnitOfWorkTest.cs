using AutoFixture;
using SanaWebShop.Api.Models.Contexts;
using SanaWebShop.Api.Models.Entities;
using SanaWebShop.Api.Repository.Implements;
using SanaWebShop.Api.Tests.Helppers;

namespace SanaWebShop.Api.Tests.Repository
{
    public class UnitOfWorkTest : SanaWebShopContextMock
    {
        private UnitOfWork _currentRepo;
        private Fixture _autodata;

        private readonly Mock<SanaWebShopContext> _context;

        public UnitOfWorkTest()
        {
            _autodata = new Fixture();
            _autodata.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
          .ForEach(b => _autodata.Behaviors.Remove(b));
            _autodata.Behaviors.Add(new OmitOnRecursionBehavior(1));
            _context = GetDbContextMock();
            _currentRepo = new(_context.Object);
        }

        [Fact]
        public void CustomerTest()
        {
            //Arrange
            string exep = string.Empty;
            var entities = _autodata.Create<List<Customer>>().AsQueryable();

            _context.Setup(c => c.Customers).Returns(GetMockDbSet(entities).Object);

            //Act
                var response = _currentRepo.Customers;
            
            //Assert
            Assert.True(response != null);
        }
    }
}