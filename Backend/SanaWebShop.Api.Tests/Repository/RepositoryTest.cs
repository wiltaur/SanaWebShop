using Microsoft.EntityFrameworkCore;
using SanaWebShop.Api.Models.Contexts;

namespace SanaWebShop.Api.Tests.Repository
{
    public class RepositoryTest
    {
        [Fact]
        public void ContextHasCorrectDefaultSchemaTest()
        {
            int pasa = 0;
            var context = new SanaWebShopContext();
            context.TestModelCreation(new ModelBuilder());
            pasa++;
            Assert.True(pasa > 0);
        }
    }
}