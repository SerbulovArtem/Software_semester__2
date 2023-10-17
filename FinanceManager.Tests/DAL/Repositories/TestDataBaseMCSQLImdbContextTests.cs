using FinanceManager.DAL.Repositories.Concreate;
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
            var context = new ImdbContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.Name == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                Date = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            var actualOrder = context.Orders.ToList()[context.Orders.Count() - 1];

            Assert.That(orderCreated, Is.EqualTo(actualOrder));
        }

        [Test]
        public void TestDeletedAction_ReturnsNone()
        {
            var context = new ImdbContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.Name == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                Date = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            orderrep.Delete(orderCreated);

            var actualOrder = context.Orders.Find(orderCreated.OrderId);


            Assert.IsTrue(actualOrder == null);
        }

        [Test]
        public void TestUpdatedAction_ReturnsUpdatedAction()
        {
            var context = new ImdbContext(0);
            var orderrep = new FinanceManagerOrdersRepository(context);

            string productName = "Milk";
            bool isDelivery = true;
            int quantity = 10;

            var product = context.Products.SingleOrDefault(p => p.Name == productName);
            var orderCreated = new Order()
            {
                ProductId = product.ProductId,
                IsDelivery = isDelivery,
                Product = product,
                Quantity = quantity,
                Date = DateTime.Now,
            };

            orderrep.Create(orderCreated);

            orderrep.Update(orderCreated);
            var orderUpdated = context.Orders.ToList().Last();

            Assert.That(orderCreated, Is.EqualTo(orderUpdated));
        }
    }
}