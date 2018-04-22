namespace Unnamed.Tests
{
    using Moq;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public static class TestHelpers
    {
        public static Mock<DbSet<T>> MockDbSet<T>() where T : class
        {
            return MockDbSet<T>(null);
        }

        public static Mock<DbSet<T>> MockDbSet<T>(List<T> inMemoryData) where T : class
        {
            if (inMemoryData == null)
            {
                inMemoryData = new List<T>();
            }
            var queryable = inMemoryData.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => inMemoryData.Add(s));

            return dbSet;
        }
    }
}
