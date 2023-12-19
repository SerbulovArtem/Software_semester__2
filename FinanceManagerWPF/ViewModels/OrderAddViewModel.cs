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

namespace WPF.ViewModels
{
    public class OrderAddViewModel : INotifyPropertyChanged
    {
        private IOrdersRepository _ordersRepository;
        private IProductsRepository _productsRepository;
        private string _productName;
        private int _quantity;
        private bool _isDelivery;
        public string SelectedTypeProduct
        {
            get { return _productName; }
            set
            {
                if (_productName != value)
                {
                    _productName = value;
                    var list = _productName.Split(' ');
                    _productName = list[1];
                    OnPropertyChanged(nameof(SelectedTypeProduct));
                    
                }
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

        public OrderAddViewModel(IOrdersRepository actionsRepository, IProductsRepository productsRepository)
        {
            _ordersRepository = actionsRepository;
            _productsRepository = productsRepository;
            SubmitCommand = new SubmitAddCommand(this);
        }

        public void Submit()
        {
            var product = _productsRepository.GetList().SingleOrDefault(p => p.ProductName == _productName);

            Order order = new Order
            {
                Product = product,
                ProductId = product.ProductId,
                Quantity = _quantity,
                InsertTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsDelivery = _isDelivery,
            };

            _ordersRepository.Create(order);

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
