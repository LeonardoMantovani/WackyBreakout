using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventInvoker : MonoBehaviour
{
    protected Dictionary<EventName, UnityEvent<int>> events = new Dictionary<EventName, UnityEvent<int>>();

    public void AddListener(EventName eventName, UnityAction<int> listener)
    {
        if (events.ContainsKey(eventName))
        {
            events[eventName].AddListener(listener);
        }
    }
}
