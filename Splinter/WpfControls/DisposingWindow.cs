using System;
using System.Windows;

namespace Splinter.WpfControls
{
    /// <summary>
    ///     A <see cref="Window" /> that implements <see cref="IDisposable" />.
    /// </summary>
    public abstract class DisposingWindow : Window, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DisposingWindow" /> class.
        /// </summary>
        protected DisposingWindow()
        {
            Closed += (sender, args) => Dispose();
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