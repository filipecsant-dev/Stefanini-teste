using Microsoft.EntityFrameworkCore;

namespace Stefanini.DAO.Repository.Extension
{
    public static class DbSetExtension
    {
        /// <summary>
        /// Inclui as propriedades de navegação
        /// </summary>
        /// <typeparam name="T">A entidade</typeparam>
        /// <param name="dbSet"></param>
        /// <param name="props">Propriedades a ser inclusas</param>
        /// <returns></returns>
        public static IQueryable<T> DynamicIncludeProp<T>(this DbSet<T> dbSet, params string[] props) where T : class
        {
            var query = dbSet.AsNoTracking();
            for (int i = 0; i < props.Length; i++)
            {
                query = query.Include(props[i]);
            }
            return query;
        }
    }
}
