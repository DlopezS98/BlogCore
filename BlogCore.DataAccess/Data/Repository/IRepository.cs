using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace BlogCore.DataAccess.Data.Repository
{
    //Donde T es la clase que recive la interfaz
    public interface IRepository<T> where T: class{
        //Metodos comúnes de cada entidad dentro de la bd Articulos o categorias 
        //Metodo para retornar un dato por el id
        T Get(int id);

        //Metodo para obtener todos los datos de una entidad y a su vez filtrarlos
        IEnumerable<T> GetAll(
            //Una expresión de filtración que recibe una función que a su vez recibe Una clase y parametro booleano
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null 
        );

        //Obteniendo un registro condicionado
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null,
            string includeProperties = null
        );

        //Metodo para agregar un registro a la entidad
        void Add(T entity);

        //Metodo para eliminar un elemento a la entidad
        void Remove(int id);

        //Metodo para eliminar una entidad
        void Remove(T entity);
    }
}