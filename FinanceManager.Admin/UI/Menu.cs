using FinanceManager.DAL.Data;
using FinanceManager.DTO;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using FinanceManager.DAL.Repositories.Concreate.DataBaseMCSQLFinanceManager;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.BLL.Repositories.Concreate.DataBaseMCSQLFinanceManager;

namespace ActionManager.Admin.UI
{
    class Menu
    {
        private FinanceManagerAdminContext _context;
        private FinanceManagerOrdersRepository _ordersRepository;
        private IUsersRepository _usersRepository;
        private string username;
        private string password;

        public Menu()
        {
            _context = new FinanceManagerAdminContext(1);
            _ordersRepository = new FinanceManagerOrdersRepository(_context);
            _usersRepository = new FinanceManagerUserRepository(_context);

            while (Authentication()) { }
        }

        public void Demo()
        {
            while (DemoOnce()) { }
        }

        private bool DemoOnce()
        {
            Console.WriteLine("Select option:\n1. - Print All Orders.\n2. - Crate New Order.\n3. - Change Order.\n4. - Delete Order.\n0. - Login Menu.\n-1 - Exit");
            string userInput = Console.ReadLine();

            try
            {
                switch (userInput)
                {
                    case "1":
                        PrintAllOrders();
                        return true;
                    case "2":
                        CreateOrder();
                        return true;
                    case "3":
                        UpdateOrder();
                        return true;
                    case "4":
                        DeleteOrder();
                        return true;
                    case "0":
                        while (Authentication()) { }
                        return true;
                    case "-1":
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured, check your input data");
                Console.WriteLine(ex);
                return true;
            }
        }

        public bool Authentication()
        {
            username = "";
            password = "";
            Console.WriteLine("Select option:\n1. - Login.\n-1. - Exit.");
            string userInput = Console.ReadLine();
            try
            {
                switch (userInput)
                {
                    case "1":
                        Console.WriteLine("~~~~~Enter Username and Password~~~~~");

                        var userInputList = Console.ReadLine()!.Split(' ');
                        username = userInputList[0];
                        password = userInputList[1];

                        return IsAuthenticated(username, password);
                    case "-1":
                        Console.WriteLine("~~~~~Access terminated~~~~~");
                        Environment.Exit(0);
                        return false;
                    default:
                        return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occured:");
                Console.WriteLine(ex);
                return true;
            }
        }

        public bool IsAuthenticated(string username, string password)
        {
            foreach (var user in _context.Users)
            {
                if (username == user.Username)
                {
                    if (_usersRepository.VerifyPassword(password + user.Salt, user.Password))
                    {
                        Console.WriteLine($"~~~~~Access granted~~~~~" +
                            $"\n~~~~~Welcome {username}~~~~~");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("~~~~~Access denied~~~~~" +
                            "\n~~~~~Wrong Username or Password~~~~~");
                        return true;
                    }
                }
            }
            Console.WriteLine("~~~~~Access denied~~~~~" +
                "\n~~~~~Wrong Username or Password~~~~~");
            return true;
        }

        public void PrintAllOrders()
        {
            foreach (var order in _ordersRepository.GetDbSet().Include(a => a.Product))
            {
                Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.InsertTime}, Is Delivery: {order.IsDelivery}, Product Name: {order.Product.ProductName}" +
                    $"Product Price: {order.Product.Price}, Product Quantity: {order.Product.Quantity}, Order Quantity: {order.Quantity}\n");
            }
        }


        public void CreateOrder()
        {
            Console.WriteLine("Enter Product Name, Is Delivery and Quantity:");
            var input = Console.ReadLine()!.Split(' ');
            string productName = input[0];
            string input2 = input[1];
            bool isDelivery;
            if (input2 == "1")
            {
                isDelivery = true;
            }
            else
            {
                isDelivery = false;
            }
            int quantity= Convert.ToInt32(input[2]);

            var product = _context.Products.SingleOrDefault(p => p.ProductName == productName);
            if (product != null)
            {
                var order = new Order()
                {
                    ProductId = product.ProductId,
                    IsDelivery = isDelivery,
                    Product = product,
                    Quantity = quantity,
                    InsertTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                _ordersRepository.Create(order);
            }
            else
            {
                Console.WriteLine("This product name doesn't exist, try again");
                return;
            }
            Console.WriteLine("Order Added");
        }

        public void UpdateOrder()
        {
            Console.WriteLine("Enter Order Id, Is Delivery and Quantity:");
            var input = Console.ReadLine()!.Split(' ');
            int orderId = Convert.ToInt32(input[0]);
            string input2 = input[1];
            bool isDelivery;
            if (input2 == "1")
            {
                isDelivery = true;
            }
            else
            {
                isDelivery = false;
            }
            int quantity = Convert.ToInt32(input[2]);

            var order = _context.Orders.Find(orderId);

            if (order != null)
            {
                order.IsDelivery = isDelivery;
                order.Quantity = quantity;
                order.UpdateTime = DateTime.Now;

                _ordersRepository.Update(order);
            }
            else
            {
                Console.WriteLine("This Order Id doesn't exist, try again");
                return;
            }
            Console.WriteLine("Order Updated");
        }

        public void DeleteOrder()
        {
            Console.WriteLine("Enter Order Id");
            var orderId = Convert.ToInt32(Console.ReadLine());
            var order = _context.Orders.Find(orderId);
            _ordersRepository.Delete(order);
            Console.WriteLine("Order Deleted");
        }
    }
}