using Stefanini.DAO.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.DAO
{
    public class UOW : IDisposable
    {
        private readonly StefaniniContext _context;
        public StefaniniContext Context => _context;
        public UOW(StefaniniContext Context = null)
        {
            if(Context == null)
                _context = new StefaniniContext();
            else
            {
                _context = Context;
            }
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public Task<int> CommitAsync()
        {
            try
            {
                return _context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        //

        private PessoaRepository _pessoaRepository;
        public PessoaRepository PessoaRepository => _pessoaRepository ??= new PessoaRepository(_context);

        private CidadeRepository _cidadeRepository;
        public CidadeRepository CidadeRepository => _cidadeRepository ??= new CidadeRepository(_context);



    }
}
