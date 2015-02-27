using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Splinter.Annotations;
using Splinter.Messaging.Helpers;
using Splinter.Messaging.Messages;

namespace Splinter.Messaging
{
    /// <summary>
    ///     Solid implementation of the <see cref="IBroker" /> interface used for
    ///     publishing and subscribing.
    /// </summary>
    public class Broker : IBroker
    {
        #region Singleton

        /// <summary>
        ///     Gets the current <see cref="IBroker" />.
        /// </summary>
        /// <value>
        ///     The current <see cref="IBroker" />.
        /// </value>
        public static IBroker Current
        {
            get { return Nested.Instance; }
        }

        /// <summary>
        ///     Nested class allows for fully lazy singleton instation.
        /// </summary>
        [UsedImplicitly]
        private class Nested
        {
            internal static readonly IBroker Instance = new Broker();

            static Nested()
            {
            }
        }

        #endregion Singleton

        /// <summary>
        ///     The subscription lock used to ensure thread-safety when accessing the _subscriptions collection.
        /// </summary>
        protected readonly object SubscriptionLock = new object();

        /// <summary>
        ///     The subscriptions in this current instance.
        /// </summary>
        private readonly IDictionary<Type, SubscribersAndActions> _subscriptions =
            new Dictionary<Type, SubscribersAndActions>();

        #region IBroker implementation

