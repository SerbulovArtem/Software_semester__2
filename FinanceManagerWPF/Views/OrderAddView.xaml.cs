using FinanceManager.DAL.Repositories.Abstract.DataBaseMCSQLFinanceManager;
using FinanceManager.DTO;
using FinanceManagerWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WPF.ViewModels;

namespace WPF.Views
{
    /// <summary>
    /// Interaction logic for ProductBuy.xaml
    /// </summary>
    public partial class OrderAddView : Window
    {
        public OrderAddView(OrderAddViewModel orderAddViewModel)
        {
            DataContext = orderAddViewModel;
            InitializeComponent();
            Loaded += ActionAdd_Loaded;
        }

        private void ActionAdd_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is OrderAddViewModel vm)
            {
                vm.Submited += () =>
                {
                    DialogResult = true;
                    this.Close();
                };
            }
        }
    }
}