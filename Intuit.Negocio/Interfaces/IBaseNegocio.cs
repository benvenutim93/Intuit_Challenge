using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Intuit.Negocio.Interfaces
{
    public interface IBaseNegocio<T> where T : class
    {
        T? GetByCondition(Expression<Func<T, bool>> where);
        List<T> GetAll();
        public List<T> GetAllByCondition(Expression<Func<T, bool>> where);
        T? GetById(int id);
        bool Insert(T model);
        bool Update(T model);
        bool Delete(T model);
        bool Save();
    }
}
