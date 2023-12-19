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
    public class FinanceManagerWPFOrdersRepository : IOrdersRepository
    {
        private FinanceManagerContext _context;
        public FinanceManagerWPFOrdersRepository(FinanceManagerContext context) 
        {
            _context = context;
        }

        public void Create(Order entity)
        {
            _context.Orders.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<Order> GetDbSet()
        {
            return _context.Orders;
        }

        public List<Order> GetList()
        {
            return GetDbSet().Include(o => o.Product).ToList();
        }

        public void Update(Order entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool CheckQuantity(int quantity)
        {
            return quantity > 0;
        }
    }
}
