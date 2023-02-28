using Intuit.EF;
using Intuit.Negocio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.Negocio
{
    public class BaseNegocio<T> : BLLContext, IBaseNegocio<T> where T : class
    {

        public bool Delete(T model)
        {
            Context.Set<T>().Remove(model);
            return Save();
        }
        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public List<T> GetAllByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).ToList();
        }
        public T? GetByCondition(Expression<Func<T, bool>> where)
        {
            return Context.Set<T>().Where(where).SingleOrDefault();
        }
        public T? GetById(int id)
        {
            return Context.Set<T>().Find(id); 
        }
        public bool Insert(T model)
        {
            Context.Add(model);
            return Save();
        }
        public bool Save()
        {
            return Context.SaveChanges() > 0;
        }
        public bool Update(T model)
        {
            Context.Update(model);
            return Save();
        }
    }
}
