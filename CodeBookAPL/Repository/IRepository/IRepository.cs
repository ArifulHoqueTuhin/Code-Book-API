using System.Linq.Expressions;

namespace CodeBookAPL.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null);
    
    }
}
