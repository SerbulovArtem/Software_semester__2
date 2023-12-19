using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using FinanceManager.BLL;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using WPF.Views;

namespace WPF.ViewModels
{
    public class OrderListViewModel : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged;


        public void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }



        private IOrdersRepository ordersRep;
        private ObservableCollection<Order> orders;
        public ICommand NavigateToUserDetailCommand { get; }



        public ObservableCollection<Order> OrderList
        { get { return orders; } 
            set {
                orders = value;
                OnPropertyChanged(nameof(OrderList));
            }
        }

        public OrderListViewModel(IOrdersRepository ordersRep)
        {
            this.ordersRep = ordersRep;
            Update();
        }


        public void Update()
        {
            var orders = ordersRep.GetList();
            OrderList = new ObservableCollection<Order>(orders);
        }
    }
}
