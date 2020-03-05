using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    static Dictionary<EventName, List<IntEventInvoker>> invokers = new Dictionary<EventName, List<IntEventInvoker>>();

    static Dictionary<EventName, List<UnityAction<int>>> listeners = new Dictionary<EventName, List<UnityAction<int>>>();

    public static void Initialize()
    {
        //create a invoker list and a listeners list for each event in the EventName enumeration (or clear the lists if they already exist for that event)
        foreach (EventName eventName in Enum.GetValues(typeof(EventName)))
        {
            if (!invokers.ContainsKey(eventName))
            {
                invokers.Add(eventName, new List<IntEventInvoker>());
                listeners.Add(eventName, new List<UnityAction<int>>());
            }
            else
            {
                invokers[eventName].Clear();
                listeners[eventName].Clear();
            }

        }
    }

    public static void AddEventInvoker(EventName @event, IntEventInvoker invoker)
    {
        // Add all existing listeners for that event to the new invoker
        foreach (UnityAction<int> listener in listeners[@event])
        {
            invoker.AddListener(@event, listener);
        }
        
        // Add the invoker to the invokers list of that event
        invokers[@event].Add(invoker);
    }

    public static void AddEventListener(EventName @event, UnityAction<int> listener)
    {
        // Add the new listener to all existing invokers of that event
        foreach (IntEventInvoker invoker in invokers[@event])
        {
            invoker.AddListener(@event, listener);
        }

        // Add the listener to the listeners list of that event
        listeners[@event].Add(listener);
    }

    public static void RemoveEventInvoker(EventName @event, IntEventInvoker invoker)
    {
        invokers[@event].Remove(invoker);
    }
}
