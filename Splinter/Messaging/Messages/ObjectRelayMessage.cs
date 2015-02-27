using System.Runtime.Serialization;

namespace Splinter.Messaging.Messages
{
    /// <summary>
    ///     A <see cref="Message" /> used to send an object instance.
    /// </summary>
    [DataContract]
    public class ObjectRelayMessage : Message
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ObjectRelayMessage" /> class.
        /// </summary>
        /// <param name="relayedObject">The relayed object.</param>
        /// <param name="publisher">The publisher.</param>
        public ObjectRelayMessage(object relayedObject, object publisher = null)
            : base(publisher)
        {
            RelayedObject = relayedObject;
        }

        /// <summary>
        ///     Gets or sets the relayed object.
        /// </summary>
        /// <value>
        ///     The relayed object.
        /// </value>
        [DataMember]
        public object RelayedObject { get; set; }
    }
}