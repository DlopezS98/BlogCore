using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using BlogCore.DataAccess.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace BlogCore.DataAccess.Data
{

    //Clase global de repositorio hereda los metodos de la intefaz IRepository
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;
        public Repository(DbContext context)
        {
            this.Context = context;
            this.dbSet = context.Set<T>();
        }

        //Implementando los metodos en la interfaz de repositorio
        public void Add(T entity)
        {
            //Agregando la entidad a la base de datos
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            //Verificación si se esta enviando un filtro al metodo
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Incluyendo las propiedades separadas por comas (,)
            if (includeProperties != null)
            {
                //Separando entidades y eliminando las entidades que este vacias 
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if(orderBy != null){
                return orderBy(query).ToList();
            }

            //Retornando los datos filtrados o no filtrados en caso 
            //que no se especifique ningun paramentro de filtración
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                //Separando entidades y eliminando las entidades que este vacias 
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            T entityToRemove = dbSet.Find(id);
            Remove(entityToRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}