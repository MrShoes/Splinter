using System;
using System.Linq;
using Splinter.Messaging.Messages;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     Represents a single subscriber and all its subscription actions.
    /// </summary>
    internal class SubscriberAndActions
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriberAndActions" /> class.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public SubscriberAndActions(object subscriber)
        {
            Subscriber = subscriber;
            Actions = new ThreadedMessageActions();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriberAndActions" /> class.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="action">The action.</param>
        public SubscriberAndActions(object subscriber, ThreadedMessageAction action)
            : this(subscriber)
        {
            Actions.Add(action);
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SubscriberAndActions" /> class.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="actions">The actions.</param>
        public SubscriberAndActions(object subscriber, ThreadedMessageActions actions)
            : this(subscriber)
        {
            Actions = actions;
        }

        /// <summary>
        ///     Gets or sets the subscriber.
        /// </summary>
        /// <value>
        ///     The subscriber.
        /// </value>
        public object Subscriber { get; set; }

        /// <summary>
        ///     Gets the actions.
        /// </summary>
        /// <value>
        ///     The actions.
        /// </value>
        public ThreadedMessageActions Actions { get; private set; }

        /// <summary>
        ///     Adds the action.
        /// </summary>
        /// <param name="messageAction">The message action.</param>
        public virtual void AddAction(ThreadedMessageAction messageAction)
        {
            Actions.Add(messageAction);
        }

        /// <summary>
        ///     Adds the action.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="threadOption">The thread option.</param>
        public virtual void AddAction<TMessage>(Action<TMessage> action, ThreadOption threadOption = ThreadOption.Current)
            where TMessage : IMessage
        {
            Actions.Add(new ThreadedMessageAction<TMessage>(action, threadOption));
        }

        /// <summary>
        ///     Removes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        public virtual void RemoveAction(ThreadedMessageAction action)
        {
            var thisAction = Actions.FirstOrDefault(act => act.Equals(action));
            if (thisAction != null)
                Actions.Remove(thisAction);
        }

        /// <summary>
        ///     Removes the action.
        /// </summary>
        /// <param name="action">The action.</param>
        public virtual void RemoveAction<TMessage>(Action<TMessage> action)
        {
            var thisAction = Actions.FirstOrDefault(act => act.Action.Equals(action));
            if (thisAction != null)
                Actions.Remove(thisAction);
        }

        /// <summary>
        ///     Removes all actions.
        /// </summary>
        public virtual void RemoveAllActions()
        {
            Actions = new ThreadedMessageActions();
        }
    }
}