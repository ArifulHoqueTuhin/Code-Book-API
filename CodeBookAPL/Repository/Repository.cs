using CodeBookAPL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Linq;
using CodeBookAPL.Models;

namespace CodeBookAPL.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CodeBookContext _dbData;
        internal DbSet<T> _dbSet;

        public Repository(CodeBookContext dbData)
        {
            _dbData = dbData;
            _dbSet = _dbData.Set<T>();

        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.ToListAsync();
        }
    

    }
}
