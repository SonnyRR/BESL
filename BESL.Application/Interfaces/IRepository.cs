namespace BESL.Application.Interfaces
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable
        where TEntity : class
    {
        IQueryable<TEntity> All();

        IQueryable<TEntity> AllAsNoTracking();

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
