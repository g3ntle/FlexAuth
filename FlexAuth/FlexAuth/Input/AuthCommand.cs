using FlexAuth.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlexAuth.Input
{
    public class AuthCommand : ICommand
    {
        #region Fields

        private readonly Action action;
        private readonly string requiredNode;

        #endregion


        #region Events

        public event EventHandler CanExecuteChanged;

        #endregion


        #region Constructors

        public AuthCommand(Action action, string requiredNode)
        {
            this.action = action;
            this.requiredNode = requiredNode;
        }

        public AuthCommand(Action action)
            : this(action, null)
        { }

        #endregion


        #region Methods

        public virtual bool CanExecute(object parameter)
        {
            if (String.IsNullOrEmpty(requiredNode))
                return true;

            return UserManager.GetInstance().HasPermission(requiredNode);
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
