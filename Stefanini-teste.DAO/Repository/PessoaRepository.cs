using Stefanini.DAO.Repository.Generic;
using Stefanini.Model.Entities;
using Stefanini.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.DAO.Repository
{
    public class PessoaRepository : RepositoryBase<PessoaDTO>
    {
        private readonly StefaniniContext _context;
        public PessoaRepository(StefaniniContext Context) : base(Context)
        {
            _context = (StefaniniContext)Context;
        }

        public List<PessoaReadVM> ReadAll()
        {

            return (from p in _context.Pessoa
                    join c in _context.Cidade on p.Id_Cidade equals c.Id
                    where p.IsDisabled == false
                    select new PessoaReadVM
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        CPF = p.CPF,
                        Cidade = new CidadeReadVM { Id = c.Id, Nome = c.Nome, UF = c.UF },
                        Idade = p.Idade
                    }).ToList();
        }

        public PessoaReadVM ReadOne(int id)
        {

            return (from p in _context.Pessoa
                    join c in _context.Cidade on p.Id_Cidade equals c.Id
                    where p.Id == id && p.IsDisabled == false
                    select new PessoaReadVM
                    {
                        Id = p.Id,
                        Nome = p.Nome,
                        CPF = p.CPF,
                        Cidade = new CidadeReadVM { Id = c.Id, Nome = c.Nome, UF = c.UF },
                        Idade = p.Idade
                    }).FirstOrDefault();
        }

    }
}
