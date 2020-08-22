using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;


/*
Event messaging system for Unity with flexible parameter passing
Code is 99% from the untiy tutorial here: https://unity3d.com/learn/tutorials/topics/scripting/events-creating-simple-messaging-system
the other 1% is from this forum post: https://forum.unity.com/threads/messaging-system-passing-parameters-with-the-event.331284/
*/

// Replacement of the basic Unityevent with a unity event that has a string that will be used to serialize JSON
[System.Serializable]
public class ThisEvent : UnityEvent<string> { }

/// <summary>
/// Singleton EventManager.
/// </summary>
public class EventManager : MonoBehaviour
{
    private Dictionary<string, ThisEvent> eventDictionary;

    private static EventManager _instance;

    public static EventManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        print("EventManager: init");
        _instance = this;

        // Creates dictionary for the events.
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, ThisEvent>();
        }
    }

    /// <summary>
    /// function called to insert an event in the dictionary.
    /// </summary>
    /// <param name="eventName"> Event to listen to. </param>
    /// <param name="listener"> Callback action to be called on event. </param>
    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        ThisEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new ThisEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Removes an event from the dictionary.
    /// </summary>
    /// <param name="eventName"> Event to remove to. </param>
    /// <param name="listener"> Callback action mapped to this event. </param>
    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (_instance == null) return;
        ThisEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Trigger an event with string params.
    /// </summary>
    /// <param name="eventName"> Event to trigger.</param>
    /// <param name="json"> String json params to pass to all the callback actions mapped to this event. </param>
    public static void TriggerEvent(string eventName, string json)
    {
        ThisEvent thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            // Passes the params to all the callbacks waiting on this event.
            thisEvent.Invoke(json);
        }
    }
}