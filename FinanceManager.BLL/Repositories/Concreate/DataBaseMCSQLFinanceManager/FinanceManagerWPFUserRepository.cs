using FinanceManager.DAL.Data;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.BLL.Repositories.Concreate.DataBaseMCSQLFinanceManager
{
    public class FinanceManagerWPFUserRepository : IUsersRepository
    {
        private FinanceManagerContext _context;
        public FinanceManagerWPFUserRepository(FinanceManagerContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public string GenerateSalt(string username, string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(username + password);
        }

        public string EncryptPassword(string password, string salt)
        {
            return BCrypt.Net.BCrypt.HashPassword(password + salt);
        }

        public bool VerifyPassword(string password, string hashpassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashpassword);
        }

        public bool AuthUser(string username, string password)
        {
            var user = _context.Users.SingleOrDefault(u => u.Username == username);
            return BCrypt.Net.BCrypt.Verify(password + user.Salt, user.Password);
        }

        public void Delete(User entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public DbSet<User> GetDbSet()
        {
            return _context.Users;
        }

        public List<User> GetList()
        {
            return _context.Users.ToList();
        }

        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
