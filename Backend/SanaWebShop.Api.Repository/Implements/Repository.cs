using Microsoft.EntityFrameworkCore;
using SanaWebShop.Api.Models.Contexts;
using SanaWebShop.Api.Repository.Interfaces;
using System.Linq;
using System.Linq.Expressions;

#nullable disable

namespace SanaWebShop.Api.Repository.Implements
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private SanaWebShopContext _context;
        private DbSet<TEntity> _dbSet;
        public Repository(SanaWebShopContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get() => _dbSet.ToList();
        public async Task<IEnumerable<TEntity>> GetAsync() => await _dbSet.ToListAsync();

        public async Task<List<TEntity>> GetWithPagesAsync(int pageNumber, int pageSize) =>
            await (from p in _dbSet
                   select p).Skip((pageNumber - 1) * pageSize)
            .Take(pageSize).AsNoTracking().ToListAsync();

        public async Task<int> CountAsync() => await _dbSet.CountAsync();

        public TEntity GetById(int id) => _dbSet.Find(id);
        public async Task<TEntity> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
        public TEntity GetByCode(string code) => _dbSet.Find(code);
        public async Task<TEntity> GetByCodeAsync(string code) => await _dbSet.FindAsync(code);

        public async Task<List<TEntity>> GetFilteredData(Expression<Func<TEntity, bool>> criteria) => await _dbSet.Where(criteria).ToListAsync();

        public void DeleteById(int id)
        {
            var dataToDelete = _dbSet.Find(id);
            _dbSet.Remove(dataToDelete);
        }

        public void DeleteByCode(string code)
        {
            var dataToDelete = _dbSet.Find(code);
            _dbSet.Remove(dataToDelete);
        }

        public void Add(TEntity data) => _dbSet.Add(data);
        public async Task AddRangeAsync(IEnumerable<TEntity> data) => await _dbSet.AddRangeAsync(data);

        public async Task Save() => await _context.SaveChangesAsync();

        public void Update(TEntity data)
        {
            _dbSet.Attach(data);
            _context.Entry(data).State = EntityState.Modified;
        }
        public void UpdateRange(IEnumerable<TEntity> data)
        {
            foreach(TEntity item in data)
            {
                _dbSet.Attach(item);
                _context.Entry(item).State = EntityState.Modified;
            }
        }
    }
}