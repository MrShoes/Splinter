using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     A collection of <see cref="SubscriberAndActions" />s that exposes an enumerator.
    /// </summary>
    internal class SubscribersAndActions : IEnumerable<SubscriberAndActions>
    {
        private readonly IList<SubscriberAndActions> _subscriberAndActions = new List<SubscriberAndActions>();

        /// <summary>
        ///     Gets the <see cref="SubscriberAndActions" /> with the specified subscriber.
        /// </summary>
        /// <value>
        ///     The <see cref="SubscriberAndActions" />.
        /// </value>
        /// <param name="subscriber">The subscriber.</param>
        /// <returns></returns>
        public SubscriberAndActions this[object subscriber]
        {
            get { return _subscriberAndActions.FirstOrDefault(sub => sub.Subscriber.Equals(subscriber)); }
            set
            {
                var subAndActions = _subscriberAndActions.FirstOrDefault(sub => sub.Subscriber.Equals(subscriber));
                if (subAndActions != null)
                {
                    _subscriberAndActions.Remove(subAndActions);
                }
                _subscriberAndActions.Add(value);
            }
        }

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<SubscriberAndActions> GetEnumerator()
        {
            return _subscriberAndActions.GetEnumerator();
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
        ///     Adds the specified subscriber and actions.
        /// </summary>
        /// <param name="subscriberAndActions">The subscriber and actions.</param>
        public virtual void Add(SubscriberAndActions subscriberAndActions)
        {
            _subscriberAndActions.Add(subscriberAndActions);
        }

        /// <summary>
        ///     Removes the specified subscription.
        /// </summary>
        /// <param name="subscription">The subscription.</param>
        public virtual void Remove(SubscriberAndActions subscription)
        {
            _subscriberAndActions.Remove(subscription);
        }
    }
}