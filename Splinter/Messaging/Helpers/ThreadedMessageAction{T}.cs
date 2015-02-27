using System;
using System.Threading.Tasks;
using System.Windows;
using Splinter.Messaging.Messages;

namespace Splinter.Messaging.Helpers
{
    /// <summary>
    ///     An <see cref="Action" /> with a specified <see cref="ThreadOption" />.
    /// </summary>
    internal class ThreadedMessageAction<TMessage> : ThreadedMessageAction where TMessage : IMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ThreadedMessageAction" /> class.
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="threadOption">The thread option.</param>
        public ThreadedMessageAction(Action<TMessage> action, ThreadOption threadOption = ThreadOption.Current)
        {
            Action = action;
            ThreadOption = threadOption;
        }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        /// <value>
        ///     The action.
        /// </value>
        public new Action<TMessage> Action { get; set; }

        /// <summary>
        ///     Gets or sets the thread option.
        /// </summary>
        /// <value>
        ///     The thread option.
        /// </value>
        public ThreadOption ThreadOption { get; set; }

        /// <summary>
        ///     Invokes the action.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public override void InvokeAction(object parameters)
        {
            if (parameters.GetType() == typeof (TMessage))
                InvokeActionOnMessage((TMessage) parameters);
        }

        /// <summary>
        ///     Invokes the action on message.
        /// </summary>
        /// <param name="message">The message.</param>
        protected virtual void InvokeActionOnMessage(TMessage message)
        {
            if (Action == null) return;
            switch (ThreadOption)
            {
                case ThreadOption.New:
                    Task.Run(() => Action.Invoke(message));
                    break;
                case ThreadOption.Dispatcher:
                    if (Application.Current != null && Application.Current.Dispatcher != null)
                    {
                        Application.Current.Dispatcher.Invoke(Action, message);
                    }
                    else
                    {
                        Action.Invoke(message);
                    }
                    break;
                case ThreadOption.Current:
                    Action.Invoke(message);
                    break;
            }
        }
    }
}