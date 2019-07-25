namespace BESL.Application.Tests.Infrastructure
{
    using BESL.Application.Interfaces;
    using BESL.Domain.Infrastructure;
    using Moq;
    using System.Linq;

    public static class MockExtensions
    {
        public static void SetupIQueryable<T, K>(this Mock<T> mock, IQueryable<K> queryable)
            where T : class, IDeletableEntityRepository<K>
            where K : class, IDeletableEntity
        {
            mock.Setup(r => r.AllAsNoTracking().GetEnumerator()).Returns(queryable.GetEnumerator());
            mock.Setup(r => r.AllAsNoTracking().Provider).Returns(queryable.Provider);
            mock.Setup(r => r.AllAsNoTracking().ElementType).Returns(queryable.ElementType);
            mock.Setup(r => r.AllAsNoTracking().Expression).Returns(queryable.Expression);
        }
    }
}
