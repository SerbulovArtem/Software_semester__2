using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager
{
    public interface IRepository<T>
    {
        void Create(T entity);

        void Delete(T entity);

        void Update(T entity);

        List<T> GetList();
    }
}
