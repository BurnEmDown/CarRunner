using System;
using System.Collections.Generic;
using System.Linq;

namespace Managers.Events
{
    public class EventsManager : BaseManager
    {
        public Dictionary<EventType, EventListenersData>
            ListenersData = new Dictionary<EventType, EventListenersData>();

        public void AddListener(EventType eventType, Action<object> additionalData)
        {
            if (ListenersData.TryGetValue(eventType, out var value))
            {
                value.ActionsOnInvoke.Add(additionalData);
            }
            else
            {
                ListenersData[eventType] = new EventListenersData(additionalData);
            }
        }

        public void RemoveListener(EventType eventType, Action<object> actionToRemove)
        {
            if (!ListenersData.TryGetValue(eventType, out var value))
            {
                return;
            }

            value.ActionsOnInvoke.Remove(actionToRemove);
            
            if(!value.ActionsOnInvoke.Any())
            {
                ListenersData.Remove(eventType);
            }
        }

        public void InvokeEvent(EventType eventType, object dataToInvoke)
        {
            if (!ListenersData.TryGetValue(eventType, out var value))
            {
                return;
            }

            foreach (var method in value.ActionsOnInvoke)
            {
                method.Invoke(dataToInvoke);
            }
        }
    }
}

