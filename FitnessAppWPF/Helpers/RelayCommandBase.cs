using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FitnessAppWPF.Helpers
{
    public abstract class RelayCommandBase : ICommand
    {
        protected readonly Func<bool> _canExecute;
        protected readonly Action _execute;





        public event EventHandler CanExecuteChanged
        {
            add
            {

                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {

                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        [DebuggerStepThrough]
        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);


    }

}
