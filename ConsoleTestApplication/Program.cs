using System;
using Splinter.Messaging;
using Splinter.Messaging.Helpers;
using Splinter.Messaging.Messages;

namespace ConsoleTestApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TestBroker();

            Console.ReadKey();
        }

        /// <summary>
        ///     Tests the broker.
        /// </summary>
        private static void TestBroker()
        {
            var responder = new Responder();

            Broker.Current.Publish(new TextMessage("Hello."));

            var subscriber = new SubscriberAndUnsubscriber();
            subscriber.Subscribe();

            Broker.Current.Publish(new TextMessage("Sent after subscribe."));

            Broker.Current.Publish(new TextMessage("Sent to type."), typeof (Responder));

            Broker.Current.Publish(new TextMessage("Sent to object."), subscriber);

            subscriber.Ubsubscribe();

            Broker.Current.Publish(new TextMessage("Sent after unsubscribe."));

            // Shouldn't be received.
            Broker.Current.Publish(new TextMessage("Sent to object after unsubscribe."), subscriber);
        }

        /// <summary>
        ///     A class to respond to messages.
        /// </summary>
        private class Responder : IDisposable
        {
            /// <summary>
            ///     Initializes a new instance of the <see cref="Responder" /> class.
            /// </summary>
            public Responder()
            {
                Broker.Current.Subscribe<TextMessage>(this,
                    o => Console.WriteLine("Message received from {0}: {1}", this, o.Text), ThreadOption.New);
            }

            /// <summary>
            ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                Broker.Current.Unsubscribe(this);
            }

            /// <summary>
            ///     Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            ///     A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return "Responder";
            }
        }

        /// <summary>
        ///     A class that subscribes and unsubscribes.
        /// </summary>
        private class SubscriberAndUnsubscriber
        {
            /// <summary>
            ///     Subscribes this instance.
            /// </summary>
            public void Subscribe()
            {
                Broker.Current.Subscribe<TextMessage>(this,
                    o => Console.WriteLine("Message received from {0}: {1}", this, o.Text), ThreadOption.New);
            }

            /// <summary>
            ///     Ubsubscribes this instance.
            /// </summary>
            public void Ubsubscribe()
            {
                Broker.Current.Unsubscribe(this);
            }

            /// <summary>
            ///     Returns a <see cref="System.String" /> that represents this instance.
            /// </summary>
            /// <returns>
            ///     A <see cref="System.String" /> that represents this instance.
            /// </returns>
            public override string ToString()
            {
                return "Subscriber";
            }
        }
    }
}