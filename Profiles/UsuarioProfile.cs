using AutoMapper;
using Einzel.Data.Dtos;
using Einzel.Models;

namespace Einzel.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile() 
        {
            CreateMap<CreateUsuarioDto, Usuario>();

            CreateMap<Usuario, ReadUsuarioDto>();
        }
    }
}
