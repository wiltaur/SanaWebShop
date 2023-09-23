using Microsoft.EntityFrameworkCore;
using SanaWebShop.Api.Models.Contexts;

namespace SanaWebShop.Api.Tests.Helppers
{
    public class SanaWebShopContextMock
    {
        public static Mock<SanaWebShopContext> GetDbContextMock()
        {
            var dbName = Guid.NewGuid().ToString();
            var dbOptions = new DbContextOptionsBuilder<SanaWebShopContext>()
                        .UseInMemoryDatabase(dbName)
                        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                        .EnableSensitiveDataLogging(true)
                        .Options;
            return new Mock<SanaWebShopContext>(dbOptions);
        }

        public static Mock<DbSet<T>> GetMockDbSet<T>(IQueryable<T> introLst) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();

            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(default))
                .Returns(new TestAsyncEnumerator<T>(introLst.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestAsyncQueryProvider<T>(introLst.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(introLst.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(introLst.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => introLst.GetEnumerator());

            return mockSet;
        }
    }
}
