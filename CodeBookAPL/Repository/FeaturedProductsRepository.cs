using CodeBookAPL.Models;
using CodeBookAPL.Repository.IRepository;

namespace CodeBookAPL.Repository
{
    public class FeaturedProductsRepository:Repository<FeaturedProduct>,IFeaturedProducts
    {
        private readonly CodeBookContext _dbData;

        public FeaturedProductsRepository(CodeBookContext dbData) : base(dbData)
        {
            _dbData = dbData;
        }


    }
}
