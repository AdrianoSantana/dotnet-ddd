using Api.Domain.Dtos.User;
using Api.Domain.Models;
using AutoMapper;

namespace Api.CrossCutting.Mappings
{
    public class DtoToModelProfile : Profile
    {
        public DtoToModelProfile()
        {
            // Faz o mapeamento de um userModel para userDto e vice Versa
            CreateMap<UserModel, UserDto>().ReverseMap();
        }
    }
}