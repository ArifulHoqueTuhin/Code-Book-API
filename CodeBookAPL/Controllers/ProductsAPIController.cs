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
    public class ProductsAPIController : ControllerBase
    {
        private readonly IProducts _dbProducts;
        private readonly IMapper _mapper;

        public ProductsAPIController(IProducts dbProducts, IMapper mapper)
        {
            _dbProducts = dbProducts;
            _mapper = mapper;
        }


     

        [HttpGet]
        public async Task<ActionResult<List<ProductsDTO>>> GetAllProducts()
        {
            var productsData = await _dbProducts.GetAllAsync();
            return Ok(_mapper.Map<List<ProductsDTO>>(productsData));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductsDTO>> GetProductById(int id)
        {
            
            var product = await _dbProducts.GetAsync(u => u.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<ProductsDTO>(product));
        }
    }
}
