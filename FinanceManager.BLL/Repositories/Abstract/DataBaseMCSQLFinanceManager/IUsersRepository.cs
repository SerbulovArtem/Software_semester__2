using FinanceManager.DAL.Data;
using FinanceManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.BLL.Repositories.Concreate.DataBaseMCSQLFinanceManager
{
    public interface IUsersRepository
    {
        public void Create(User entity);

        public string GenerateSalt(string username, string password);

        public string EncryptPassword(string password, string salt);

        public bool VerifyPassword(string password, string hashpassword);

        public bool AuthUser(string username, string password);

        public void Delete(User entity);

        public DbSet<User> GetDbSet();

        public List<User> GetList();

        public void Update(User entity);
    }
}
