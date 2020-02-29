using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

class EventManager:MonoBehaviour
{
    private Dictionary<string, UnityEvent> _eventDictionary;
    private static EventManager _i;

    public static EventManager i
    {
        get
        {
            //Debug.Log("EventManager Get");
            if (!_i)
            {
                _i = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!_i)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {                    
                    _i.Init();
                }
            }

            return _i;
        }
    }

    public void Init()
    {
        //Debug.Log("Init");
        _eventDictionary = new Dictionary<string, UnityEvent>();
    }

    public static void StartListening(string eName, UnityAction listener)
    {
        //Debug.Log("StartListening");
        UnityEvent temp = null;

        if(i._eventDictionary.TryGetValue(eName, out temp))
        {
            temp.AddListener(listener);
        }
        else
        {
            temp = new UnityEvent();
            temp.AddListener(listener);
            _i._eventDictionary.Add(eName, temp);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        //Debug.Log("StopListening");
        if (i == null) return;

        UnityEvent thisEvent = null;
        if (i._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        //Debug.Log("TriggerEvent");
        UnityEvent thisEvent = null;
        if (i._eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            Debug.Log(eventName);
            thisEvent.Invoke();
        }
    }

}