        /// <summary>
        ///     Subscribes the specified subscriber to any {TMessage}.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="receivedAction">The action to perform when a {TMessage} is received.</param>
        /// <param name="threadOption">The thread option.</param>
        public virtual void Subscribe<TMessage>(object subscriber, Action<TMessage> receivedAction,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage
        {
            var threadedMessageAction = new ThreadedMessageAction<TMessage>(receivedAction, threadOption);
            var subscriberAndActions = new SubscriberAndActions(subscriber, threadedMessageAction);
            var messageType = typeof (TMessage);

            lock (SubscriptionLock)
            {
                try
                {
                    // There are no subscribers to this message subscriberType.
                    if (!_subscriptions.ContainsKey(messageType) || _subscriptions[messageType] == null)
                    {
                        _subscriptions.Add(messageType, new SubscribersAndActions {subscriberAndActions});
                    }
                    else
                    {
                        // This subscriber is not currently subscribed.
                        if (_subscriptions[messageType][subscriber] == null)
                        {
                            _subscriptions[messageType].Add(subscriberAndActions);
                        }
                            // This subscriber is subscribed, so add a new action.
                        else
                        {
                            _subscriptions[messageType][subscriber].AddAction(threadedMessageAction);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock not held.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Publishes the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="threadOption">The thread option.</param>
        public virtual void Publish<TMessage>(TMessage message, ThreadOption threadOption = ThreadOption.Current)
            where TMessage : IMessage
        {
            switch (threadOption)
            {
                case ThreadOption.New:
                    Task.Run(() => PublishMessage(message));
                    break;
                case ThreadOption.Dispatcher:
                    if (Application.Current != null && Application.Current.Dispatcher != null)
                        Application.Current.Dispatcher.Invoke(() => PublishMessage(message));
                    else
                        PublishMessage(message);
                    break;
                case ThreadOption.Current:
                    PublishMessage(message);
                    break;
            }
        }

        /// <summary>
        ///     Publishes the specified message that will be received by the
        ///     subscriber specified in the target parameter.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        /// <param name="threadOption">The thread option.</param>
        public virtual void Publish<TMessage>(TMessage message, object target,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage
        {
            switch (threadOption)
            {
                case ThreadOption.New:
                    Task.Run(() => PublishMessageToTarget(message, target));
                    break;
                case ThreadOption.Dispatcher:
                    if (Application.Current != null && Application.Current.Dispatcher != null)
                        Application.Current.Dispatcher.Invoke(() => PublishMessageToTarget(message, target));
                    else
                        PublishMessageToTarget(message, target);
                    break;
                case ThreadOption.Current:
                    PublishMessageToTarget(message, target);
                    break;
            }
        }

        /// <summary>
        ///     Publishes the specified message that will be received
        ///     by any subscriber of the same type as the targetType parameter.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="threadOption">The thread option.</param>
        public virtual void Publish<TMessage>(TMessage message, Type targetType,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage
        {
            switch (threadOption)
            {
                case ThreadOption.New:
                    Task.Run(() => PublishMessageToType(message, targetType));
                    break;
                case ThreadOption.Dispatcher:
                    if (Application.Current != null && Application.Current.Dispatcher != null)
                        Application.Current.Dispatcher.Invoke(() => PublishMessageToType(message, targetType));
                    else
                        PublishMessageToType(message, targetType);
                    break;
                case ThreadOption.Current:
                    PublishMessageToType(message, targetType);
                    break;
            }
        }

        /// <summary>
        ///     Unsubscribes the specified subscriber from all <see cref="IMessage" />s
        ///     of type {TMessage}.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        public virtual void Unsubscribe<TMessage>(object subscriber)
        {
            var messageType = typeof (TMessage);
            lock (SubscriptionLock)
            {
                try
                {
                    if (_subscriptions[messageType] == null || _subscriptions[messageType][subscriber] == null) return;

                    // Remove all the Actions.
                    foreach (var action in _subscriptions[messageType][subscriber].Actions)
                    {
                        _subscriptions[messageType][subscriber].RemoveAction(action);
                    }

                    var subscription = _subscriptions[messageType].FirstOrDefault(s => s.Subscriber.Equals(subscriber));
                    if (subscription != null)
                    {
                        _subscriptions[messageType].Remove(subscription);
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Unsubscribes the specified subscriber from all <see cref="IMessage" />s.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public virtual void Unsubscribe(object subscriber)
        {
            lock (SubscriptionLock)
            {
                try
                {
                    var subscriptionsToRemove = new List<SubscriberAndActions>();

                    foreach (var subscription in _subscriptions.Values)
                    {
                        foreach (var subscribersAndAction in subscription.Where(sa => sa.Subscriber.Equals(subscriber)))
                        {
                            subscribersAndAction.RemoveAllActions();
                            subscriptionsToRemove.Add(subscribersAndAction);
                            //subscription.Remove(subscribersAndAction);
                        }
                        foreach (var subscriptionToRemove in subscriptionsToRemove)
                        {
                            subscription.Remove(subscriptionToRemove);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Unsubscribes the specified subscriber's action.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="receivedAction">The received action.</param>
        public virtual void Unsubscribe<TMessage>(object subscriber, Action<object> receivedAction)
        {
            lock (SubscriptionLock)
            {
                try
                {
                    foreach (var subscription in _subscriptions.Values)
                    {
                        foreach (var subscribersAndAction in subscription.Where(sa => sa.Subscriber.Equals(subscriber)))
                        {
                            subscribersAndAction.RemoveAction(receivedAction);
                            if (!subscribersAndAction.Actions.Any())
                            {
                                subscription.Remove(subscribersAndAction);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        #endregion IBroker implementation

        /// <summary>
        ///     Publishes the message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        protected virtual void PublishMessage<TMessage>(TMessage message)
        {
            var messageType = typeof (TMessage);

            lock (SubscriptionLock)
            {
                try
                {
                    if (!_subscriptions.ContainsKey(messageType)) return;
                    // Get all the subscribed actions for the TMessage type.
                    foreach (
                        var action in _subscriptions[messageType].SelectMany(subscriber => subscriber.Actions).ToArray()
                        )
                    {
                        action.InvokeAction(message);
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Publishes the message to a specified subscriber type.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="subscriberType">Type of the subscriber.</param>
        protected virtual void PublishMessageToType<TMessage>(TMessage message, Type subscriberType)
        {
            var messageType = typeof (TMessage);
            lock (SubscriptionLock)
            {
                try
                {
                    if (!_subscriptions.ContainsKey(messageType)) return;

                    foreach (
                        var action in
                            _subscriptions[messageType].Where(sub => sub.Subscriber.GetType() == subscriberType)
                                .SelectMany(sub => sub.Actions).ToArray())
                    {
                        action.InvokeAction(message);
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }

        /// <summary>
        ///     Publishes the message to a target object.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="target">The target object.</param>
        protected virtual void PublishMessageToTarget<TMessage>(TMessage message, object target)
        {
            var messageType = typeof (TMessage);
            lock (SubscriptionLock)
            {
                try
                {
                    if (!_subscriptions.ContainsKey(messageType)) return;
                    foreach (
                        var action in
                            _subscriptions[messageType].Where(sub => sub.Subscriber == target)
                                .SelectMany(sub => sub.Actions)
                                .ToArray())
                    {
                        action.InvokeAction(message);
                    }
                }
                catch (Exception ex)
                {
                    // To ensure lock released.
                    Debug.WriteLine(ex.Message);
                }
            }
        }
    }
}