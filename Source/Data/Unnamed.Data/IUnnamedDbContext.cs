namespace Unnamed.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;
    using Unnamed.Data.Models;

    public interface IUnnamedDbContext
    {
        DbSet<Book> Books { get; set; }

        DbSet<Author> Authors { get; set; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<T> Set<T>() where T : class;
    }
}
