using System.Linq.Expressions;
using System.Linq;
using CodeBookAPL.Models;
using CodeBookAPL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CodeBookAPL.Repository
{
    public class ProductsRepository: Repository<Product>,IProducts
    {
        private readonly CodeBookContext _dbData;

        public ProductsRepository(CodeBookContext dbData) : base(dbData)
        {
            _dbData = dbData;
          
        }
        public async Task<Product> GetAsync(Expression<Func<Product, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<Product> query = _dbData.Products;

            if (!tracked)

            {
                query = query.AsNoTracking();
            }


            if (filter != null)
            {
                query = query.Where(filter);
            }

            //return await query.FirstOrDefaultAsync();

            return await query.FirstOrDefaultAsync() ?? default!;



        }

    }
}
