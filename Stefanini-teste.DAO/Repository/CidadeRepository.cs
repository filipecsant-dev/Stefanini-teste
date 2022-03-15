using Stefanini.DAO.Repository.Generic;
using Stefanini.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stefanini.DAO.Repository
{
    public class CidadeRepository : RepositoryBase<CidadeDTO>
    {
        private readonly StefaniniContext _context;
        public CidadeRepository(StefaniniContext Context) : base(Context)
        {
            _context = (StefaniniContext)Context;
        }

    }
}
