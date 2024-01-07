using AutoMapper;
using Einzel.Data.Dtos;
using Einzel.Models;

namespace Einzel.Profiles
{
    public class CompraProfile : Profile
    {
        public CompraProfile() 
        {
            CreateMap<CreateCompraDto, Compra>();
            CreateMap<Compra, ReadCompraDto>();
        }
    }
}
