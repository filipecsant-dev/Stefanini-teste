using AutoMapper;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini.WEB.AutoMapper
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
