using System;
using System.Windows;
using System.Windows.Input;

namespace Splinter
{
    /// <summary>
    ///     A base class for all commands used by the application.
    /// </summary>
    public abstract class Command : ICommand
    {
        /// <summary>
        ///     INdicates whether the command can execute.
        /// </summary>
        private bool _canExecute;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Command" /> class.
        /// </summary>
        /// <param name="canExecute">if set to <c>true</c> [can execute].</param>
        protected Command(bool canExecute = true)
        {
            _canExecute = canExecute;
        }

        /// <summary>
        ///     Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        /// <returns>
        ///     true if this command can be executed; otherwise, false.
        /// </returns>
        public virtual bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        /// <summary>
        ///     Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public abstract void Execute(object parameter);

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        ///     Called when can execute changed.
        /// </summary>
        protected virtual void OnCanExecuteChanged()
        {
            // Ensure this raises on the UI thread if possible.

            if (Application.Current != null)
                Application.Current.Dispatcher.Invoke(RaiseCanExecuteChangedEventHandler);
            else
            {
                RaiseCanExecuteChangedEventHandler();
            }
        }

        /// <summary>
        ///     Raises the CanExecuteChanged event handler.
        /// </summary>
        protected virtual void RaiseCanExecuteChangedEventHandler()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
                handler(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Changes the can execute variable.
        /// </summary>
        /// <param name="newValue"><c>true</c> if the Command can execute; otherwise, <c>false</c>.</param>
        protected virtual void ChangeCanExecute(bool newValue)
        {
            if (_canExecute == newValue) return;
            _canExecute = newValue;
            OnCanExecuteChanged();
        }
    }
}