using FinanceManager.DAL.Repositories.Concreate;
using FinanceManager.BLL.Repositories.Concreate.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using FinanceManager.DAL.Data;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;

namespace FinanceManager.Tests.DAL.Repositories
{
    public class Tests
    {
        [Test]
        public void TestCreatedOrder_ReturnsCreatedOrder()
        {
            var context = new FinanceManagerAdminContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.ProductName == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            var actualOrder = context.Orders.ToList()[context.Orders.Count() - 1];

            Assert.That(orderCreated, Is.EqualTo(actualOrder));

            orderrep.Delete(orderCreated);
        }

        [Test]
        public void TestCreatedUser_ReturnsCreatedUser()
        {
            var context = new FinanceManagerAdminContext(0);
            var userrep = new FinanceManagerUserRepository(context);

            string username = "Ivannnn";
            string password = "Jogurt";

            string salt = userrep.GenerateSalt(username, password);
            string hashpassword = userrep.EncryptPassword(password, salt);
            bool is_true = userrep.VerifyPassword(password + salt, hashpassword);

            User userCreated = new User
            {
                Username = username,
                Password = hashpassword,
                Salt = salt
            };

            userrep.Create(userCreated);

            var actualUser = context.Users.ToList()[context.Users.Count() - 1];

            userrep.Delete(userCreated);

            Assert.That(userCreated.Username, Is.EqualTo(username));
        }

        [Test]
        public void TestDeletedAction_ReturnsNone()
        {
            var context = new FinanceManagerAdminContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.ProductName == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            orderrep.Delete(orderCreated);

            var actualOrder = context.Orders.Find(orderCreated.OrderId);

            Assert.IsTrue(actualOrder == null);
        }

        [Test]
        public void TestDeletedUser_ReturnsNone()
        {
            var context = new FinanceManagerAdminContext(0);
            var userrep = new FinanceManagerUserRepository(context);

            string username = "Ivannnn";
            string password = "Jogurt";

            string salt = userrep.GenerateSalt(username, password);
            string hashpassword = userrep.EncryptPassword(password, salt);
            bool is_true = userrep.VerifyPassword(password + salt, hashpassword);

            User userCreated = new User
            {
                Username = username,
                Password = hashpassword,
                Salt = salt
            };

            userrep.Create(userCreated);

            userrep.Delete(userCreated);

            var actualUser = context.Users.Find(userCreated.Username);

            Assert.IsTrue(actualUser == null);
        }

        [Test]
        public void TestUpdatedAction_ReturnsUpdatedAction()
        {
            var context = new FinanceManagerAdminContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.ProductName == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            orderrep.Update(orderCreated);
            var orderUpdated = context.Orders.ToList().Last();

            Assert.That(orderCreated, Is.EqualTo(orderUpdated));

            orderrep.Delete(orderCreated);
        }
    }
}