using System;
using System.Collections.Generic;

namespace Managers.Events
{
    public class EventListenersData
    {
        public List<Action<object>> ActionsOnInvoke;

        public EventListenersData(Action<object> additionalData)
        {
            ActionsOnInvoke = new List<Action<object>>
            {
                additionalData
            };
        }
    }
}

