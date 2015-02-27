using System.Collections;
using System.Collections.Generic;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     A collection of <see cref="ThreadedMessageAction" />s that exposes an enumerator.
    /// </summary>
    internal class ThreadedMessageActions : IEnumerable<ThreadedMessageAction>
    {
        /// <summary>
        ///     The message actions.
        /// </summary>
        protected readonly IList<ThreadedMessageAction> MessageActions = new List<ThreadedMessageAction>();


        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<ThreadedMessageAction> GetEnumerator()
        {
            return MessageActions.GetEnumerator();
        }

        /// <summary>
        ///     Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        ///     An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Adds the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public virtual void Add(ThreadedMessageAction action)
        {
            MessageActions.Add(action);
        }

        /// <summary>
        ///     Removes the specified action.
        /// </summary>
        /// <param name="action">The action.</param>
        public virtual void Remove(ThreadedMessageAction action)
        {
            MessageActions.Remove(action);
        }
    }
}