namespace Unnamed.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDbRepository<T> : IDisposable where T : class
    {
        IQueryable<T> All();

        T GetById(object id);

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(object id);

        T Attach(T entity);

        void Detach(T entity);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
