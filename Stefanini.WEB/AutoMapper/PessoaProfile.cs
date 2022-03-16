using AutoMapper;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;

namespace Stefanini.WEB.AutoMapper
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<PessoaDTO, PessoaVM>();
            CreateMap<PessoaVM, PessoaDTO>();

            CreateMap<PessoaDTO, PessoaInsertVM>();
            CreateMap<PessoaInsertVM, PessoaDTO>();

            CreateMap<PessoaVM, PessoaInsertVM>();
            CreateMap<PessoaInsertVM, PessoaVM>();
        }
    }
}
