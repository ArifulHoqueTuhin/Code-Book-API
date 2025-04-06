using AutoMapper;
using CodeBookAPL.Models;
using CodeBookAPL.Models.DTO;

namespace CodeBookAPL
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Product, ProductsDTO>().ReverseMap();
            CreateMap<FeaturedProduct, FeaturedProductsDTO>().ReverseMap();
            CreateMap<Order, OrderDTOcs>().ReverseMap();
            CreateMap<OrderItem,OrderItemDTO> ().ReverseMap();
     
        }
    }
    

    
}
