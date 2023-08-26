using System;
using System.Collections.Generic;

namespace DBD.Events
{
    public class EventMediator
    {
        private static EventMediator _instance;
        public static EventMediator Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new EventMediator();

                return _instance;
            }
            private set => _instance = value;
        }

        private Dictionary<Type, EventHelper> _eventHelpers = new Dictionary<Type, EventHelper>();

        private EventMediator()
        {
            if (_instance != null)
                throw new Exception("Event Mediator: Cannot create multiple instances of EventMediator");

            _instance = this;
        }

        public void Publish<T>(string eventAction, T args)
        {
            if (!_eventHelpers.ContainsKey(typeof(T)))
                _eventHelpers.Add(typeof(T), new EventHelper<T>());

            var helper = _eventHelpers[typeof(T)] as EventHelper<T>;

            helper.Publish(eventAction, args);
        }

        public void Subscribe<T>(string eventAction, Action<T> action)
        {
            if (!_eventHelpers.ContainsKey(typeof(T)))
                _eventHelpers.Add(typeof(T), new EventHelper<T>());

            var helper = _eventHelpers[typeof(T)] as EventHelper<T>;

            helper.Subscribe(eventAction, action);
        }

        public void Unsubscribe<T>(string eventAction, Action<T> action)
        {
            if (!_eventHelpers.ContainsKey(typeof(T)))
                _eventHelpers.Add(typeof(T), new EventHelper<T>());

            var helper = _eventHelpers[typeof(T)] as EventHelper<T>;

            helper.Unsubscribe(eventAction, action);
        }

        private abstract class EventHelper { }

        private class EventHelper<T> : EventHelper
        {
            private Dictionary<string, Action<T>> _subscribers;

            public EventHelper()
            {
                _subscribers = new Dictionary<string, Action<T>>();
            }

            public void Publish(string eventAction, T args)
            {

                if (!_subscribers.ContainsKey(eventAction))
                    return;
                if (_subscribers[eventAction] == null)
                    return;

                _subscribers[eventAction].Invoke(args);
            }

            public void Subscribe(string eventAction, Action<T> action)
            {
                if (!_subscribers.ContainsKey(eventAction))
                {
                    _subscribers[eventAction] = new Action<T>(action);
                    return;
                }

                _subscribers[eventAction] += action;
            }

            public void Unsubscribe(string eventAction, Action<T> action)
            {
                if (!_subscribers.ContainsKey(eventAction))
                    return;

                _subscribers[eventAction] -= action;
            }
        }
    }
}