using FinanceManager.BLL;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Commands;
using WPF.Utilities;
using WPF.Views;
using WPF.Commands;
using WPF.Interfaces;
using WPF.Utilities;
using System.Linq;
using Avalonia.Controls;

namespace WPF.ViewModels
{
    public class OrderChangeViewModel : INotifyPropertyChanged
    {
        private Order _order;
        private IOrdersRepository _ordersRepository;
        private IProductsRepository _productsRepository;
        private int _quantity;
        private bool _isDelivery;

        public Order Order
        {
            get { return _order; }
            set
            {
                _order = value;
                OnPropertyChanged(nameof(Order));
            }
        }

        public int Quantity
        {
            get { return _quantity; }
            set
            {
                _quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public string IsDelivery
        {
            get { return Convert.ToString(_isDelivery); }
            set
            {
                var temp = value;
                var list = temp.Split(' ');
                _isDelivery = bool.Parse(list[1]);
                OnPropertyChanged(nameof(IsDelivery));
            }
        }

        public ICommand SubmitCommand { get; }

        public OrderChangeViewModel(Order order, IOrdersRepository ordersRepository, IProductsRepository productsRepository)
        {
            _order = order;
            _ordersRepository = ordersRepository;
            _productsRepository = productsRepository;
            SubmitCommand = new SubmitChangeCommand(this);
        }

        public void Submit()
        {
            var order = _ordersRepository.GetList().Find(a => a.OrderId == Order.OrderId);

            order.Quantity = _quantity;
            order.IsDelivery = _isDelivery;
            order.UpdateTime = DateTime.Now;

            _ordersRepository.Update(order);

            // Add your logic here for handling the submit action
            // You can access the entered quantity using Quantity
            // and other details using Product properties.
            // Your business logic goes here.
        }

        public Action Submited { get; set; }
        public bool IsValid
        {
            get
            {
                return _ordersRepository.CheckQuantity(Quantity);
            }
        }
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
