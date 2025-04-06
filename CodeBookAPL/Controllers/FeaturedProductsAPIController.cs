using AutoMapper;
using CodeBookAPL.Models;
using CodeBookAPL.Models.DTO;
using CodeBookAPL.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodeBookAPL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturedProductsAPIController : ControllerBase
    {
        private readonly IFeaturedProducts _dbFeature;
        private readonly IMapper _mapper;

        public FeaturedProductsAPIController(IFeaturedProducts dbFeature, IMapper mapper)
        {
            _dbFeature = dbFeature;
            _mapper = mapper;
        }

     

        [HttpGet]
        public async Task<ActionResult<List<FeaturedProductsDTO>>> GetAllProducts()
        {
            var Featuredproducts = await _dbFeature.GetAllAsync();
            return Ok(_mapper.Map<List<FeaturedProductsDTO>>(Featuredproducts));
        }
    }
}
