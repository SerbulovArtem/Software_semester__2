using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF.ViewModels;

namespace WPF.Commands
{
    public class SubmitAddCommand : ICommand
    {
        private OrderAddViewModel _vm;
        public SubmitAddCommand(OrderAddViewModel vm)
        {
            _vm = vm;
        }

        #region ICommand
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return _vm.IsValid;
        }

        public void Execute(object parameter)
        {
            _vm.Submit();
            _vm.Submited.Invoke();
        }
        #endregion
    }
}
