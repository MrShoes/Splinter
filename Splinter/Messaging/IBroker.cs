using System;
using Splinter.Messaging.Helpers;
using Splinter.Messaging.Messages;

namespace Splinter.Messaging
{
    /// <summary>
    ///     Handles all subscriptions to <see cref="IMessage" /> types and allows publishing.
    ///     Ensures an anonymous publish/subscribe pattern can be used.
    /// </summary>
    public interface IBroker
    {
        /// <summary>
        ///     Subscribes the specified subscriber to any {TMessage}.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="receivedAction">The action to perform when a {TMessage} is received.</param>
        /// <param name="threadOption">The thread option.</param>
        void Subscribe<TMessage>(object subscriber, Action<TMessage> receivedAction,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage;

        /// <summary>
        ///     Publishes the specified message.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="threadOption">The thread option.</param>
        void Publish<TMessage>(TMessage message, ThreadOption threadOption = ThreadOption.Current)
            where TMessage : IMessage;

        /// <summary>
        ///     Publishes the specified message that will be received by the
        ///     subscriber specified in the target parameter.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="target">The target.</param>
        /// <param name="threadOption">The thread option.</param>
        void Publish<TMessage>(TMessage message, object target,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage;

        /// <summary>
        ///     Publishes the specified message that will be received
        ///     by any subscriber of the same type as the targetType parameter.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="threadOption">The thread option.</param>
        void Publish<TMessage>(TMessage message, Type targetType,
            ThreadOption threadOption = ThreadOption.Current) where TMessage : IMessage;

        /// <summary>
        ///     Unsubscribes the specified subscriber from all <see cref="IMessage" />s
        ///     of type {TMessage}.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        void Unsubscribe<TMessage>(object subscriber);

        /// <summary>
        ///     Unsubscribes the specified subscriber from all <see cref="IMessage" />s.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Unsubscribe(object subscriber);

        /// <summary>
        ///     Unsubscribes the specified subscriber's action.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="subscriber">The subscriber.</param>
        /// <param name="receivedAction">The received action.</param>
        void Unsubscribe<TMessage>(object subscriber, Action<object> receivedAction);
    }
}