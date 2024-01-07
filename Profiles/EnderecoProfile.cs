using AutoMapper;
using Einzel.Data.Dtos;
using Einzel.Models;

namespace Einzel.Profiles
{
    public class EnderecoProfile : Profile
    {
        public EnderecoProfile() 
        {
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, ReadEnderecoDto>();
        }
    }
}
