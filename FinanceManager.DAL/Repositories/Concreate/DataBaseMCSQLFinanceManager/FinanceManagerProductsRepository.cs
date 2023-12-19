using FinanceManager.DAL.Data;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.DAL.Repositories.Concreate.DataBaseMCSQLFinanceManager
{
    public class FinanceManagerProductsRepository : IProductsRepository
    {
        private FinanceManagerAdminContext _context;
        public FinanceManagerProductsRepository(FinanceManagerAdminContext context) 
        {
            _context = context;
        }

        public void Create(Product entity)
        {
            _context.Products.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<Product> GetDbSet()
        {
            return _context.Products;
        }

        public List<Product> GetList()
        {
            return GetDbSet().ToList();
        }

        public void Update(Product entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
