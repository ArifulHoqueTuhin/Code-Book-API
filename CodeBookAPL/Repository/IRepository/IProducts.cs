using System.Linq.Expressions;
using CodeBookAPL.Models;

namespace CodeBookAPL.Repository.IRepository
{
    public interface IProducts :IRepository<Product>
    {
        Task<Product> GetAsync(Expression<Func<Product, bool>>? filter = null, bool tracked = true);

    }
}
