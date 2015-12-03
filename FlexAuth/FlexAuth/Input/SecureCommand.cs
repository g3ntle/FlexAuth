using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlexAuth.Input
{
    public class SecureCommand : ICommand
    {
        #region Fields

        private readonly Action action;

        #endregion


        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion


        #region Constructors

        public SecureCommand(Action action)
        {
            this.action = action;
        }

        #endregion


        #region Methods

        public virtual bool CanExecute(object parameter)
        {
            return true;
        }

        public virtual void Execute(object parameter)
        {
            if (CanExecute(parameter)
                && action != null)
                action.Invoke();
        }

        protected void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}
