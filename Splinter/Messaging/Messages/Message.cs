using System.Runtime.Serialization;

namespace Splinter.Messaging.Messages
{
    /// <summary>
    ///     Base class for all message types.
    /// </summary>
    [DataContract]
    public abstract class Message : IMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Message" /> class.
        /// </summary>
        /// <param name="publisher">The publisher.</param>
        protected Message(object publisher = null)
        {
            Publisher = publisher;
        }

        /// <summary>
        ///     Gets or sets the publisher of the message.
        /// </summary>
        /// <value>
        ///     The publisher of the message.
        /// </value>
        [DataMember]
        public object Publisher { get; set; }
    }
}