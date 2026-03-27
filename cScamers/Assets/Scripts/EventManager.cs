using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interaface;
using Random = UnityEngine.Random;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [Header("Event Managers")]
    [SerializeField] private EmailEventManager emailManager;
    [SerializeField] private PhoneEventManager phoneManager;
    [SerializeField] private PrinterEventManager printerManager;

    private EventData currentData;
    private EventType? lastEventType = null;
    private IGameEvent currentEvent;

    private bool isWaitingForDecision;
    private Coroutine decisionCoroutine; 

    public enum EventType
    {
        Email,
        Phone,
        Printer
    }

    private void Awake()
    {
        //instance adj.
    }

    private void Start()
    {
        StartRandomEvent();
    }
    
    public void StartRandomEvent()
    {
        currentEvent = GetRandomEvent();

        if (currentEvent == null) return;
        currentEvent.GenerateEvent();
        
        isWaitingForDecision = true;
    }

    private IGameEvent GetRandomEvent()
    {
        List<EventType> possibleEvents = new List<EventType>
        {
            EventType.Email,
            EventType.Phone,
            EventType.Printer
        };
        
        if (lastEventType.HasValue)
            possibleEvents.Remove(lastEventType.Value);

        EventType type = possibleEvents[Random.Range(0, possibleEvents.Count)];
        lastEventType = type;

        switch (type)
        {
            case EventType.Email: return emailManager;
            case EventType.Phone: return phoneManager;
            case EventType.Printer: return printerManager;
        }

        return null;
    }
    
    public void StartEvent(EventData data)
    {
        currentEvent = GetEvent(data.deviceType);
        currentEvent.UpdateEvent(data);

        isWaitingForDecision = true;
    }
    
    private IGameEvent GetEvent(EventType type)
    {
        switch (type)
        {
            case EventType.Email: return emailManager;
            case EventType.Phone: return phoneManager;
            case EventType.Printer: return printerManager;
        }

        return null;
    }
    
    public void PlayerPressedKey1()
    {
        HandleDecision(false);
    }

    public void PlayerPressedKey2()
    {
        HandleDecision(true);
    }

    private void HandleDecision(bool playerThinksScam)
    {
        if (!isWaitingForDecision) return;

        currentData = currentEvent.GetCurrentEvent();
        
        bool correct = currentData.IsScam == playerThinksScam;
        Debug.Log($"{currentEvent.GetType().Name}: {(correct ? "Correct!" : "Wrong!")}");
        currentEvent.EndEvent();
        
        EventData next = playerThinksScam ? currentData.nextEventsIfScammer : currentData.nextEventsIfNormal;
        
        if (next != null)
        {
            StartEvent(next);
            return;
        }
        
        StartRandomEvent();
    }
}