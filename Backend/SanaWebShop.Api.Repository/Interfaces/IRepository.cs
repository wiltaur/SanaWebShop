using System.Linq.Expressions;

namespace SanaWebShop.Api.Repository.Interfaces
{
    public interface IRepository<TEntity>
    {
        IEnumerable<TEntity> Get();
        Task<IEnumerable<TEntity>> GetAsync();
        Task<List<TEntity>> GetWithPagesAsync(int pageNumber, int pageSize);
        Task<int> CountAsync();
        TEntity GetById(int id);
        Task<TEntity> GetByIdAsync(int id);
        TEntity GetByCode(string code);
        Task<TEntity> GetByCodeAsync(string code);
        Task<List<TEntity>> GetFilteredData(Expression<Func<TEntity, bool>> criteria);
        void Add(TEntity data);
        Task AddRangeAsync(IEnumerable<TEntity> data);
        void DeleteById(int id);
        void DeleteByCode(string code);
        void Update(TEntity data);

        Task Save();
    }
}