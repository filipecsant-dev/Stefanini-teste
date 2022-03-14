using Stefanini.DAO;
using Stefanini.DAO.Repository.Generic;
using Stefanini.Model;
using System.Linq.Expressions;

namespace Stefanini.Business
{
    public class RNBase<T> where T : Disable
    {
        public static void Inserir(T ent)
        {
            try
            {
                using (var uow = new UOW())
                {
                     new RepositoryBase<T>(uow.Context).Insert(ent);
                    uow.Commit();
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Inserir(List<T> ent)
        {
            try
            {
                using (var uow = new UOW())
                {
                    new RepositoryBase<T>(uow.Context).InsertMany(ent);
                    uow.Commit();
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static T FindOne(Expression<Func<T, bool>> condition)
        {
            try
            {
                return new RepositoryBase<T>(new StefaniniContext()).GetFirst(condition);
            }
            catch (Exception)
            {
                    
                throw;
            }
        }

        public  static void Update(T ent)
        {
            try
            {
                using (var uow = new UOW())
                {
                    new RepositoryBase<T>(new StefaniniContext()).Update(ent);
                    uow.Commit();
                }
                
                
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, int? pagesize = null)
        {
            {
                try
                {
                    return new RepositoryBase<T>(new StefaniniContext()).Get(filter,orderBy,includeProperties,page,pagesize);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        public static T FindOne(int id)
        {
            try
            {
                return new RepositoryBase<T>(new StefaniniContext()).GetByID(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void Delete(int id)
        {
            try
            {
                new RepositoryBase<T>(new StefaniniContext()).Delete(id);
                using (var uow = new UOW())
                {
                    var repo = new RepositoryBase<T>(uow.Context);
                    repo.Delete(id);
                    uow.Commit();
                }
            }
            catch (Exception)
            {
                    
                throw;
            }
        }

        public static int Count(Expression<Func<T, bool>> filter = null)
        {
            try
            {
                using (var uow = new UOW())
                {
                    var qtd = new RepositoryBase<T>(uow.Context).Count(filter);
                    return qtd;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
