using FinanceManager.BLL;
using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using FinanceManagerWPF;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Unity;
using WPF.ViewModels;

namespace WPF.Views
{
    public partial class OrderListView : Window
    {
        OrderListViewModel ordersListViewModel;
        CollectionViewSource orderCollection;
        public OrderListView(OrderListViewModel vm)
        {
            ordersListViewModel = vm;
            DataContext = vm;
            InitializeComponent();

            orderCollection = (CollectionViewSource)(Resources["OrderCollection"]);
        }

        public void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                IOrdersRepository orderRepository = ((App)Application.Current).Container.Resolve<IOrdersRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity
                IProductsRepository productsRepository = ((App)Application.Current).Container.Resolve<IProductsRepository>();


                var orderChangeViewModel = new OrderAddViewModel(orderRepository, productsRepository);
                var orderChangeWindow = new OrderAddView(orderChangeViewModel);
                orderChangeWindow.ShowDialog();
                ordersListViewModel.Update();
            }
        }

        private void ChangeButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dataGrid = FindParent<DataGrid>(button);
                if (dataGrid != null)
                {
                    var selectedProduct = dataGrid.SelectedItem as Order;

                    // Now 'selectedProduct' holds the object in the selected row.

                    if (selectedProduct != null)
                    {
                        IOrdersRepository orderRepository = ((App)Application.Current).Container.Resolve<IOrdersRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity
                        IProductsRepository productsRepository = ((App)Application.Current).Container.Resolve<IProductsRepository>();

                        var orderChangeViewModel = new OrderChangeViewModel(selectedProduct, orderRepository, productsRepository);
                        var orderChangeWindow = new OrderChangeView(orderChangeViewModel);
                        orderChangeWindow.ShowDialog();
                        ordersListViewModel.Update();
                    }
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var dataGrid = FindParent<DataGrid>(button);
                if (dataGrid != null)
                {
                    var selectedProduct = dataGrid.SelectedItem as Order;

                    // Now 'selectedProduct' holds the object in the selected row.

                    if (selectedProduct != null)
                    {
                        IOrdersRepository orderRepository = ((App)Application.Current).Container.Resolve<IOrdersRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity

                        orderRepository.Delete(selectedProduct);

                        ordersListViewModel.Update();
                    }

                }
            }
        }

        public void PrintAllButton_Click(object sender, RoutedEventArgs e)
        {
            IOrdersRepository orderRepository = ((App)Application.Current).Container.Resolve<IOrdersRepository>(); // You might need to adjust this based on how your ICartBL is registered in Unity

            ordersListViewModel.Update();
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            while (true)
            {
                // Get parent item
                DependencyObject parentObject = VisualTreeHelper.GetParent(child);

                // We've reached the end of the tree
                if (parentObject == null) return null;

                // Check if the parent matches the type we're looking for
                if (parentObject is T parent)
                    return parent;

                child = parentObject;
            }
        }
    }
}
