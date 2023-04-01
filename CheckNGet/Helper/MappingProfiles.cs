using AutoMapper;
using CheckNGet.Models;
using CheckNGet.Models.DTO;

namespace CheckNGet.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Restaurant, RestaurantDTO>();
            CreateMap<RestaurantDTO, Restaurant>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Dish, DishDTO>();
            CreateMap<User, UserDTO>();
            CreateMap<Order, OrderDTO>();
        }
    }
}
