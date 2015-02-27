using Splinter.Messaging.Messages;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     The thread options available for publishing and
    ///     subscribing using an <see cref="IBroker" />.
    /// </summary>
    public enum ThreadOption
    {
        /// <summary>
        ///     The current thread will be used.
        /// </summary>
        Current,

        /// <summary>
        ///     A new thread will be created.
        /// </summary>
        New,

        /// <summary>
        ///     The WPF dispatcher thread for this application will be used.
        ///     This ensures the <see cref="IMessage" /> is handled in the UI
        ///     thread.
        ///     <remarks>
        ///         If this option is used in a non-WPF application, the <see cref="ThreadOption.Current" />
        ///         option will be used instead.
        ///     </remarks>
        /// </summary>
        Dispatcher
    }
}