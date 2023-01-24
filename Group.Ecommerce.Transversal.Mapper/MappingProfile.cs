using AutoMapper;
using Group.Ecommerce.Domain.Entity;
using Group.Ecommerce.Application.DTO;


namespace Group.Ecommerce.Transversal.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        { 
                CreateMap<Customers, CustomersDto>().ReverseMap();
                CreateMap<Users, UsersDto>().ReverseMap();
        }
    }
}