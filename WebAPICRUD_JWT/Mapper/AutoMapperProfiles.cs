using AutoMapper;
using WebAPICRUD_JWT.Dtos;
using WebAPICRUD_JWT.Models;

namespace WebAPICRUD_JWT.Mapper
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //Para el post o create 
            CreateMap<CreateProductDTO, Product>();

            //POST o UPDATE
            CreateMap<UpdateProductDTO, Product>();

            //Usuarios 
            CreateMap<RegisterUserDTO, User>();

            CreateMap<LoginUserDTO, User>();
            
            CreateMap<User, ListUsersDTO > ();
        }
    }
}
