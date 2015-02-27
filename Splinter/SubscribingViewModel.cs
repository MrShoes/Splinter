using System;
using Splinter.Messaging;
using Splinter.Messaging.Messages;

namespace Splinter
{
    /// <summary>
    ///     A <see cref="ViewModel" /> that subscribes to <see cref="IMessage" />s via the <see cref="IBroker" />
    ///     and must unsubscribe when no longer in use.
    /// </summary>
    public class SubscribingViewModel : ViewModel, IDisposable
    {
        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Broker.Current.Unsubscribe(this);
        }
    }
}