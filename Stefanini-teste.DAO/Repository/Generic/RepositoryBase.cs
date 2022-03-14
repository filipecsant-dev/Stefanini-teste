using Microsoft.EntityFrameworkCore;
using Stefanini.Model;
using System.Linq.Expressions;

namespace Stefanini.DAO.Repository.Generic
{
    public class RepositoryBase<T> where T : Disable
    { 

        protected StefaniniContext Context { get; set; }
        private DbSet<T> dbSet;

        public RepositoryBase(StefaniniContext Context)
        {
            this.Context = Context;
            this.dbSet = Context.Set<T>();
        }


        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, int? pagesize = null, string sortColumn = "", string sortColumnDir = "")
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (sortColumn != string.Empty && sortColumn.IndexOf(".") <= 0)
            {
                ParameterExpression[] typeParams = new ParameterExpression[] {
                    Expression.Parameter(typeof(T), "")
                };

                System.Reflection.PropertyInfo pi = typeof(T).GetProperty(sortColumn);

                sortColumnDir = sortColumnDir == "asc" ? "OrderBy" : "OrderByDescending";

                query = (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        sortColumnDir,
                        new Type[] { typeof(T), pi.PropertyType },
                        query.Expression,
                        Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
                );
            }
            else if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(obj => obj.IsDisabled);
            }

            if (pagesize != null)
                query = query.Skip(page * pagesize.Value).Take(pagesize.Value);

            return query.ToList();
        }

        public IEnumerable<T> GetAll(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, int? pagesize = null, string sortColumn = "", string sortColumnDir = "")
        {

            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (sortColumn != string.Empty && sortColumn.IndexOf(".") <= 0)
            {
                ParameterExpression[] typeParams = new ParameterExpression[] {
                    Expression.Parameter(typeof(T), "")
                };

                System.Reflection.PropertyInfo pi = typeof(T).GetProperty(sortColumn);

                sortColumnDir = sortColumnDir == "asc" ? "OrderBy" : "OrderByDescending";

                query = (IOrderedQueryable<T>)query.Provider.CreateQuery(
                    Expression.Call(
                        typeof(Queryable),
                        sortColumnDir,
                        new Type[] { typeof(T), pi.PropertyType },
                        query.Expression,
                        Expression.Lambda(Expression.Property(typeParams[0], pi), typeParams))
                );
            }
            else if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(obj => obj.IsDisabled);
            }


            if (pagesize != null)
                query = query.Skip(page * pagesize.Value).Take(pagesize.Value);

            return query.ToList();
        }


        public virtual int Count(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = "")
        {
            try
            {
                if (filter != null)
                {
                    var query = Context.Set<T>().Where(filter);
                    //query = query.Where(obj => obj.Disable == false);
                    return query.Count();
                }
                return Context.Set<T>().Count();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public virtual T GetAllFirst(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {

                var consulta = Context.Set<T>().Where(filter);

                if (!String.IsNullOrEmpty(includeProperties))
                    foreach (var p in includeProperties.Split(','))
                    {
                        consulta = consulta.Include(p);
                    }
                if (orderBy != null)
                {
                    return orderBy(consulta).FirstOrDefault();
                }
                else
                {
                    return consulta.FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public virtual T GetFirst(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "")
        {
            try
            {
                var consulta = (filter == null) ? Context.Set<T>() : Context.Set<T>().Where(filter);
                //consulta = consulta.Where(obj => obj.Disable == false);

                if (!String.IsNullOrEmpty(includeProperties))
                    foreach (var p in includeProperties.Split(','))
                    {
                        consulta = consulta.Include(p);
                    }
                if (orderBy != null)
                {
                    return orderBy(consulta).FirstOrDefault();
                }
                else
                {
                    return consulta.OrderBy(obj => obj.IsDisabled).FirstOrDefault();
                }



            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }

        public virtual T GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void InsertMany(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            T entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public void DeleteMany(IEnumerable<T> objects)
        {
            try
            {
                foreach (var o in objects)
                {
                    Delete(o);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public void DeleteManyPermanent(IEnumerable<T> objects)
        {
            try
            {
                foreach (var o in objects)
                {
                    DeletePermanent(o);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public virtual void DeletePermanent(T entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            entityToDelete.IsDisabled = true;
            Update(entityToDelete);

        }

        public virtual void UnDelete(object id)
        {
            T entityToUnDelete = dbSet.Find(id);
            UnDelete(entityToUnDelete);
        }

        public virtual void UnDelete(T entityToUnDelete)
        {
            entityToUnDelete.IsDisabled = false;
            Update(entityToUnDelete);

        }

        public virtual void Update(T entityToUpdate)
        {
            try
            {
                dbSet.Attach(entityToUpdate);
                Context.Entry(entityToUpdate).State = EntityState.Modified;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public virtual int GetCount(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Count();
        }
    }
}
