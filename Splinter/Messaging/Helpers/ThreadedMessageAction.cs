using System;
using Splinter.Messaging.Messages;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     Base class for a <see cref="Action" /> to be performed with a <see cref="IMessage" /> on a specific thread
    /// </summary>
    internal abstract class ThreadedMessageAction
    {
        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        /// <value>
        ///     The action.
        /// </value>
        public virtual Action Action { get; set; }

        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public abstract void InvokeAction(object parameters);
    }
}