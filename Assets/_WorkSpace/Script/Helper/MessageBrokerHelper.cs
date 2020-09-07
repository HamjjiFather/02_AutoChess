using System;
using UniRx;


namespace Helper
{
    public static class MessageBrokerHelper
    {
        public static void Publish<T>(T message)
        {
            MessageBroker.Default.Publish<T> (message);
        }


        public static IObservable<T> Receive<T> ()
        {
            return MessageBroker.Default.Receive<T> ();
        }
    }
}