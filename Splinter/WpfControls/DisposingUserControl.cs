using System;
using System.Windows.Controls;

namespace Splinter.WpfControls
{
    /// <summary>
    ///     A <see cref="UserControl" /> that implements <see cref="IDisposable" />.
    /// </summary>
    public abstract class DisposingUserControl : UserControl, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposingUserControl" /> class.
        /// </summary>
        protected DisposingUserControl()
        {
            Unloaded += (sender, args) => Dispose();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            if (DataContext == null) return;

            var disposableContext = DataContext as IDisposable;
            if (disposableContext == null) return;

            disposableContext.Dispose();
        }
    }
}