namespace Splinter.Messaging.Messages
{
    /// <summary>
    ///     A message to be sent through an <see cref="IBroker" /> instance.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        ///     Gets or sets the publisher of the message.
        /// </summary>
        /// <value>
        ///     The publisher of the message.
        /// </value>
        object Publisher { get; set; }
    }
}