using AutoMapper;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini_teste.AutoMapper
{
    public class CidadeProfile : Profile
    {
        public CidadeProfile()
        {
            CreateMap<CidadeDTO, CidadeVM>();
            CreateMap<CidadeVM, CidadeDTO>();
        }
    }
}
