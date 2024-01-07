using AutoMapper;
using Einzel.Data.Dtos;

namespace Einzel.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CreateProdutoDto, Produto>();
            CreateMap<Produto, ReadProdutoDto>();

            CreateMap<CreateVariacaoTamanhoDto, VariacaoTamanho>();
            CreateMap<VariacaoTamanho, ReadVariacaoTamanhoDto>();

            CreateMap<CreateMedidasDto, Medidas>();
            CreateMap<Medidas, ReadMedidasDto>();
        }

    }
}
