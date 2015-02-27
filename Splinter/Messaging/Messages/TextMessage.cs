using System.Runtime.Serialization;

namespace Splinter.Messaging.Messages
{
    /// <summary>
    ///     A message containing a single piece of text.
    /// </summary>
    [DataContract]
    public class TextMessage : Message
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TextMessage" /> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="publisher">The publisher.</param>
        public TextMessage(string text, object publisher = null)
            : base(publisher)
        {
            Text = text;
        }

        /// <summary>
        ///     Gets or sets the text.
        /// </summary>
        /// <value>
        ///     The text.
        /// </value>
        [DataMember]
        public string Text { get; set; }
    }
}